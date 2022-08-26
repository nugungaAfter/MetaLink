using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NetworkType
{
    Send,
    Receive,
}

public abstract class NetworkHandler : MonoBehaviour
{
    [SerializeField, ReadOnly]       protected NetworkHandlingManager m_NetworkHandlingManager;

    [Header("ID")]
    [SerializeField, ReadOnly(true)] protected string m_HandleUUID;
    [Header("UpdateTime")]
    [SerializeField, ReadOnly(true)] protected TrackingUpdateTimeType m_UpdateTimeType;
    [SerializeField, ReadOnly(true)] protected int m_TrackingCount;
    [Header("Subscriber")]
    [SerializeField, ReadOnly(true)] protected TrackingSubscriberType m_SubscriberType;
    [SerializeField, ReadOnly(true)] protected string m_SubscriberName;
    [Header("Connected")]
    [SerializeField, ReadOnly(true)] protected bool m_Activate;
    [SerializeField, ReadOnly(true)] protected NetworkType m_isSender; // 보내는 사람인지 기준

    public TrackingSubscriberType SubscriberType { get => m_SubscriberType; }
    public string HandleUUID { get => m_HandleUUID; }
    public string SubscriberName { get => m_SubscriberName; }
    public NetworkType isSender { get => m_isSender; set => m_isSender = value; }

    /// <summary>
    /// 이 함수를 통해 보낼 값을 정의함.
    /// { Client(Me) } => Net => Client(Other)
    /// 앞 뒤로 들어가는 공백은 최대한 지양
    /// </summary>
    /// <returns> Send Value </returns>
    protected abstract string TargetObjectData();
    /// <summary>
    /// 이 함수를 통해 해당 트래킹 조건이 참일 경우 보냄
    /// </summary>
    /// <returns> True is Server Send </returns>
    protected abstract bool TrackingExpression();
    /// <summary>
    /// 이 함수를 통해서 네트워크로 받을 정보를 기입함.
    /// Client(Other) => Net => { Client(Me) }
    /// 앞 뒤로 들어가는 공백은 최대한 삭제해서 들어감
    /// </summary>
    /// <param name="Context"></param>
    protected abstract void ParseData(string Context);

    protected virtual void Awake()
    {
        m_NetworkHandlingManager = GameObject.FindObjectOfType<NetworkHandlingManager>();

        // 유효성 검증
        AvailabilityCheck();

        // 헨들링 매니저에 등록
        m_NetworkHandlingManager.AppendListener(m_HandleUUID, this);
    }

    protected virtual void OnEnable()
    {
        // 헨들링 매니저에 활성화 요청
        m_NetworkHandlingManager.ActivateHandler(this);
    }

    protected virtual void OnDisable()
    {
        // 헨들링 매니저에 비활성화 요청
        m_NetworkHandlingManager.DeactivateHandler(this);
    }

    protected virtual void OnDestroy()
    {
        // 헨들링 매니저에 삭제
        m_NetworkHandlingManager.RemoveListener(this);
    }

    protected virtual void Update()
    {
        if (!m_Activate || m_UpdateTimeType == TrackingUpdateTimeType.None)
            return;

        if (
            m_UpdateTimeType == TrackingUpdateTimeType.First ||
            m_UpdateTimeType == TrackingUpdateTimeType.Nth ||
            m_UpdateTimeType == TrackingUpdateTimeType.Update
        )
            TrackingUpdate();
    }
    protected virtual void LateUpdate()
    {
        if (!m_Activate || m_UpdateTimeType == TrackingUpdateTimeType.None)
            return;

        if (m_UpdateTimeType == TrackingUpdateTimeType.LateUpdate)
            TrackingUpdate();
    }

    // TrackingExpression과 m_UpdateTimeType 둘다 참이여만 정보 송신
    private void TrackingUpdate()
    {
        if (!TrackingExpression()) // Ealiy-return
            return;

        if (
            m_UpdateTimeType == TrackingUpdateTimeType.First ||
            m_UpdateTimeType == TrackingUpdateTimeType.Nth
        ) {
            if (m_TrackingCount == 0) // Ealiy-return
                return;

            m_TrackingCount--;
        }

        // 업데이트 조건
        m_NetworkHandlingManager.Write(this, TargetObjectData());
    }

    private void AvailabilityCheck()
    {
        // 업데이트 타입이 업데이트 계열 함수를 사용한다면
        if(m_UpdateTimeType.isAffiliationUpdateType())
        {
            m_TrackingCount = -1;
        }

        // 업데이트 타입이 없다면
        if(m_UpdateTimeType == TrackingUpdateTimeType.None)
        {
            m_Activate = false;
        }
        else
        {
            m_Activate = true;
        }

        if(m_UpdateTimeType == TrackingUpdateTimeType.First)
        {
            m_TrackingCount = 1;
        }    
        else if (m_UpdateTimeType == TrackingUpdateTimeType.Nth && m_TrackingCount <= 0)
        {
            Debug.LogError("UpdateType이 Nth이면 Count는 0보다 작거나 같을 수 없습니다.");
            throw new System.Exception("Nth Event - OutOfRange");
        }

            // 이 정보를 받는 대상이 지정되어 있지 않다면.
        if (m_SubscriberType != TrackingSubscriberType.OnlyTarget)
        {
            m_SubscriberName = m_SubscriberType.ToString();
        }
        else
        {   // 없는 경우
            if (m_SubscriberName.Trim() == "")
            {
                Debug.LogError("보낼 대상이 존재하지 않습니다. 대상을 추가해주세요");
                throw new System.Exception("Not defined from send");
            }
            else if(m_SubscriberType == TrackingSubscriberType.OnlyServer)
            {
                Debug.LogError("Tracking(동기화) 대상은 서버가 될 수 없습니다.");
                throw new System.Exception("Not send tracking value from me to server");
            }
        }

        // m_HandleUUID가 없는 경우
        if (m_HandleUUID.Trim() == "")
        {
            Debug.LogError("ID가 존재하지 않습니다. ID를 추가해주세요");
            throw new System.Exception("Not defined tracking ID");
        }

        // m_HandleUUID 겹칠 경우 오류 처리
    }

    public void ReadHandle(string context)
    {
        ParseData(context);
    }
}

public static class NetworkHandlerExtension
{
    public static bool isAffiliationUpdateType(this TrackingUpdateTimeType type)
        => type == TrackingUpdateTimeType.Update || type == TrackingUpdateTimeType.LateUpdate;
    public static bool isAffiliationLinearType(this TrackingUpdateTimeType type)
        => type == TrackingUpdateTimeType.First || type == TrackingUpdateTimeType.Nth;
}
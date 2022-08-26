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
    [SerializeField, ReadOnly(true)] protected NetworkType m_isSender; // ������ ������� ����

    public TrackingSubscriberType SubscriberType { get => m_SubscriberType; }
    public string HandleUUID { get => m_HandleUUID; }
    public string SubscriberName { get => m_SubscriberName; }
    public NetworkType isSender { get => m_isSender; set => m_isSender = value; }

    /// <summary>
    /// �� �Լ��� ���� ���� ���� ������.
    /// { Client(Me) } => Net => Client(Other)
    /// �� �ڷ� ���� ������ �ִ��� ����
    /// </summary>
    /// <returns> Send Value </returns>
    protected abstract string TargetObjectData();
    /// <summary>
    /// �� �Լ��� ���� �ش� Ʈ��ŷ ������ ���� ��� ����
    /// </summary>
    /// <returns> True is Server Send </returns>
    protected abstract bool TrackingExpression();
    /// <summary>
    /// �� �Լ��� ���ؼ� ��Ʈ��ũ�� ���� ������ ������.
    /// Client(Other) => Net => { Client(Me) }
    /// �� �ڷ� ���� ������ �ִ��� �����ؼ� ��
    /// </summary>
    /// <param name="Context"></param>
    protected abstract void ParseData(string Context);

    protected virtual void Awake()
    {
        m_NetworkHandlingManager = GameObject.FindObjectOfType<NetworkHandlingManager>();

        // ��ȿ�� ����
        AvailabilityCheck();

        // ��鸵 �Ŵ����� ���
        m_NetworkHandlingManager.AppendListener(m_HandleUUID, this);
    }

    protected virtual void OnEnable()
    {
        // ��鸵 �Ŵ����� Ȱ��ȭ ��û
        m_NetworkHandlingManager.ActivateHandler(this);
    }

    protected virtual void OnDisable()
    {
        // ��鸵 �Ŵ����� ��Ȱ��ȭ ��û
        m_NetworkHandlingManager.DeactivateHandler(this);
    }

    protected virtual void OnDestroy()
    {
        // ��鸵 �Ŵ����� ����
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

    // TrackingExpression�� m_UpdateTimeType �Ѵ� ���̿��� ���� �۽�
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

        // ������Ʈ ����
        m_NetworkHandlingManager.Write(this, TargetObjectData());
    }

    private void AvailabilityCheck()
    {
        // ������Ʈ Ÿ���� ������Ʈ �迭 �Լ��� ����Ѵٸ�
        if(m_UpdateTimeType.isAffiliationUpdateType())
        {
            m_TrackingCount = -1;
        }

        // ������Ʈ Ÿ���� ���ٸ�
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
            Debug.LogError("UpdateType�� Nth�̸� Count�� 0���� �۰ų� ���� �� �����ϴ�.");
            throw new System.Exception("Nth Event - OutOfRange");
        }

            // �� ������ �޴� ����� �����Ǿ� ���� �ʴٸ�.
        if (m_SubscriberType != TrackingSubscriberType.OnlyTarget)
        {
            m_SubscriberName = m_SubscriberType.ToString();
        }
        else
        {   // ���� ���
            if (m_SubscriberName.Trim() == "")
            {
                Debug.LogError("���� ����� �������� �ʽ��ϴ�. ����� �߰����ּ���");
                throw new System.Exception("Not defined from send");
            }
            else if(m_SubscriberType == TrackingSubscriberType.OnlyServer)
            {
                Debug.LogError("Tracking(����ȭ) ����� ������ �� �� �����ϴ�.");
                throw new System.Exception("Not send tracking value from me to server");
            }
        }

        // m_HandleUUID�� ���� ���
        if (m_HandleUUID.Trim() == "")
        {
            Debug.LogError("ID�� �������� �ʽ��ϴ�. ID�� �߰����ּ���");
            throw new System.Exception("Not defined tracking ID");
        }

        // m_HandleUUID ��ĥ ��� ���� ó��
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
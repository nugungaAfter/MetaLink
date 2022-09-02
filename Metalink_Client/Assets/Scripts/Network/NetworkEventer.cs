using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 단향적으로 정보를 타겟으로 보냅니다.

public class NetworkEventer 
{
    [Header("ID")]
    [SerializeField, ReadOnly(true)] protected string m_HandleUUID;
    [Header("Subscriber")]
    [SerializeField, ReadOnly(true)] protected TrackingSubscriberType m_SubscriberType;
    [SerializeField, ReadOnly(true)] protected string m_SubscriberName;
    [Header("Connected")]
    [SerializeField, ReadOnly(true)] protected bool m_Activate;

}

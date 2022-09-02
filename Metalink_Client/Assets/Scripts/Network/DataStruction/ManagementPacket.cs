public struct ManagementPacket
{
    public TrackingSubscriberType m_SubscriberType; // 보낼 대상
    public string m_Context; // 보낼 데이터
    public string m_ExtendData; // 추가로 보내거나 받는 데이터

    public ManagementPacket(TrackingSubscriberType type, string context, string extendData ) {
        m_SubscriberType = type;
        m_Context = context;
        m_ExtendData = extendData;
    }
}

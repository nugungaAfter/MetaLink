
// 통신을 위한 자료형
public struct NetworkPacket
{
    public TrackingSubscriberType m_SubscriberType; // 구독자
    public string m_SubscriberName; // 대상이 있는 경우 IP로 적어야함.
    public string m_HandleUUID; // 통신 주소(ID)
    public string m_Context; // 보낼 데이터
    public NetworkType m_isSend;

    public NetworkPacket(TrackingSubscriberType type, string name, string uuid, string context, NetworkType isSend)
    {
        m_SubscriberType = type;
        m_SubscriberName = name;
        m_HandleUUID = uuid;
        m_Context = context;
        m_isSend = isSend;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adapter 클래스: Server 통신 클래스와 각 핸들러간의 통신을 중재
// 싱글톤 적용하려면 하셈
// 여기만 대해서 멀티 쓰레드 안정성을 보장하지 못함.

public class NetworkHandlingManager : MonoBehaviour
{
    private static Dictionary<string, (bool, NetworkHandler)> m_NetworkHandlers;
    private Queue<NetworkEventer> m_NetworkEventers;
    
    private void Awake()
    {
        m_NetworkHandlers = new Dictionary<string, (bool, NetworkHandler)>();
    }

    public void AppendListener(string UUID, NetworkHandler handler) {
        if(m_NetworkHandlers.ContainsKey(UUID))
        {
            Debug.LogError("해당 아이디를 가지고 있는 핸들이 존재합니다.");
            throw new System.Exception("Defined Handle ID");
        }
        m_NetworkHandlers.Add(UUID, (true, handler));
    }
    public void RemoveListener(NetworkHandler handler) {
        if(!m_NetworkHandlers.ContainsKey(handler.HandleUUID))
        {
            Debug.LogError("해당 아이디를 가지고 있는 핸들이 존재하지 않습니다.");
            throw new System.Exception("Not found Handle ID");
        }
        m_NetworkHandlers.Remove(handler.HandleUUID);
    }
    public void ActivateHandler(NetworkHandler handler)
    {
        if (!m_NetworkHandlers.ContainsKey(handler.HandleUUID))
        {
            Debug.LogError("해당 아이디를 가지고 있는 핸들이 존재하지 않습니다.");
            throw new System.Exception("Not found Handle ID");
        }
        m_NetworkHandlers[handler.HandleUUID] = (true, handler);
    }
    public void DeactivateHandler(NetworkHandler handler)
    {
        if (!m_NetworkHandlers.ContainsKey(handler.HandleUUID))
        {
            Debug.LogError("해당 아이디를 가지고 있는 핸들이 존재하지 않습니다.");
            throw new System.Exception("Not found Handle ID");
        }
        m_NetworkHandlers[handler.HandleUUID] = (false, handler);
    }

    public void Read(NetworkPacket context) {
        if (m_NetworkHandlers.ContainsKey(context.m_HandleUUID))
        {
            var handle = m_NetworkHandlers[context.m_HandleUUID];
            if (handle.Item1)
            {
                handle.Item2.ReadHandle(context.m_Context);
            }
        }
    }

    public void Write(NetworkHandler handle, string context) {
        NetworkManager.Instance.SendPacket(handle, context);
    }
}

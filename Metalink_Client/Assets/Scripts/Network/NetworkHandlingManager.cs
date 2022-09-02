using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adapter Ŭ����: Server ��� Ŭ������ �� �ڵ鷯���� ����� ����
// �̱��� �����Ϸ��� �ϼ�
// ���⸸ ���ؼ� ��Ƽ ������ �������� �������� ����.

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
            Debug.LogError("�ش� ���̵� ������ �ִ� �ڵ��� �����մϴ�.");
            throw new System.Exception("Defined Handle ID");
        }
        m_NetworkHandlers.Add(UUID, (true, handler));
    }
    public void RemoveListener(NetworkHandler handler) {
        if(!m_NetworkHandlers.ContainsKey(handler.HandleUUID))
        {
            Debug.LogError("�ش� ���̵� ������ �ִ� �ڵ��� �������� �ʽ��ϴ�.");
            throw new System.Exception("Not found Handle ID");
        }
        m_NetworkHandlers.Remove(handler.HandleUUID);
    }
    public void ActivateHandler(NetworkHandler handler)
    {
        if (!m_NetworkHandlers.ContainsKey(handler.HandleUUID))
        {
            Debug.LogError("�ش� ���̵� ������ �ִ� �ڵ��� �������� �ʽ��ϴ�.");
            throw new System.Exception("Not found Handle ID");
        }
        m_NetworkHandlers[handler.HandleUUID] = (true, handler);
    }
    public void DeactivateHandler(NetworkHandler handler)
    {
        if (!m_NetworkHandlers.ContainsKey(handler.HandleUUID))
        {
            Debug.LogError("�ش� ���̵� ������ �ִ� �ڵ��� �������� �ʽ��ϴ�.");
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

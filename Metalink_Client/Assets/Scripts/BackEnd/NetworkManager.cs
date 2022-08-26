using System.Net.Sockets;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Json;
using System.Text;
using System.IO;
using System;

// 서버와 통신만 하는 구체적인 역할 

public class NetworkManager : MonoBehaviour
{
    [ReadOnly] NetworkHandlingManager m_NetworkHandlingManager;
    [ReadOnly(true)] public string m_ServerIP;
    [ReadOnly(true)] public ushort m_ServerPORT;
    [ReadOnly(true)] public ushort m_ManagementServerPORT;
    [ReadOnly(true)] public bool m_isServerConnect = true;
    private static NetworkSession m_NowSession = null;

    private static NetworkManager m_Instance;
    private bool m_isServerOpen = false;
    private TcpClient m_Server;
    private NetworkStream m_ServerStream;
    private TcpClient m_ManagementServer;
    private NetworkStream m_ManagementStream;

    public NetworkStream ServerStream { get => m_ServerStream; }
    public NetworkStream ManagementStream { get => m_ManagementStream; }

    public bool ServerOpen
    {
        get => m_isServerOpen;
    }

    public static NetworkManager Instance { get => m_Instance == null ? null : m_Instance; }

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (!m_isServerConnect)
            return;

        m_NetworkHandlingManager = GameObject.FindObjectOfType<NetworkHandlingManager>();

        try
        {
            m_Server = new TcpClient(m_ServerIP, m_ServerPORT);
            m_ManagementServer = new TcpClient(m_ServerIP, m_ManagementServerPORT);

            m_ServerStream = m_Server.GetStream();
            m_ManagementStream = m_ManagementServer.GetStream();

            m_isServerOpen = true;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            m_isServerOpen = false;
        }

        m_NowSession?.Clear();
    }
    private void OnDestroy()
    {
        m_isServerOpen = false;
        if (!m_isServerConnect)
            return;

        if (!m_isServerOpen)
            return;

        m_ServerStream.Close();
        m_ManagementStream.Close();
        m_ManagementServer.Close();
        m_Server.Close();
    }

    public async void SendPacket(NetworkHandler Handle, string Context)
    {
        NetworkPacket packet = new NetworkPacket(Handle.SubscriberType, Handle.SubscriberName, Handle.HandleUUID, Context, Handle.isSender);
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(NetworkPacket));
        MemoryStream ms = new MemoryStream();
        serializer.WriteObject(ms, packet);

        var ary = UTF8Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(ms.ToArray()));
        Debug.Log("send" + Encoding.UTF8.GetString(ms.ToArray()));
        await WriteAsync(m_ServerStream, ary, 0, ary.Length);
    }
    public ManagementPacket SendCMDPacket(string command, string other = "") // command는 비공개함.
    {
        ManagementPacket sendPacket = new ManagementPacket(TrackingSubscriberType.OnlyServer, command, other);
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(NetworkPacket));
        MemoryStream ms = new MemoryStream();
        serializer.WriteObject(ms, sendPacket);

        byte[] buffer = new byte[1024 * 1024 * 8];
        var ary = UTF8Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(ms.ToArray()));
        Write(m_ManagementStream, ary, 0, ary.Length);
        Read(m_ManagementStream, buffer, 0, buffer.Length);

        ManagementPacket recvPacket = new ManagementPacket();
        ms = new MemoryStream(UTF8Encoding.UTF8.GetBytes(Encoding.Unicode.GetString(buffer)));
        recvPacket = (ManagementPacket)serializer.ReadObject(ms);
        ms.Close();

        return recvPacket;
    }
    public async void RecvCMDPacket()
    {
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(NetworkPacket));

        byte[] buffer = new byte[1024 * 1024 * 8];
        await ReadAsync(m_ManagementStream, buffer, 0, buffer.Length);

        ManagementPacket recvPacket = new ManagementPacket();
        MemoryStream ms = new MemoryStream(UTF8Encoding.UTF8.GetBytes(Encoding.Unicode.GetString(buffer)));
        recvPacket = (ManagementPacket)serializer.ReadObject(ms);
        ms.Close();

        // 받은 정보를 처리기로 넘김
    }

    public async void RecvPacket()
    {
        byte[] buffer = new byte[1024 * 1024 * 8];
        await ReadAsync(m_ServerStream, buffer, 0, buffer.Length);

        NetworkPacket packet = new NetworkPacket();
        MemoryStream ms = new MemoryStream(UTF8Encoding.UTF8.GetBytes(Encoding.Unicode.GetString(buffer)));
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(NetworkPacket));
        packet = (NetworkPacket)serializer.ReadObject(ms);
        ms.Close();

        m_NetworkHandlingManager.Read(packet);
    }

    public int Read(NetworkStream stream, byte[] buffer, int offset, int size)
    {
        if (!m_isServerOpen)
            return -1;
        return stream.Read(buffer, offset, size);
    }
    public async System.Threading.Tasks.Task<int> ReadAsync(NetworkStream stream, byte[] buffer, int offset, int size)
    {
        if (!m_isServerOpen)
            return -1;
        return await stream.ReadAsync(buffer, offset, size);
    }
    public void Write(NetworkStream stream, byte[] buffer, int offset, int size)
    {
        if (!m_isServerOpen)
            return; stream.Write(buffer, offset, size);
    }
    public async System.Threading.Tasks.Task WriteAsync(NetworkStream stream, byte[] buffer, int offset, int size)
    {
        if (!m_isServerOpen)
            return;
        await stream.WriteAsync(buffer, offset, size);
    }


    // 서버에 질의 하는 부분
    public static string Session2Ip(NetworkSession session)
    {
        return null;
    }
    public static NetworkSession Ip2Session(string ip)
    {
        return null;
    }


}

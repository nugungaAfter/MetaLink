using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSession
{
    // �÷��̾� �̸� -> �÷��̾� �ν��Ͻ�
    private Dictionary<string, Player> m_PlayerNameMap = new Dictionary<string, Player>();
    private string m_HostIP;
    private string m_OwnerPlayer;

    public Dictionary<string, Player> PlayerNameMap { get => m_PlayerNameMap; }
    public string HostIP { get => m_HostIP; }
    public string OwnerPlayer { get => m_OwnerPlayer; }
    public void Clear() => m_PlayerNameMap.Clear();
}

// �� ��ü�� �÷��̾ ����Ű�� ��ü�� ������ּ���.
// �� ����� �ʼ������� �־���մϴ�.
// �÷��̾� �ȿ��� ���� �÷��̾ �����ؾ��մϴ�. �ڱ� �ڽ��� �÷��̾ �����ؾ���.
public class Player
{
    public NetworkSession Session; // �ڽ��� ���� �ִ� ����
    public string IP; // �ڱ� �ڽ��� ������
    public static string PlayerName; // �ڱ� �ڽ��� �̸� �� ����� �ִٸ� �װɷ� �ٲٰ� NetworkManager.cs:SessionDestroy:ù��° ���ǹ��� �ٲټ���.
}
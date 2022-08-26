using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSession
{
    // 플레이어 이름 -> 플레이어 인스턴스
    private Dictionary<string, Player> m_PlayerNameMap = new Dictionary<string, Player>();
    private string m_HostIP;
    private string m_OwnerPlayer;

    public Dictionary<string, Player> PlayerNameMap { get => m_PlayerNameMap; }
    public string HostIP { get => m_HostIP; }
    public string OwnerPlayer { get => m_OwnerPlayer; }
    public void Clear() => m_PlayerNameMap.Clear();
}

// 이 객체는 플레이어를 가르키는 객체로 사용해주세요.
// 각 멤버는 필수적으로 있어야합니다.
// 플레이어 안에는 로컬 플레이어도 존재해야합니다. 자기 자신의 플레이어가 존재해야함.
public class Player
{
    public NetworkSession Session; // 자신이 속해 있는 세션
    public string IP; // 자기 자신의 아이피
    public static string PlayerName; // 자기 자신의 이름 이 멤버가 있다면 그걸로 바꾸고 NetworkManager.cs:SessionDestroy:첫번째 조건문을 바꾸세요.
}
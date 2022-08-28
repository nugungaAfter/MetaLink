using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mutiplay MatchMaking TextSetting", menuName = "Create New Mutiplay MatchMaking TextSetting", order = 25003)]
public class MutiplayMatchMakingTextSetting : ScriptableObject
{
    public string matchServerConnectionFailed; // 매치서버 접속에 실패함
    public string disconnectFromRemote; // 매칭 서버에서 비정상적인 이유로 클라이언트와의 연결을 강제로 끊은 경우
    public string noneMatchType; // 콘솔에서 생성하지 않은 매치 타입 & 매치 유형으로 매칭을 신청했을 때
    public string connectionTimeout; // 매치 서버와 클라이언트가 30초 이상 연결이 끊어진 경우
    public string getMatchListFailed; // 매치 리스트 조회에 실패했을경우
}

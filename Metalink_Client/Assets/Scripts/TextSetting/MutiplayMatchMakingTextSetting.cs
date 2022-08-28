using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mutiplay MatchMaking TextSetting", menuName = "Create New Mutiplay MatchMaking TextSetting", order = 25003)]
public class MutiplayMatchMakingTextSetting : ScriptableObject
{
    public string matchServerConnectionFailed; // ��ġ���� ���ӿ� ������
    public string disconnectFromRemote; // ��Ī �������� ���������� ������ Ŭ���̾�Ʈ���� ������ ������ ���� ���
    public string noneMatchType; // �ֿܼ��� �������� ���� ��ġ Ÿ�� & ��ġ �������� ��Ī�� ��û���� ��
    public string connectionTimeout; // ��ġ ������ Ŭ���̾�Ʈ�� 30�� �̻� ������ ������ ���
    public string getMatchListFailed; // ��ġ ����Ʈ ��ȸ�� �����������
}

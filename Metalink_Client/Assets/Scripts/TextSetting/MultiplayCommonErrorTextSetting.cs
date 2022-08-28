using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Multiplay CommonError TextSetting", menuName = "Create New Multiplay CommonError TextSetting", order = 25002)]
public class MultiplayCommonErrorTextSetting : ScriptableObject
{
    [Header("����")]
    public string error_OnMaintenance; // ������Ʈ ������
    public string error_OnTooManyRequest; // ������ ��û�� ������
    public string error_OnTooManyRequestByLocal; // ���� ������ ��û����
    public string error_OnOtherDeviceLoginDetected; // �ٸ� ��⿡�� �α��ε�
}

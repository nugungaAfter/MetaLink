using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Multiplay CommonError TextSetting", menuName = "Create New Multiplay CommonError TextSetting", order = 25002)]
public class MultiplayCommonErrorTextSetting : ScriptableObject
{
    [Header("에러")]
    public string error_OnMaintenance; // 프로젝트 점검중
    public string error_OnTooManyRequest; // 과도한 요청이 감지됨
    public string error_OnTooManyRequestByLocal; // 현제 과도한 요청중임
    public string error_OnOtherDeviceLoginDetected; // 다른 기기에서 로그인됨
}

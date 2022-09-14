using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UITextSetting", menuName = "Create New UITextSetting", order = 25001)]
public class AccountTextSetting : ScriptableObject
{
    [Header("���� - �α���")]
    public string account_SuccesLogin; // �α��� ����
    public string account_FailedLogin_NoneID; // �������� �ʴ� ���̵�
    public string account_FailedLogin_PasswordError; // ��й�ȣ�� Ʋ��
    public string account_FailedLogin_BlockedUser; // ���ܴ��� ����

    [Header("���� - ȸ������")]
    public string account_SuccesSignUp; // ȸ������ ����
    public string account_ConfirmPasswordError; // Ȯ�� �н����� �ٸ�
    public string account_FailedSignUp_DuplicatedID; //ID �ߺ�

    [Header("���� - ����")]
    public string account_DuplicatedNickName; // �ߺ��� �г���
    public string account_ErrorEmailAdress; // ���� �߸� �Էµ�

    [Space(10)]
    public string account_NoneInputNickName; // �̸� �Է� ����
    public string account_NoneInputEmailAdress; // ���� �Է� ����
    public string account_NoneInputPassword; // ��й�ȣ �Է� ����
}

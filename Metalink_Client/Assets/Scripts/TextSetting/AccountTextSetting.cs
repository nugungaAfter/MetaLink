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
    public string account_FailedLogin_GoneResource; // Ż������ ������ ���

    [Header("���� - ȸ������")]
    public string account_SuccesSignUp; // ȸ������ ����
    public string account_ConfirmPasswordError; // Ȯ�� �н����� �ٸ�
    public string account_FailedSignUp_DuplicatedID; //ID �ߺ�

    [Header("���� - ����")]
    public string account_DuplicatedNickName; // �ߺ��� �г���
    public string account_ErrorEmailAdress; // ���� �߸� �Էµ�
    public string account_ForbiddenActiveUser; // ��� ������ �׽�Ʈ�ε� AU�� 10�� �ʰ��� (�ڳ� API ����)

    [Space(10)]
    public string account_NoneInputNickName; // �̸� �Է� ����
    public string account_NoneInputEmailAdress; // ���� �Է� ����
    public string account_NoneInputPassword; // ��й�ȣ �Է� ����

    public string GetErrorMessage(string errorMessage, string errorCode = "")
    {
        if (errorMessage == "bad customId, �߸��� customId �Դϴ�") {
            return account_FailedLogin_NoneID;
        }

        if (errorMessage == "bad customPassword, �߸��� customPassword �Դϴ�") {
            return account_FailedLogin_PasswordError;
        }

        if (errorMessage == "forbidden blocked user, ������ blocked user �Դϴ�") {
            return account_FailedLogin_BlockedUser + "\n���� ����:" + errorCode;
        }

        if (errorMessage == "Gone user, ����� user �Դϴ�") {
            return account_FailedLogin_GoneResource;
        }

        if (errorMessage == "Duplicated customId, �ߺ��� customId �Դϴ�") {
            return account_FailedSignUp_DuplicatedID;
        }

        if (errorMessage == "Invalid email input") {
            return account_ErrorEmailAdress;
        }

        if (errorMessage == "Duplicated nickname, �ߺ��� nickname �Դϴ�") {
            return account_DuplicatedNickName;
        }

        return "�˼����� ����";
    }
}

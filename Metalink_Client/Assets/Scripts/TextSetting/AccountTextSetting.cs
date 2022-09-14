using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UITextSetting", menuName = "Create New UITextSetting", order = 25001)]
public class AccountTextSetting : ScriptableObject
{
    [Header("계정 - 로그인")]
    public string account_SuccesLogin; // 로그인 성공
    public string account_FailedLogin_NoneID; // 존재하지 않는 아이디
    public string account_FailedLogin_PasswordError; // 비밀번호가 틀림
    public string account_FailedLogin_BlockedUser; // 차단당한 유저

    [Header("계정 - 회원가입")]
    public string account_SuccesSignUp; // 회원가입 성공
    public string account_ConfirmPasswordError; // 확인 패스워드 다름
    public string account_FailedSignUp_DuplicatedID; //ID 중복

    [Header("계정 - 공통")]
    public string account_DuplicatedNickName; // 중복된 닉네임
    public string account_ErrorEmailAdress; // 계정 잘못 입력됨

    [Space(10)]
    public string account_NoneInputNickName; // 이름 입력 안함
    public string account_NoneInputEmailAdress; // 계정 입력 안함
    public string account_NoneInputPassword; // 비밀번호 입력 안함
}

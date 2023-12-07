using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using TMPro;

public class Login : LoginBase
{

    [SerializeField]
    private Image imageID;
    [SerializeField]
    private TMP_InputField inputFieldID;
    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private TMP_InputField inputFieldPW;

    [SerializeField]
    private Button btnLogin;
    /// <summary>
    /// "로그인"버튼을 눌렀을 때 호출
    /// </summary>
    public void OnClickLogin()
    {
        // 매개변수로 입력한 인풋필드 ui색상과 message 내용 초기화
        ResetUI(imageID, imagePW);
        //필드값 비어있나요
        if (IsFieldDataEmpty(imageID, inputFieldID.text, "아이디")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "비밀번호")) return;
        //로그인 연타금지
        btnLogin.interactable = false;
        //서버에 로그인 요청하는 동안 화면에 출력할 내용
        StartCoroutine(nameof(LoginProcess)) ;
        ResponseToLogin(inputFieldID.text, inputFieldPW.text);
    }
    /// <summary>
    /// 로그인 시도 후 서버로부터 전달받은 message를 기반으로 로직처리
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="PW"></param>
    private void ResponseToLogin(string ID, string PW)
    {
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
            StopCoroutine(nameof(LoginProcess));
            
            if(callback.IsSuccess())
            {
                SetMessage($"{inputFieldID.text}님 환영합니다.");
            }
            else
            {
                //로그인에 실패했을 대는 다시 로그인 해야하기때문에 버튼 살리기
                btnLogin.interactable = true;
                string message = string.Empty;
                switch(int.Parse(callback.GetStatusCode()))
                {
                    case 401: //존재하지 않는 아이디, 잘못된 비번
                        message = callback.GetMessage().Contains("customId") ? "존재하지 않는 아이디입니다." : "잘못된 비밀번호 입니다.";
                        break;
                    case 403: // 유저 or 디바이스 차단
                        message = callback.GetMessage().Contains("user") ? "차단당한 유저입니다." : "차단당한 디바이스입니다.";
                        break;
                    case 410: //탈퇴 진행중
                        message = "탈퇴가 진행중인 유저입니다.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }
                //statusCode 401에서 "잘못된 비밀번호 입니다."일 때
                if(message.Contains("비밀번호"))
                {
                    GuideForIncorrectlyEnteredData(imagePW, message);
                }
                else
                {
                    GuideForIncorrectlyEnteredData(imageID, message);
                }
            }
        });
    }
    private IEnumerator LoginProcess()
    {
        float time = 0;
        while (true) 
        {
            time += Time.deltaTime;
            SetMessage($"로그인 중입니다...{time:F1}");
            yield return null;
        }
    }
}

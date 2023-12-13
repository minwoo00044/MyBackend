using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using TMPro;

public class RegisterAccount : LoginBase
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
    private Image imageConfirmPW;
    [SerializeField]
    private TMP_InputField inputFieldConfirmPW;
    [SerializeField]
    private Image imageEmail;
    [SerializeField]
    private TMP_InputField inputFieldEmail;

    [SerializeField]
    private Button btnRegisterAccount;

    /// <summary>
    /// 계정생성
    /// </summary>
    public void OnClickRegisterAccount()
    {
        //
        ResetUI(imageID, imagePW, imageConfirmPW, imageEmail);
        //필드 비어있?
        if (IsFieldDataEmpty(imageID, inputFieldID.text, "아이디")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "아이디")) return;
        if (IsFieldDataEmpty(imageConfirmPW, inputFieldConfirmPW.text, "아이디")) return;
        if (IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "아이디")) return;

        //비밀번호와 비밀번호 확인의 내용이 다를 대
        if (!inputFieldPW.text.Equals(inputFieldConfirmPW.text))
        {
            GuideForIncorrectlyEnteredData(imageConfirmPW, "비밀번호가 일치하지 않습니다.");
            return;
        }
        //메일형식아님
        if(!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "메일 형식이 잘못되었습니다.");
            return;
        }
        //계정생성 버튼 상호작용잠구기
        btnRegisterAccount.interactable = false;
        SetMessage("계정 생성중입니다.");
        //뒤끝 서버 계정 생성시도
        CustomSignUp();
    }
    /// <summary>
    /// 계정 생성 시도 후 서버로부터 전달받은 메시지 기반으로 로직 처리
    /// </summary>
    private void CustomSignUp()
    {
        Backend.BMember.CustomSignUp(inputFieldID.text, inputFieldPW.text, callback =>
        {
            btnRegisterAccount.interactable = true;
            if(callback.IsSuccess())
            {
                Backend.BMember.UpdateCustomEmail(inputFieldEmail.text, callback =>
                {
                    if(callback.IsSuccess())
                    {
                        SetMessage($"계정 생성 성공.{inputFieldID.text}님 환영합니다.");

                        Utils.LoadScene(SceneName.Lobby);
                    }
                });
            }
            else
            {
                string message = "";
                switch(int.Parse(callback.GetStatusCode()))
                {
                    case 409://중복된 customId가 존재하는 경우
                        message = "이미 존재하는 아이디입니다.";
                        break;
                    case 403: //차단당한 디바이스
                    case 401: // 프로젝트 상태가 점검
                    case 400: // 디바이스 정보가 NULL
                    default:
                        message = callback.GetMessage();
                        break;
                }
                if(message.Contains("아이디"))
                {
                    GuideForIncorrectlyEnteredData(imageID, message);
                }
                else
                {
                    SetMessage(message);
                }
            }
        });
    }
}

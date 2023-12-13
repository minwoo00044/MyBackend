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
    /// ��������
    /// </summary>
    public void OnClickRegisterAccount()
    {
        //
        ResetUI(imageID, imagePW, imageConfirmPW, imageEmail);
        //�ʵ� �����?
        if (IsFieldDataEmpty(imageID, inputFieldID.text, "���̵�")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "���̵�")) return;
        if (IsFieldDataEmpty(imageConfirmPW, inputFieldConfirmPW.text, "���̵�")) return;
        if (IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "���̵�")) return;

        //��й�ȣ�� ��й�ȣ Ȯ���� ������ �ٸ� ��
        if (!inputFieldPW.text.Equals(inputFieldConfirmPW.text))
        {
            GuideForIncorrectlyEnteredData(imageConfirmPW, "��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return;
        }
        //�������ľƴ�
        if(!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "���� ������ �߸��Ǿ����ϴ�.");
            return;
        }
        //�������� ��ư ��ȣ�ۿ��ᱸ��
        btnRegisterAccount.interactable = false;
        SetMessage("���� �������Դϴ�.");
        //�ڳ� ���� ���� �����õ�
        CustomSignUp();
    }
    /// <summary>
    /// ���� ���� �õ� �� �����κ��� ���޹��� �޽��� ������� ���� ó��
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
                        SetMessage($"���� ���� ����.{inputFieldID.text}�� ȯ���մϴ�.");

                        Utils.LoadScene(SceneName.Lobby);
                    }
                });
            }
            else
            {
                string message = "";
                switch(int.Parse(callback.GetStatusCode()))
                {
                    case 409://�ߺ��� customId�� �����ϴ� ���
                        message = "�̹� �����ϴ� ���̵��Դϴ�.";
                        break;
                    case 403: //���ܴ��� ����̽�
                    case 401: // ������Ʈ ���°� ����
                    case 400: // ����̽� ������ NULL
                    default:
                        message = callback.GetMessage();
                        break;
                }
                if(message.Contains("���̵�"))
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

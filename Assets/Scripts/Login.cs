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
    /// "�α���"��ư�� ������ �� ȣ��
    /// </summary>
    public void OnClickLogin()
    {
        // �Ű������� �Է��� ��ǲ�ʵ� ui����� message ���� �ʱ�ȭ
        ResetUI(imageID, imagePW);
        //�ʵ尪 ����ֳ���
        if (IsFieldDataEmpty(imageID, inputFieldID.text, "���̵�")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "��й�ȣ")) return;
        //�α��� ��Ÿ����
        btnLogin.interactable = false;
        //������ �α��� ��û�ϴ� ���� ȭ�鿡 ����� ����
        StartCoroutine(nameof(LoginProcess)) ;
        ResponseToLogin(inputFieldID.text, inputFieldPW.text);
    }
    /// <summary>
    /// �α��� �õ� �� �����κ��� ���޹��� message�� ������� ����ó��
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
                SetMessage($"{inputFieldID.text}�� ȯ���մϴ�.");
            }
            else
            {
                //�α��ο� �������� ��� �ٽ� �α��� �ؾ��ϱ⶧���� ��ư �츮��
                btnLogin.interactable = true;
                string message = string.Empty;
                switch(int.Parse(callback.GetStatusCode()))
                {
                    case 401: //�������� �ʴ� ���̵�, �߸��� ���
                        message = callback.GetMessage().Contains("customId") ? "�������� �ʴ� ���̵��Դϴ�." : "�߸��� ��й�ȣ �Դϴ�.";
                        break;
                    case 403: // ���� or ����̽� ����
                        message = callback.GetMessage().Contains("user") ? "���ܴ��� �����Դϴ�." : "���ܴ��� ����̽��Դϴ�.";
                        break;
                    case 410: //Ż�� ������
                        message = "Ż�� �������� �����Դϴ�.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }
                //statusCode 401���� "�߸��� ��й�ȣ �Դϴ�."�� ��
                if(message.Contains("��й�ȣ"))
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
            SetMessage($"�α��� ���Դϴ�...{time:F1}");
            yield return null;
        }
    }
}

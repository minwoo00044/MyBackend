using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FindID : LoginBase
{
    [SerializeField]
    private Image imageEmail;
    [SerializeField]
    private TMP_InputField inputFieldEmail;

    [SerializeField]
    private Button btnFindID;

    public void OnClickFindID()
    {
        //�Ű������� �Է��� ��ǲ�ʵ� ����� �޽��� �ʱ�ȭ
        ResetUI(imageEmail);
        if (IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "���� �ּ�")) return;
        if (!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "���� ������ �߸� �Ǿ����ϴ�.");
            return;
        }
        //���̵� ã�� ��ư ��Ȱ��ȭ
        btnFindID.interactable = false;
        SetMessage("���� ã�����Դϴ�.");
        FindCustomID();
    }
    private void FindCustomID()
    {
        //���̵� ������ �̸��Ϸ� �߼�
        Backend.BMember.FindCustomID(inputFieldEmail.text, callback =>
        {
            //"���̵� ã��" ��ư ��ȣ�ۿ� Ȱ��ȭ
            btnFindID.interactable = true;
            //���� �߼� ����
            if(callback.IsSuccess())
            {
                SetMessage($"{inputFieldEmail.text} �ּҷ� ������ �߼��Ͽ����ϴ�.");
            }
            else
            {
                string message = string.Empty;
                switch(int.Parse(callback.GetStatusCode()))
                {
                    case 404:
                        message = "�ش� �̸����� ����ϴ� ����ڰ� �����ϴ�.";
                        break;
                    case 429: //24�ð� �̳��� 5ȸ �̻� ���� �̸��� ������ ���̵�/��й�ȣ ã�⸦ �õ��� ���
                        message = "24�ð� �̳��� 5ȸ �̻� ���̵�/��й�ȣ ã�⸦ �õ��߽��ϴ�.";
                        break;
                    default:
                        //statusCode : 400 => ������Ʈ�� Ư�����ڰ� �߰��� ���(�ȳ� ���� �̹߼� �� ���� �߻�)
                        message = callback.GetMessage();
                        break;

                }
                if(message.Contains("�̸���"))
                {
                    GuideForIncorrectlyEnteredData(imageEmail, message);
                }
                else
                {
                    SetMessage(message);
                }
            }
        });
    }
}

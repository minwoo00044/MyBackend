using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    [System.Serializable]
    public class UserInfoEvent : UnityEngine.Events.UnityEvent { }
    public UserInfoEvent onUseInfoEvent = new UserInfoEvent();

    private static UserInfoData data = new UserInfoData();
    public static UserInfoData Data => data;

    public void GetUserInfoFromBackend()
    {
        Backend.BMember.GetUserInfo(callback =>
        {
            //����
            if(callback.IsSuccess())
            {
                try 
                {
                    JsonData json = callback.GetReturnValuetoJSON()["row"];

                    data.gamerId = json["gamerId"].ToString();
                    data.countryCode = json["countryCode"]?.ToString();
                    data.nickName = json["nickname"]?.ToString();
                    data.inDate = json["inData"].ToString();
                    data.emailForFindPassword = json["emailForFindPassword"].ToString();
                    data.subscriptionType = json["subscriptionType"].ToString();
                    data.federationId = json["federationId"]?.ToString();
                }
                catch(System.Exception e)
                {
                    data.Reset();
                    Debug.LogError(e);
                }

            }
            //����
            else
            {
                //�������� ���� ���, �⺻���� ����
                data.Reset();
                Debug.LogError(callback.GetMessage());
            }
            //���� ���� �ҷ����� �Ϸ������� onUserInfoEvent�� ��� �� �ִ� �̺�Ʈ �޼ҵ� ȣ��
            onUseInfoEvent?.Invoke();
        });
    }

}
public class UserInfoData
{
    public string gamerId; // ���� �Ƶ�
    public string countryCode; // ���� �ڵ� ���� �� ������ ��
    public string nickName; // �г���, ���� �� ������ ��
    public string inDate; //������ inData
    public string emailForFindPassword; // �̸��� �ּ�, ���� �� ������ ��
    public string subscriptionType; // Ŀ����, ������̼� Ÿ��
    public string federationId; // ���� , ����, ���̽���, ������̼� id, Ŀ���� ������ ��

    public void Reset()
    {
        gamerId = "offline";
        countryCode = "Unknown";
        nickName = "Noname";
        inDate = string.Empty;
        emailForFindPassword = string.Empty;
        subscriptionType = string.Empty;
        federationId = string.Empty;
    }
}

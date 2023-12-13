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
            //성공
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
            //실패
            else
            {
                //오프라인 상태 대비, 기본정보 저장
                data.Reset();
                Debug.LogError(callback.GetMessage());
            }
            //유저 정보 불러오기 완료했을때 onUserInfoEvent에 등록 돼 있는 이벤트 메소드 호출
            onUseInfoEvent?.Invoke();
        });
    }

}
public class UserInfoData
{
    public string gamerId; // 유저 아디
    public string countryCode; // 국가 코드 설정 안 했으면 널
    public string nickName; // 닉네임, 설정 안 했으면 널
    public string inDate; //유저의 inData
    public string emailForFindPassword; // 이메일 주소, 설정 안 했으면 널
    public string subscriptionType; // 커스템, 페더레이션 타입
    public string federationId; // 구글 , 애플, 페이스북, 페더레이션 id, 커스텀 계정은 널

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

using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackendManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        BackendSetup();
    }

    private void Update()
    {
        //서버의 비동기 메소드 호출(콜백함수풀링)을 위해 작성
        if(Backend.IsInitialized)
        {
            Backend.AsyncPoll();
        }
    }

    private void BackendSetup()
    {
        var bro = Backend.Initialize(true);

        if (bro.IsSuccess()) 
        {
            Debug.Log($"초기화 성공 : {bro}");
        }
        else
        {
            Debug.LogError($"초기화 실패 : {bro}");
        }
    }
}

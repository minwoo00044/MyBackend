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
        //������ �񵿱� �޼ҵ� ȣ��(�ݹ��Լ�Ǯ��)�� ���� �ۼ�
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
            Debug.Log($"�ʱ�ȭ ���� : {bro}");
        }
        else
        {
            Debug.LogError($"�ʱ�ȭ ���� : {bro}");
        }
    }
}

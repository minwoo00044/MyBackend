using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginBase : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMessage;
    /// <summary>
    /// 메시지 내용, 인풋필드 색상 초기화
    /// </summary>
    /// <param name="images"></param>
    protected void ResetUI(params Image[] images)
    {
        textMessage.text = string.Empty;
        for(int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.white;
        }
    }
    /// <summary>
    /// 매개변수의 내용을 출력
    /// </summary>
    /// <param name="msg"></param>
    protected void SetMessage(string msg)
    {
        textMessage.text = msg;
    }
    /// <summary>
    /// 입력 오류가 있는 인풋 필드 색상변경
    /// 오류메시지 출력
    /// </summary>
    /// <param name="image"></param>
    /// <param name="msg"></param>
    protected void GuideForIncorrectlyEnteredData(Image image, string msg)
    {
        textMessage.text = msg;
        image.color = Color.red;
    }/// <summary>
    /// 필드값이 비어있는지 확인(image : 필드, field: 내용, result : 출력될 내용)
    /// </summary>
    /// <param name="image"></param>
    /// <param name="field"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected bool IsFieldDataEmpty(Image image, string field, string result)
    {
        if(field.Trim().Equals(""))
        {
            GuideForIncorrectlyEnteredData(image, $"\"{result}\" 필드를 채워주세요.");
            return true;
        }
        return false;
    }
}

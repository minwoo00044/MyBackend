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
    /// �޽��� ����, ��ǲ�ʵ� ���� �ʱ�ȭ
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
    /// �Ű������� ������ ���
    /// </summary>
    /// <param name="msg"></param>
    protected void SetMessage(string msg)
    {
        textMessage.text = msg;
    }
    /// <summary>
    /// �Է� ������ �ִ� ��ǲ �ʵ� ���󺯰�
    /// �����޽��� ���
    /// </summary>
    /// <param name="image"></param>
    /// <param name="msg"></param>
    protected void GuideForIncorrectlyEnteredData(Image image, string msg)
    {
        textMessage.text = msg;
        image.color = Color.red;
    }/// <summary>
    /// �ʵ尪�� ����ִ��� Ȯ��(image : �ʵ�, field: ����, result : ��µ� ����)
    /// </summary>
    /// <param name="image"></param>
    /// <param name="field"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected bool IsFieldDataEmpty(Image image, string field, string result)
    {
        if(field.Trim().Equals(""))
        {
            GuideForIncorrectlyEnteredData(image, $"\"{result}\" �ʵ带 ä���ּ���.");
            return true;
        }
        return false;
    }
}

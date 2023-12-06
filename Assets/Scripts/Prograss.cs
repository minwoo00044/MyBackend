using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

public class Prograss : MonoBehaviour
{
    [SerializeField]
    private Slider slderProgress;
    [SerializeField]
    private TextMeshProUGUI textPrograssData;
    [SerializeField]
    private float progressTime;

    public void Play(UnityAction action = null)
    {
        StartCoroutine(OnProgress(action));
    }
    private IEnumerator OnProgress(UnityAction action)
    {
        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / progressTime;

            textPrograssData.text = $"Now Loading... {slderProgress.value * 100:F0}%";
            slderProgress.value = Mathf.Lerp(0, 1, percent);

            yield return null;
        }
        action?.Invoke();
    }
}

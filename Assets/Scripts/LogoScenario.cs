using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScenario : MonoBehaviour
{
    [SerializeField]
    private Prograss prograss;
    [SerializeField]
    private SceneName nextScene;
    private void Awake()
    {
        SystemSetup();
    }

    private void SystemSetup()
    {
        Application.runInBackground = true;
        int width = Screen.width;
        int height = (int)(Screen.width * 18.5f /9);
        Screen.SetResolution(width, height, true);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        prograss.Play(OnAfterProgress);
    }
    private void OnAfterProgress()
    {
        Utils.LoadScene(nextScene);
    }
}

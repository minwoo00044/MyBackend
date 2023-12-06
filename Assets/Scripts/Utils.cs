using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    Logo = 0,
    Login,
}
public class Utils : MonoBehaviour
{
    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    public static void LoadScene(string sceneName ="")
    {
        if(sceneName == "")
        {
            SceneManager.LoadScene(GetActiveScene());
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    public static void LoadScene(SceneName scneneName)
    {
        SceneManager.LoadScene(scneneName.ToString());
    }
}

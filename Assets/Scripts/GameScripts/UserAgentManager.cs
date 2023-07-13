using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UserAgentManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool GetUserDevice();

    [DllImport("__Internal")]
    private static extern string GetLang();

    public bool isPC;
    public string language;

    public static UserAgentManager Instance;
    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
#if !UNITY_EDITOR && UNITY_WEBGL
            isPC = GetUserDevice();           
            language = GetLang();
#endif
        }
        else
            Destroy(gameObject);

        Debug.Log($"PC: {isPC};" + $" Language: {language}");
    }
}

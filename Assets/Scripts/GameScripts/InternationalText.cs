using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InternationalText : MonoBehaviour
{
    [SerializeField] string _en;
    [SerializeField] string _ru;

    [SerializeField] Font ruFont;

    private void Start()
    {
        if (GetComponent<Text>())
        {
            if (UserAgentManager.Instance.language == "en")
            {
                GetComponent<Text>().text = _en;
            }
            else if (UserAgentManager.Instance.language == "ru")
            {
                GetComponent<Text>().text = _ru;
                GetComponent<Text>().font = ruFont;
            }
            else
                GetComponent<Text>().text = _en;
        }
        if (GetComponent<Image>())
        {
            if (UserAgentManager.Instance.language == "ru")
            {
                GetComponent<Image>().enabled = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel, lightFlare, UIPanel;
    [SerializeField] SessionManager sessionManager;
    [SerializeField] RewardManager reward;

    private bool settingsOpen = false;

    private void Start()
    {
        Time.timeScale = 1;
        Debug.Log(Cursor.lockState);
    }
    public void PlayButton()
    {
        if ((PlayerPrefs.HasKey("first?")))
        {
            //Cursor.lockState = CursorLockMode.Locked;
            reward.AddShow();
            SceneManager.LoadScene(1);
        }
        else
            sessionManager.FirstLaunch();
    }
    public void ToMenu()
    {
        reward.AddShow();
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Settings()
    {
        reward.AddShow();
        settingsPanel.SetActive(true);
        lightFlare.SetActive(false);
        UIPanel.SetActive(false);
        settingsOpen = true;
        Time.timeScale = 0;

        if (FindObjectOfType<EnemyScript>())
        {
            Cursor.lockState = CursorLockMode.Confined;
            FindObjectOfType<EnemyScript>().GetComponent<AudioSource>().volume = 0;
        }
    }
    public void CloseSettings()
    {
        reward.AddShow();
        settingsPanel.SetActive(false);
        lightFlare.SetActive(true);
        UIPanel.SetActive(true);
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (FindObjectOfType<MouseController>())
            {
                FindObjectOfType<MouseController>().SetSensitivity();
            }
            settingsOpen = false;
        }
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!settingsOpen)
                    Settings();
                else
                    CloseSettings();
            }
        }
    }
}

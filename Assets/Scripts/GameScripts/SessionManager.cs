using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SessionManager : MonoBehaviour
{
    public Image panel;
    public Text mane, timeText;
    public float timer = 0;
    public float duration = 1.5f;
    private float bestTime;

    public EnemyScript snail;

    public Text[] textLst;
    private void Start()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetInt("BestTime");
            timeText.text = bestTime.ToString() + " sec";
        }
        else
        {
            bestTime = 0;
        }
        
    }
    public void FirstLaunch()
    {
        Cursor.lockState = CursorLockMode.Locked;
        panel.gameObject.SetActive(true);
        StartCoroutine(StartTextAnimation());
    }
    public void Win()
    {
        int time = Mathf.RoundToInt(timer);
        snail.Staned();
        panel.enabled = true;
        mane.enabled = true;
        timeText.enabled = true;
        timeText.text = time.ToString("0") + " sec";
        Debug.Log("Win()");
        if (timer < bestTime | bestTime == 0)
        {
            GetComponent<RewardManager>().TimeSet(time);
            PlayerPrefs.SetInt("BestTime", time);
        }
        snail.StopGame(7);
        StartCoroutine(WinAnimation());
    }
    void Update()
    {
        timer += Time.deltaTime;
    }
    IEnumerator StartTextAnimation()
    {
        Tweener tweener = textLst[0].DOFade(1f, duration);
        yield return tweener.WaitForCompletion(); // Ждем завершения анимации
        yield return new WaitForSeconds(1f);
        tweener = textLst[1].DOFade(1f, duration);
        yield return tweener.WaitForCompletion();
        textLst[0].DOFade(0, duration);
        tweener = textLst[1].DOFade(0, duration);
        yield return tweener.WaitForCompletion();// Ждем завершения анимации
        yield return new WaitForSeconds(0.3f);
        tweener = textLst[2].DOFade(1f, duration);
        yield return tweener.WaitForCompletion();
        yield return new WaitForSeconds(1f);
        tweener = textLst[2].DOFade(0f, duration);
        yield return tweener.WaitForCompletion();// Ждем завершения анимации
        tweener = textLst[3].DOFade(1f, duration);
        yield return tweener.WaitForCompletion();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
    IEnumerator WinAnimation()
    {
        Tweener tweener = panel.DOFade(1f, duration); // Анимация изменения alpha с 0 до 1
        yield return tweener.WaitForCompletion(); // Ждем завершения анимации
        yield return new WaitForSeconds(0.8f); // Задержка перед следующей строкой
        tweener = mane.DOFade(1f, duration); // Анимация изменения alpha с 0 до 1
        yield return tweener.WaitForCompletion(); // Ждем завершения анимации
        yield return new WaitForSeconds(0.4f); // Задержка перед следующей строкой
        tweener = timeText.DOFade(1f, duration); // Анимация изменения alpha с 0 до 1
        yield return tweener.WaitForCompletion(); // Ждем завершения анимации
    }
}

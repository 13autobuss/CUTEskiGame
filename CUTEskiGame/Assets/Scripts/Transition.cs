using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [SerializeField] private Image overlay;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private float fadeDuration;
    [SerializeField] private int nextLevelIndex;

    private void Start()
    {
        gameOverMenu.SetActive(false);
        StartCoroutine(FadeIn());
    }
    
    private void OnEnable()
    {
        GameEvents.raceEnd += EnableGameOver;
        GameEvents.QuitGame += Exit;
    }

    private void OnDisable()
    {
        GameEvents.raceEnd -= EnableGameOver;
        GameEvents.QuitGame -= Exit;
    }

    private void EnableGameOver()
    {
        gameOverMenu.SetActive(true);
    }

    public void QuitButton()
    {
        GameEvents.CallQuit();
    }
    
    public void RestartLevel()
    {
        StartCoroutine(RestartCoroutine());
    }

    public void NextLevel()
    {
        
    }

    public void Exit()
    {
        StartCoroutine(QuitCoroutine());
    }
    
    private IEnumerator FadeIn()
    {
        overlay.CrossFadeAlpha(0,fadeDuration,true);
        yield return new WaitForSeconds(fadeDuration);
    }

    private IEnumerator RestartCoroutine()
    {
        overlay.CrossFadeAlpha(1f,1f,true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator NextLevelCoroutine()
    {
        overlay.CrossFadeAlpha(1f,fadeDuration,true);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(nextLevelIndex);
    }

    private IEnumerator QuitCoroutine()
    {
        overlay.CrossFadeAlpha(1f,fadeDuration,true);
        yield return new WaitForSeconds(fadeDuration);
        Application.Quit();
    }
}

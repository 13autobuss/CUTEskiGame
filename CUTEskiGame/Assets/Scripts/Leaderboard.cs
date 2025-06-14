using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<float> Besttimes = new();
    [SerializeField] private TextMeshProUGUI[] scoreText;

    private void Awake()
    {
        Besttimes.Clear();
        for (int i  = 0; i < scoreText.Length; i++)
        {
           Besttimes.Add(PlayerPrefs.GetFloat("time: " + i, 9999999));
        }
    }

    public void AddTime(float time)
    {
        Besttimes.Add(time);
        Besttimes.Sort();
        SaveData();
    }

    public void ShowScore()
    {
        for (int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].text = i + 1 +". Place :" + Besttimes[i].ToString("F3");
        }
    }

    private void OnEnable()
    {
        GameEvents.raceEnd += ShowScore;
    }

    private void OnDisable()
    {
        GameEvents.raceEnd -= ShowScore;
    }

    private void SaveData()
    {
        for (int i = 0; i < scoreText.Length; i++)
        {
            if (i<Besttimes.Count)
            {
                PlayerPrefs.SetFloat("time: " +i, Besttimes[i]); 
            }
        }
        PlayerPrefs.Save();
    }
    
}

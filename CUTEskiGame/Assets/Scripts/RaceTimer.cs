using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private float penaltyTime;
    [SerializeField] private Leaderboard leaderboard;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip wrong;
    
    private bool timeRunning = false;
    private float time;

    private void Start()
    {
        timer.text = "";
    }

    private void Update()
    {
        if (timeRunning)
        {
            time += Time.deltaTime;
            timer.text = time.ToString("F2");
        }
    }

    private void Penalty()
    {
        source.PlayOneShot(wrong);
        time += penaltyTime;
    }

    private void StartRace()
    {
        time = 0;
        timeRunning = true;
    }

    private void EndRace()
    {
        timeRunning = false;
        leaderboard.AddTime(time);
        GameData.Instance.racesCompleted++;
        Debug.Log(GameData.Instance.racesCompleted);
        Debug.Log("race time: " + time);
    }

    private void OnEnable()
    {
        GameEvents.startRace += StartRace;
        GameEvents.racePenalty += Penalty;
        GameEvents.raceEnd += EndRace;
    }

    private void OnDisable()
    {
        GameEvents.startRace -= StartRace;
        GameEvents.racePenalty -= Penalty;
        GameEvents.raceEnd -= EndRace;
    }
}

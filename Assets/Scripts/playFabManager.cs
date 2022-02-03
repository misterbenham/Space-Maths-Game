using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class playFabManager : MonoBehaviour
{
    public GameObject finalPanel;
    void Awake()
    {
        finalPanel.SetActive(false);
    }
    public void SendLeaderboardHighScoreAllTime(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successufull leaderboard sent");
        finalPanel.SetActive(true);
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating accunt !");
        Debug.Log(error.GenerateErrorReport());
    }
}

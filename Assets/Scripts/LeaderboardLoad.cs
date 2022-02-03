using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class LeaderboardLoad : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowsParent;
    void Awake()
    {
        GetLeaderboardHighScoreAllTime();
    }
    public void GetLeaderboardHighScoreAllTime()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScore",
            StartPosition = 0,
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardHighScoreAllTimeGet, OnError);
    }
    void OnLeaderboardHighScoreAllTimeGet(GetLeaderboardResult result)
    {
        Debug.Log("Leaderboard scores received");
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
            if (item.DisplayName == nameManager.name)
            {
                texts[0].fontStyle = FontStyle.Bold;
                texts[1].fontStyle = FontStyle.Bold;
                texts[0].color = Color.green;
                texts[1].color = Color.green;
            }
            if (item.Position == 0)
            {
                texts[0].color = Color.yellow;
            }
            if (item.Position == 1)
            {
                texts[0].color = Color.grey;
            }
            if (item.Position == 2)
            {
                texts[0].color = new Color(140 / 255f, 90 / 255f, 36 / 255f);
            }
        }
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account !");
        Debug.Log(error.GenerateErrorReport());
    }
}

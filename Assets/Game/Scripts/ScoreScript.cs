using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
public class ScoreScript : MonoBehaviour
{
    private GameManager gameManager;

    void Awake()
    {
        
    }
    public void UpdateScore(float finalTime)
    {
        float multTime = finalTime * 100;
        int intTime = Mathf.RoundToInt(multTime);
        gameManager ??= GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Best Time",
                    Value = intTime
                }   
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateSuccess, OnUpdateError);
    }
    void OnUpdateSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Score updated successfully.");
    }
    void OnUpdateError(PlayFabError error)
    {
        Debug.Log("PlayFab UpdatePlayerStatistics failed: " + error.GenerateErrorReport());
    }
}

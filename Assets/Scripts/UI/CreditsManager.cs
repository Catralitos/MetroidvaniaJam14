using System;
using GameManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    public Button backToTitle;

    public TextMeshProUGUI completionText;

    // Start is called before the first frame update
    private void Start()
    {
        backToTitle.onClick.AddListener(ReturnToTitle);
        float completionPercentage =
            100f * (GameManager.Instance.lastCollectedItems / GameManager.Instance.lastMaxItems);
        completionText.text = "Completion Time: " + FormatTime(GameManager.Instance.lastRecordedTime) + "\nCompletion %: " +
                              completionPercentage;
    }

    private void ReturnToTitle()
    {
        GameManager.Instance.LoadTitleScreen();
    }
    
    private string FormatTime (float time){
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = String.Format ("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        return timeText;
    }
}
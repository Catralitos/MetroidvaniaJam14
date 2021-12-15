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
        completionText.text = "Completion Time: " + GameManager.Instance.lastRecordedTime + "\nCompletion %: " +
                              completionPercentage;
    }

    private void ReturnToTitle()
    {
        GameManager.Instance.LoadTitleScreen();
    }
}
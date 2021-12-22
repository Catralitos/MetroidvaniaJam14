using System.IO;
using Audio;
using GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TitleScreenManager : MonoBehaviour
    {
        [Header("Screens")] public GameObject titleScreen;

        [Header("TitleScreen")] public Button newGameButton, loadGameButton, exitButton;
        
        private void Start()
        {
            newGameButton.onClick.AddListener(StartGame);
            if (File.Exists(GameManager.Instance.savePath)){
                loadGameButton.onClick.AddListener(LoadGame);
            } else
            {
                loadGameButton.GetComponent<Image>().color = Color.gray;
            }
            exitButton.onClick.AddListener(ExitGame);
        }
    
        private void StartGame()
        {
            SaveSystem.DeletePlayer();
            GameManager.Instance.LoadMainScene();
        }
    
        private void LoadGame()
        {
            GameManager.Instance.LoadMainScene();
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}
using System.IO;
using GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TitleScreenManager : MonoBehaviour
    {
        [Header("Screens")]public GameObject titleScreen, optionsScreen;

        [Header("TitleScreen")] public Button newGameButton, loadGameButton, optionsButton, exitButton;

        [Header("OptionsScreen")] public Button backButton;
    
        //private AudioManager _audioManager;
    
        private void Start()
        {
            //_audioManager = GetComponent<AudioManager>();
            newGameButton.onClick.AddListener(StartGame);
            if (File.Exists(GameManager.Instance.savePath)){
                loadGameButton.onClick.AddListener(LoadGame);
            } else
            {
                loadGameButton.GetComponent<Image>().color = Color.gray;
            }
            optionsButton.onClick.AddListener(ShowOptions);
            backButton.onClick.AddListener(HideOptions);
            exitButton.onClick.AddListener(ExitGame);
            //_audioManager.Play("TitleMusic");
        }
    
        private void StartGame()
        {
            SaveSystem.DeletePlayer();
            GameManager.Instance.LoadNextScene();
        }
    
        private void LoadGame()
        {
            GameManager.Instance.LoadNextScene();
        }

        private void ShowOptions()
        {
            optionsScreen.SetActive(true);
            titleScreen.SetActive(false);
        }

        private void HideOptions()
        {
            titleScreen.SetActive(true);
            optionsScreen.SetActive(false);
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}
using System.IO;
using GameManagement;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseScreenManager : MonoBehaviour
    {
        [Header("Screens")] public GameObject pauseMenu;
        public GameObject mapMenu;
        public GameObject upgradesMenu;

        [Header("PauseMenu")] public Button backToGameButton;
        public Button loadGameButton;
        public Button backToTileButton;
        public Button mapButton;
        public Button upgradesButton;

        [Header("MapMenu")] public Button backButton1;

        [Header("UpgradesMenu")] public Button backButton2;
        public Button jumpButton;
        public Button wallJumpButton;
        public Button ledgeGrabButton;
        public Button kickButton;
        public Button tentacleButton;
        public Button dashButton;
        public Button tripleButton;
        public Button doubleJumpButton;
        public Button morphButton;
        public Button piercingButton;
        public Button gravityButton;

        public void Start()
        {
            pauseMenu.SetActive(true);
            mapMenu.SetActive(false);
            upgradesMenu.SetActive(false);

            //PAUSE SCREEN
            backToGameButton.onClick.AddListener(UnpauseGame);
            if (File.Exists(GameManager.Instance.savePath))
            {
                loadGameButton.onClick.AddListener(LoadGame);
            }
            else
            {
                loadGameButton.GetComponent<Image>().color = Color.gray;
            }

            backToTileButton.onClick.AddListener(LoadTitle);
            mapButton.onClick.AddListener(LoadMap);
            upgradesButton.onClick.AddListener(LoadUpgrades);

            //MAP SCREEN
            backButton1.onClick.AddListener(LoadPause);

            //UPGRADES SCREEN
            backButton2.onClick.AddListener(LoadPause);
            //eventualmente se tivermos tempo, metemos eventos nos botões para dar display de uma tooltip
            //portanto eventos metemos no start
            //mas o display é só quando carregarmos o upgrade screen
        }

        public void CheckIfLoad()
        {
            if (File.Exists(GameManager.Instance.savePath))
            {
                loadGameButton.onClick.AddListener(LoadGame);
            }
            else
            {
                loadGameButton.GetComponent<Image>().color = Color.gray;
            }
        }

        private void UnpauseGame()
        {
            LevelManager.Instance.UnpauseGame();
        }

        private void LoadGame()
        {
            GameManager.Instance.ReloadScene();
        }

        private void LoadTitle()
        {
            GameManager.Instance.LoadTitleScreen();
        }

        private void LoadPause()
        {
            pauseMenu.SetActive(true);
            mapMenu.SetActive(false);
            upgradesMenu.SetActive(false);
        }

        private void LoadMap()
        {
            pauseMenu.SetActive(false);
            mapMenu.SetActive(true);
            upgradesMenu.SetActive(false);
        }

        private void LoadUpgrades()
        {
            pauseMenu.SetActive(false);
            mapMenu.SetActive(false);
            upgradesMenu.SetActive(true);
            DisplayUpgrades();
        }

        private void DisplayUpgrades()
        {
            dashButton.gameObject.SetActive(PlayerEntity.Instance.unlockedDash);
            tripleButton.gameObject.SetActive(PlayerEntity.Instance.unlockedTripleBeam);
            doubleJumpButton.gameObject.SetActive(PlayerEntity.Instance.unlockedDoubleJump);
            morphButton.gameObject.SetActive(PlayerEntity.Instance.unlockedMorphBall);
            piercingButton.gameObject.SetActive(PlayerEntity.Instance.unlockedPiercingBeam);
            gravityButton.gameObject.SetActive(PlayerEntity.Instance.unlockedGravitySuit);
        }
    }
}
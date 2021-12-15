using System;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {

        [HideInInspector] public static GameManager Instance { get; private set; }

        [HideInInspector] public bool countingTime;
        [HideInInspector] public float timeElapsed;
 
        [HideInInspector] public string savePath;

        public int lastMaxItems;
        public int lastCollectedItems;
        public float lastRecordedTime;

        private AudioManager _audioManager;
        
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                savePath = Application.persistentDataPath + "/player.steve";
            }
            else
            {
                Debug.LogWarning("Multiple game managers present in scene! Destroying...");
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _audioManager = GetComponent<AudioManager>();
            _audioManager.Play("TitleMusic");
        }

        public void Update()
        {
            if (countingTime)
            {
                timeElapsed += Time.deltaTime;
            }
        }

        public void StartCountingTime()
        {
            countingTime = true;
        }

        public void StopCountingTime()
        {
            countingTime = false;
        }

        public void LoadTitleScreen()
        {
            _audioManager.Stop("GameMusic");
            _audioManager.Play("TitleMusic");
            SceneManager.LoadScene(0);
        }
    
        public void LoadMainScene()
        {
            _audioManager.Stop("TitleMusic");
            _audioManager.Play("GameMusic");
            countingTime = true;
            SceneManager.LoadScene(1);
        }
        
        public void LoadCredits()
        {
            _audioManager.Stop("GameMusic");
            _audioManager.Play("TitleMusic");
            countingTime = false;
            lastRecordedTime = timeElapsed;
            SceneManager.LoadScene(2);
        }
    
        public void ReloadScene()
        {
            _audioManager.Stop("TitleMusic");
            _audioManager.Play("GameMusic");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

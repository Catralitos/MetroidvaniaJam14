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
            SceneManager.LoadScene(0);
        }
    
        public void LoadMainScene()
        {
            countingTime = true;
            SceneManager.LoadScene(1);
        }
        
        public void LoadCredits()
        {
            countingTime = false;
            lastRecordedTime = timeElapsed;
            SceneManager.LoadScene(2);
        }
    
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

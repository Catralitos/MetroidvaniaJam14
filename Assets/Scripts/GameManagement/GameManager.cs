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
    
        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

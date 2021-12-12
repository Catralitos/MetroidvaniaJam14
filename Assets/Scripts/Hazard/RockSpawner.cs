using UnityEngine;

namespace Hazard
{
    public class RockSpawner : MonoBehaviour
    {
        public float timeSpawn;
        private float _currentTimer;
        public GameObject rocks;
        public Transform spawnTransform;

        // Start is called before the first frame update
        void Start()
        {
            _currentTimer = timeSpawn;

        }

        // Update is called once per frame
        void Update()
        {
            _currentTimer -= Time.deltaTime;
            if (_currentTimer < 0)
            {
                Instantiate(rocks, spawnTransform.position, spawnTransform.rotation, gameObject.transform);
                _currentTimer = timeSpawn;
            }

        }


    }
}

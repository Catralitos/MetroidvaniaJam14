using System.Collections.Generic;
using Extensions;
using GameManagement;
using Player;
using UnityEditor;
using UnityEngine;

namespace Hazard
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] public Openable leftDoor;
        public Openable rightDoor;

        public float timeBetweenSpawns;
        private float _spawnTimer;

        private bool _spawning;

        public LayerMask playerMask;
        public List<Transform> spawnPoints;
        public List<MonsterList> monsterLists;

        private List<GameObject> _spawnedMonsters;

        private bool _allSpawned;

        private int _numCycles;
        private int _c = 0;

        // Start is called before the first frame update
        private void Start()
        {
            if (spawnPoints.Count != monsterLists.Count)
            {
                Debug.LogError("There must be a list for each spawn point");
                return;
            }

            for (int i = 0; i < monsterLists.Count - 1; i++)
            {
                if (monsterLists[i].monsters.Count != monsterLists[i + 1].monsters.Count)
                {
                    Debug.LogError("Every monster list must be the same length");
                    return;
                }
            }

            _spawnedMonsters = new List<GameObject>();
            _numCycles = monsterLists[0].monsters.Count;
        }

        private void Update()
        {
            if (_allSpawned)
            {
                bool allNull = true;
                foreach (GameObject monster in _spawnedMonsters)
                {
                    if (monster != null && monster.activeSelf)
                    {
                        allNull = false;
                        break;
                    }
                }

                if (allNull)
                {
                    {
                        OpenDoor();
                        return;
                    }
                }
            }

            if (_c >= _numCycles)
            {
                _spawning = false;
                _allSpawned = true;
            }

            if (!_spawning) return;

            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer <= 0)
            {
                foreach (Transform t in spawnPoints)
                {
                    GameObject toSpawn = monsterLists[spawnPoints.IndexOf(t)].monsters[_c];
                    if (toSpawn == null) break;
                    GameObject spawned = Instantiate(toSpawn, t.position, t.rotation, t);
                    if (spawned != null) _spawnedMonsters.Add(spawned);
                }

                _spawnTimer = timeBetweenSpawns;
                _c++;
            }
        }

        private void OpenDoor()
        {
            PlayerEntity.Instance.combatRoomsBeaten[ArrayUtility.IndexOf(LevelManager.Instance.combatRooms, this)] =
                true;
            leftDoor.Open();
            rightDoor.Open();
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (playerMask.HasLayer(other.gameObject.layer))
            {
                if (!(_c >= _numCycles))
                {
                    leftDoor.Close();
                    rightDoor.Close();
                    _spawning = true;
                }
            }
        }
    }
}
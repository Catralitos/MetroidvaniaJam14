using System.Collections.Generic;
using Extensions;
using UnityEditor;
using UnityEngine;
using static LevelManager;

public class EnemySpawner : MonoBehaviour
{
    public float timeBetweenSpawns;
    private float _spawnTimer;

    private bool _spawning;

    public LayerMask playerMask;
    public List<Transform> spawnPoints;
    public List<MonsterList> monsterLists;

    private List<GameObject> _spawnedMonsters;

    private bool _allSpawned;
    private bool _allKilled;

    private int _numCycles;
    private int _c = 0;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < monsterLists.Count - 1; i++)
        {
            if (monsterLists[i].monsters.Count != monsterLists[i + 1].monsters.Count)
            {
                Debug.LogError("Every monster list must be the same length");
                return;
            }
        }

        _numCycles = monsterLists[0].monsters.Count;
    }

    private void Update()
    {
        if (!_spawning && _allKilled)
        {
            bool allNull = true;
            foreach (GameObject monster in _spawnedMonsters)
            {
                if (monster != null) allNull = false;
            }

            if (allNull) OpenDoor();
        }

        if (_c >= _numCycles)
        {
            _spawning = false;
            _allSpawned = true;
        }

        if (!_spawning) return;

        if (_spawnTimer <= 0)
        {
            foreach (Transform t in spawnPoints)
            {
                GameObject toSpawn = monsterLists[spawnPoints.IndexOf(t)].monsters[_c];
                if (toSpawn == null) break;
                Instantiate(toSpawn, t.position, t.rotation, t);
            }

            _spawnTimer = timeBetweenSpawns;
            _c++;
        }
    }

    public void OpenDoor()
    {
        PlayerEntity.Instance.combatRoomsBeaten[ArrayUtility.IndexOf(LevelManager.Instance.combatRooms, this)] = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerMask.HasLayer(other.gameObject.layer))
        {
            if (!(_c >= _numCycles)) _spawning = true;
        }
    }
}
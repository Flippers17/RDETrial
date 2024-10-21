using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private Vector2 _spawnAreaSize = Vector2.one;

    [SerializeField]
    private float _spawnDelay = 10f;
    private float _timeLastSpawned = 0;



    // Update is called once per frame
    void Update()
    {
        if(_timeLastSpawned + _spawnDelay < Time.time)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        float x = Random.Range(-_spawnAreaSize.x / 2, _spawnAreaSize.x / 2);
        float y = Random.Range(-_spawnAreaSize.y / 2, _spawnAreaSize.y / 2);

        if(Pathfinder.Instance.Grid.GetNearestUnoccupiedSpace(new Vector2(x, y), out Vector2 spawnPos))
        {
            Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
        }

        _timeLastSpawned = Time.time;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector2.zero, _spawnAreaSize);
    }
}

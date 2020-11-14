using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject enemy;

    [SerializeField] private int initialAmount;

    [SerializeField] private float spawnRate;

    [SerializeField] private float minDistance;

    [SerializeField] private float maxDistance;

    #endregion

    #region Private

    private Transform _player;

    private float _nextSpawn;

    #endregion

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        InitialSpawn();
    }

    private void Update()
    {
        if (_nextSpawn <= Time.time)
        {
            Spawn();

            _nextSpawn = Time.time + 1 / spawnRate;
        }
    }

    private void InitialSpawn()
    {
        for (var i = 0; i < initialAmount; i++)
        {
            Spawn();
        }

        _nextSpawn = Time.time + 1 / spawnRate;
    }

    private void Spawn()
    {
        var position = Random.insideUnitCircle.normalized * Random.Range(minDistance, maxDistance);
        position += (Vector2) _player.position;
        
        Instantiate(enemy, position, Quaternion.identity);
    }
}
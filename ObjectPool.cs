using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("ObjectWasSpawned")]
    [SerializeField] private List<GameObject> _enemyWasSpawned;
    [SerializeField] private List<GameObject> _pointWasSpawned;

    [Header("ObjectForSpawn")]
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private GameObject _enemyPrefab;

    [Header("SpawnPlace")]
    [SerializeField] private Transform[] _spawnPlaceAtScene;
    [SerializeField] private Vector2[] _spawnPlace;

    [Header("Parent")]
    [SerializeField] private Transform _parentEnemy;
    [SerializeField] private Transform _parentPoint;

    [Header("Count")]
    [SerializeField] private int _countEnemyPrefab;
    [SerializeField] private int _countPointPrefab;

    [Header("RandomNumber")]
    //point - black cube
    private int _randomSpawnPoint;
    //0 no | 1 yes
    private int _willBeSpawnEnemyOrNot;

    [Header("Turn")]
    //point - black cube
    private int _turnPoint;
    private int _turEnemy;

    [Header("Time")]
    [SerializeField] private float _timeBetvenSpawnObject;
    private float _elapsedTime;

    private void Start()
    {
        //initialize spawn point
        for(int i = 0; i <= (_spawnPlaceAtScene.Length - 1); i++)
        {
            _spawnPlace[i] = _spawnPlaceAtScene[i].transform.position;
        }

        //initialize enemy prefab and disable
        for(int i = 0; i <= _countEnemyPrefab; i++)
        {
            _enemyWasSpawned.Add(Instantiate(_enemyPrefab));

            if(i >= _countEnemyPrefab)
            {
                for(int j = 0; j <= (_enemyWasSpawned.Count - 1); j++)
                {
                    _enemyWasSpawned[j].transform.SetParent(_parentEnemy);
                    _enemyWasSpawned[j].gameObject.SetActive(false);
                }
            }
        }

        //initialize point prefab and disable
        for(int i = 0; i <= _countPointPrefab; i++)
        {
            _pointWasSpawned.Add(Instantiate(_pointPrefab));

            if(i >= _countPointPrefab)
            {
                for(int j = 0; j <= (_pointWasSpawned.Count - 1); j++)
                {
                    _pointWasSpawned[j].transform.SetParent(_parentPoint);
                    _pointWasSpawned[j].gameObject.SetActive(false);
                }
            }
        } 

    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;

        if(_elapsedTime >= _timeBetvenSpawnObject)
        {
            _randomSpawnPoint = Random.Range(0, _spawnPlaceAtScene.Length);

            //point part ↓
            _pointWasSpawned[_turnPoint].transform.position = _spawnPlace[_randomSpawnPoint];
            _pointWasSpawned[_turnPoint].gameObject.SetActive(true);

            _turnPoint++;

            if(_turnPoint > (_pointWasSpawned.Count - 1))
            {
                _turnPoint = 0;
            }
            //point part ↑

            //enemy part ↓
            _willBeSpawnEnemyOrNot = Random.Range(0, 2);

            if(_willBeSpawnEnemyOrNot == 1)
            {
                _randomSpawnPoint++;

                if(_randomSpawnPoint > (_spawnPlaceAtScene.Length - 1))
                {
                    _randomSpawnPoint -= 2;
                }
                else if(_randomSpawnPoint < 0)
                {
                    _randomSpawnPoint += 2;
                }

                _turEnemy++;

                if(_turEnemy > (_enemyWasSpawned.Count - 1))
                {
                    _turEnemy = 0;
                }

                _enemyWasSpawned[_turEnemy].transform.position = _spawnPlace[_randomSpawnPoint];
                _enemyWasSpawned[_turEnemy].gameObject.SetActive(true);
            }
            //enemy part ↑

            _elapsedTime = 0;
        }
    }
}

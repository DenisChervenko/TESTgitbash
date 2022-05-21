using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevel : MonoBehaviour
{
    [Header("SpawnPoint")]
    [SerializeField] private Transform[] _spawnPointAtScene;
    [SerializeField] private Vector3[] _spawnPointVector;
    //spawn point
    private int _randomSpawnPoint;
    private bool _wasUseLongAngle = false;
    private bool _firstIterationRandomSpawn = true;
    private bool _spawnIsEnd = true;
    //angle road
    private bool _canSpawnAngle = false;

    [Header("Prefab")]
    [SerializeField] private GameObject _prefabRoad;
    [SerializeField] private GameObject _angleOfRoadPrefab;
    [SerializeField] private GameObject _longAngleOfRoadPrefab;
    [SerializeField] private GameObject _leftLongAngleOfRoadPrefab;
    [SerializeField] private List<GameObject> _roadWasSpawned;
    [SerializeField] private List<GameObject> _angleWasSpawned;
    [SerializeField] private List<GameObject> _longAngleOfRoadSpawned;
    [SerializeField] private List<GameObject> _leftLongAngleOfRoadSpawned;
    [SerializeField] private int _countObject;
    //prefab
    private int _countObjectAtOneSpawnPoint;
    private int _iterationBetvenEndSpawn;
    private int _currentTurnPrefab;
    private int _currentTurnPrefabAngle;
    private int _currentTurnPrefabLonAngle;
    private int  _currentTurnPrefabLeftLonAngle;
    private int _pastUsePrefabLeftLongAngle;
    private int _pastUsePrefabLongAngle;
    private int _pastUsePrefabAngle;
    private int _pastUsePrefab;
    private bool _itWasNegative;

    [Header("Parent")]
    [SerializeField] private Transform _parentForRoad;
    [SerializeField] private Transform _parentForAngleRoad;
    [SerializeField] private Transform _parentLongAngleForRoad;
    [SerializeField] private Transform _parentLeftLongAngleForRoad;

    [Header("Time")]
    [SerializeField] private float _timeBetwenSpawn;
    [SerializeField] private float _timeBetwenSpawnAngle;
    private float _elapsedTime;

    private void Start()
    {
        //initialize spawn point
        for(int i = 0; i <= 6;  i++)
        {
            _spawnPointVector[i] = _spawnPointAtScene[i].transform.position;
        }

        //create prefab
        for(int i = 0; i < _countObject; i++)
        {
            _roadWasSpawned.Add(Instantiate(_prefabRoad));

            _angleWasSpawned.Add(Instantiate(_angleOfRoadPrefab));

            _longAngleOfRoadSpawned.Add(Instantiate(_longAngleOfRoadPrefab));

            _leftLongAngleOfRoadSpawned.Add(Instantiate(_leftLongAngleOfRoadPrefab));
        }

        //disable prefab
        for(int j = 0; j <= 49; j++)
        {
            _roadWasSpawned[j].gameObject.SetActive(false);

            _angleWasSpawned[j].gameObject.SetActive(false);

            _longAngleOfRoadSpawned[j].gameObject.SetActive(false);

            _leftLongAngleOfRoadSpawned[j].gameObject.SetActive(false);
        }

        //adopt prefab

        for(int j = 0; j <= 49; j++)
        {
            _roadWasSpawned[j].transform.SetParent(_parentForRoad);

            _angleWasSpawned[j].transform.SetParent(_parentForAngleRoad);

            _longAngleOfRoadSpawned[j].transform.SetParent(_parentLongAngleForRoad);

            _leftLongAngleOfRoadSpawned[j].transform.SetParent(_parentLeftLongAngleForRoad);
        }
    }

    private void FixedUpdate()
    {
        if(_spawnIsEnd)
        {
            _countObjectAtOneSpawnPoint = Random.Range(1, 4);

            //this part of code will be use only once
            if(_firstIterationRandomSpawn)
            {
                
                _randomSpawnPoint = Random.Range(0, 7);

                _firstIterationRandomSpawn = false;
            }        
            else
            {
                //get random point for spawn object
                RandomizeSpawnPoint();

                if(!_wasUseLongAngle)
                {
                    //initialize place were will be spawn angle
                    TransformAngleForSpawn(_randomSpawnPoint);
                }
                else
                {
                    TransformLongAngleForRoad(_randomSpawnPoint);
                }
            }

            //initialize place were will be spawn main road
            TransformRoadToSpawnPoint(_randomSpawnPoint);  
        }
        else
        {
            // spawn part
            _elapsedTime += Time.fixedDeltaTime;
            
            if(!_wasUseLongAngle)
            {
                if(_canSpawnAngle)
                {
                    EnableAngleOfRoad();
                }
            }
            else
            {
                if(_canSpawnAngle)
                {
                    EnableLongAngleOfRoad();
                }
            }
            
            ActiveRoadAtSpawnPoint();
        }
    }

    private void RandomizeSpawnPoint()
    {
        int bigAngleWillBe = 0;

        if(_randomSpawnPoint >= 2  & _randomSpawnPoint <= 4)
        {
            bigAngleWillBe = Random.Range(0, 2);
        }

        int tempSmallAngle = Random.Range(0, 2);
        
        int tempBigAngle = Random.Range(0, 2);

        if(bigAngleWillBe == 0)
        {
            _wasUseLongAngle = false;

            if(tempSmallAngle == 0)
            {
                _randomSpawnPoint++;

                if(_randomSpawnPoint == 7)
                {
                    _randomSpawnPoint = 5;
                }
            }
            else if(tempSmallAngle == 1)
            {
                _randomSpawnPoint--;

                if(_randomSpawnPoint == -1)
                {
                    _randomSpawnPoint = 1;
                }
            }
        }
        else
        {
            //disable short angle
            _wasUseLongAngle = true;

            if(_randomSpawnPoint == 2)
            {
                _randomSpawnPoint += 2;
                _itWasNegative = false;
            }
            else if(_randomSpawnPoint == 3)
            {
                if(tempBigAngle == 0)
                {
                    _randomSpawnPoint -= 2;
                    _itWasNegative = true;
                }
                else
                {
                    _randomSpawnPoint += 2;
                    _itWasNegative = false;
                }
            }
            else if(_randomSpawnPoint == 4)
            {
                _randomSpawnPoint -= 2;
                _itWasNegative = true;
            }
        }
        
    }

    //main road
    private void TransformRoadToSpawnPoint(int spawnPoint)
    {
        for(int i = 0; i <= (_countObjectAtOneSpawnPoint - 1); i++)
        {
              _roadWasSpawned[_pastUsePrefab].transform.position = _spawnPointVector[spawnPoint];

            _pastUsePrefab++;

            if(_pastUsePrefab > 49)
            {
                _pastUsePrefab = 0;
            }
        }         

        _spawnIsEnd = false;
    }

    private void ActiveRoadAtSpawnPoint()
    {   
        if(!_spawnIsEnd)
        {
            if(_elapsedTime >= _timeBetwenSpawn)
            {
                _roadWasSpawned[_currentTurnPrefab].gameObject.SetActive(true);

                _currentTurnPrefab++;

                if(_currentTurnPrefab > 49)
                {
                    _currentTurnPrefab = 0;
                }

                _elapsedTime = 0;

                _iterationBetvenEndSpawn++;

                if(_iterationBetvenEndSpawn == _countObjectAtOneSpawnPoint)
                {
                    _iterationBetvenEndSpawn = 0;

                    _spawnIsEnd = true;
                }
            }
        }
    }

    //angle of road

    private void TransformAngleForSpawn(int spawnPoint)
    {
        _angleWasSpawned[_pastUsePrefabAngle].transform.position = _spawnPointVector[spawnPoint];

        _pastUsePrefabAngle++;

        if(_pastUsePrefabAngle > 49)
        {
            _pastUsePrefabAngle = 0;
        }

        _canSpawnAngle = true;
    }

    private void TransformLongAngleForRoad(int spawnPoint)
    {
        if(_itWasNegative)
        {
            _longAngleOfRoadSpawned[_pastUsePrefabLongAngle].transform.position = _spawnPointVector[spawnPoint + 1];

            _pastUsePrefabLongAngle++;

            if(_pastUsePrefabLongAngle > 49)
            {
                _pastUsePrefabLongAngle = 0;
            }

            spawnPoint -= 1;
        }
        else
        {
            _leftLongAngleOfRoadSpawned[_pastUsePrefabLeftLongAngle].transform.position = _spawnPointVector[spawnPoint - 1];

            _pastUsePrefabLeftLongAngle++;

            if(_pastUsePrefabLeftLongAngle > 49)
            {
                _pastUsePrefabLeftLongAngle = 0;
            }

            spawnPoint += 1;
        }

        _canSpawnAngle = true;
    }

    private void EnableAngleOfRoad()
    {
        if(_elapsedTime >= _timeBetwenSpawnAngle)
        {
            


            _angleWasSpawned[_currentTurnPrefabAngle].gameObject.SetActive(true);

                _currentTurnPrefabAngle++;

                if(_currentTurnPrefabAngle > 49)
                {
                    _currentTurnPrefabAngle = 0;
                }
            

            

            _canSpawnAngle = false;
        }
    }

    private void EnableLongAngleOfRoad()
    {
        if(_elapsedTime >= _timeBetwenSpawnAngle)
        {
           

            if(_itWasNegative)
            {
                 _longAngleOfRoadSpawned[_currentTurnPrefabLonAngle].gameObject.SetActive(true);

                _currentTurnPrefabLonAngle++;

                if(_currentTurnPrefabLonAngle > 49)
                {
                    _currentTurnPrefabLonAngle = 0;
                }
            }
            else
            {
                _leftLongAngleOfRoadSpawned[_currentTurnPrefabLeftLonAngle].gameObject.SetActive(true);

                _currentTurnPrefabLeftLonAngle++;

                if(_currentTurnPrefabLeftLonAngle > 49)
                {
                    _currentTurnPrefabLeftLonAngle = 0;
                }
            }

            _canSpawnAngle = false;
        }
    }

    private void TestLine() => return;
}

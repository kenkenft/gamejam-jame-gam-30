using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject[] ValidSpawnArea = new GameObject[4]; // Represent the coordinate boundaries that coins can spawn in. Indexes 0 and 3 represent the outer most boundaries; Index 1 and 2 represent internal boundary box i.e. the coin hole 
    private GameObject _tmpObj;

    private List<GameObject> _pooledObjsList = new List<GameObject>();

    private Vector3 _spawnTargetPos = new Vector3(0f, 0f, 0f);
    // private List<CoinScriptable> _coinTypes = new List<CoinScriptable>();
    public SOEnemy[] EnemyTypes;

    private int _tempInt;

    void OnEnable()
    {
        // UIManager.StartGameSetUp += GameStartSetUp;
        PlayerAge.SpawnTimerExpired += SpawnEnemyOnField;
        // Timer.CheckCoinsDespawn += UpdateCoinDespawnTimers;
    }

    void OnDisable()
    {
        // UIManager.StartGameSetUp -= GameStartSetUp;
        PlayerAge.SpawnTimerExpired -= SpawnEnemyOnField;
        // Timer.CheckCoinsDespawn -= UpdateCoinDespawnTimers;
    }

    public void GameStartSetUp()
    {
        //  _coinTypes.Clear();
        //  _coinTypes.AddRange(GameProperties.GetCoinScriptables());
        if(_pooledObjsList.Count < 1)
            InstantiateEnemyPrefabPool(50);
        else
            ClearField();
        for(int i = 0; i < 4; i ++)
            SpawnEnemyOnField();
    }

    public void InstantiateEnemyPrefabPool(int expectedUpperLimit)
    {
        // Method that instantiates initial pool of coin prefab objects
        for(int i = 0; i < expectedUpperLimit; i++)
            InstantiateEnemyPrefab(false);
    }

    private void InstantiateEnemyPrefab(bool isActive)
    {
        // Instantiate coin prefab and add to pooledObjsList 
        _tmpObj = Instantiate(EnemyPrefab);
        _tmpObj.name = "Enemy Prefab " + _pooledObjsList.Count;
        _tmpObj.transform.parent = transform;
        EnableEnemyComponents(isActive);
        _tmpObj.GetComponent<EnemyMain>().SetUp();
        _pooledObjsList.Add(_tmpObj);
    }

    private void EnableEnemyComponents(bool isActive)
    {
        _tmpObj.SetActive(isActive);   // Set coin spriterender to inactive to hide coin
        _tmpObj.GetComponentInChildren<Collider2D>().enabled = isActive;
    }
    
    public GameObject GetPooledEnemy()
    {
        // Method returns a inactive coin gameobject prefab 
        for(int i = 0; i < _pooledObjsList.Count; i ++)
        {
            if(!_pooledObjsList[i].activeInHierarchy)
                return _pooledObjsList[i];
        }
        Debug.Log("No inactive enemy found - All pooled enemies used. Instantiating extra enemies");
        InstantiateEnemyPrefab(true);
        return _pooledObjsList[_pooledObjsList.Count-1];
    }

    public void SpawnEnemyOnField()
    {
        int randomInt = Random.Range(1,6);
        
        for(int i=0; i < randomInt; i++)
        {
            _tmpObj = GetPooledEnemy();
            _tmpObj.GetComponent<EnemyMain>().EnemyProperties = SelectRandomEnemyType();
            _tmpObj.GetComponent<EnemyMain>().SetUp();
            SelectValidSpawnCoord();
            EnableEnemyComponents(true);
            _tmpObj.transform.position = _spawnTargetPos;
        }
    }
    private void SelectValidSpawnCoord()
    {
        _spawnTargetPos[0] = Random.Range(ValidSpawnArea[0].transform.position.x, ValidSpawnArea[3].transform.position.x);
        _spawnTargetPos[1] = Random.Range(ValidSpawnArea[0].transform.position.y, ValidSpawnArea[3].transform.position.y);

        IsSpawnTargetInPlayerZone(); 
    }

    private void IsSpawnTargetInPlayerZone()
    {
        if(ValidSpawnArea[1].transform.position.x < _spawnTargetPos[0] && _spawnTargetPos[0] < ValidSpawnArea[2].transform.position.x)
        {
            while(ValidSpawnArea[2].transform.position.y < _spawnTargetPos[1] && _spawnTargetPos[1] < ValidSpawnArea[1].transform.position.y)
                _spawnTargetPos[1] = Random.Range(ValidSpawnArea[0].transform.position.y, ValidSpawnArea[3].transform.position.y);
        }
    }      

    private SOEnemy SelectRandomEnemyType()
    {
        _tempInt =  Random.Range(0,100);
        Debug.Log("_tempInt: " + _tempInt);

        if(_tempInt > 90)
        {    
            Debug.Log("Large gear!: " + (EnemyTypes.Length-1));
            return EnemyTypes[EnemyTypes.Length-1];
        }
        else if (_tempInt <= 90 && _tempInt > 70)
        {    
            Debug.Log("Medium gear!: " + (EnemyTypes.Length-1));
            return EnemyTypes[1];
        }
        else
        {
            Debug.Log("Small gear!: " + (EnemyTypes.Length-1));
            return EnemyTypes[0];
        }
    } 

    private void ClearField()
    {
        foreach(GameObject enemy in _pooledObjsList)
        {
            _tmpObj = enemy;
            EnableEnemyComponents(false);
        }
    }

    // public void UpdateCoinDespawnTimers()
    // {
    //     Debug.Log("CoinSpawner.UpdateCoinDespawnTimers() invoked!");
    //     for(int i = 0; i < _pooledObjsList.Count; i ++)
    //     {
    //         if(_pooledObjsList[i].activeInHierarchy)
    //             _pooledObjsList[i].GetComponent<EnemyMain>().UpdateCoinDespawnTimer();
    //     }
    // }
}

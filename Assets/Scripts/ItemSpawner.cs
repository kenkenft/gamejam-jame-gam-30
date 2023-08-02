using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    private GameObject _tmpObj;

    private List<GameObject> _pooledObjsList = new List<GameObject>();

    private Vector3 _spawnTargetPos = new Vector3(0f, 0f, 0f);
    // public SOEnemy[] EnemyTypes;

    private int _tempInt;

    void OnEnable()
    {
        UIManager.StartGameSetUp += GameStartSetUp;
        UIManager.FieldCleared += ClearField;
        PlayerAge.CheckItemsDespawn += UpdateItemDespawnTimers;
        EnemyMain.ItemDropped += SpawnItemOnField;
    }

    void OnDisable()
    {
        UIManager.StartGameSetUp -= GameStartSetUp;
        UIManager.FieldCleared -= ClearField;
        PlayerAge.CheckItemsDespawn -= UpdateItemDespawnTimers;
        EnemyMain.ItemDropped -= SpawnItemOnField;
    }

    public void GameStartSetUp()
    {
        if(_pooledObjsList.Count < 1)
            InstantiateEnemyPrefabPool(5);
        else
            ClearField();
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
        _tmpObj = Instantiate(ItemPrefab);
        _tmpObj.name = "Item Prefab " + _pooledObjsList.Count;
        _tmpObj.transform.parent = transform;
        _tmpObj.GetComponent<ItemHourGlass>().SetUpItem();
        EnableEnemyComponents(isActive);
        _pooledObjsList.Add(_tmpObj);
    }

    private void EnableEnemyComponents(bool isActive)
    {
        _tmpObj.SetActive(isActive);   // Set coin spriterender to inactive to hide coin
        _tmpObj.GetComponent<Collider2D>().enabled = isActive;
    }
    
    public GameObject GetPooledEnemy()
    {
        // Method returns a inactive coin gameobject prefab 
        for(int i = 0; i < _pooledObjsList.Count; i ++)
        {
            if(!_pooledObjsList[i].activeInHierarchy)
                return _pooledObjsList[i];
        }
        Debug.Log("No inactive item found - All pooled items used. Instantiating extra item");
        InstantiateEnemyPrefab(true);
        return _pooledObjsList[_pooledObjsList.Count-1];
    }

    public void SpawnItemOnField(Vector3 deadEnemyPos)
    {
            _tmpObj = GetPooledEnemy();
            EnableEnemyComponents(true);
            _tmpObj.GetComponent<ItemHourGlass>().SetUpItem();
            _tmpObj.transform.position = deadEnemyPos;
    } 

    public void ClearField()
    {
        foreach(GameObject item in _pooledObjsList)
        {
            _tmpObj = item;
            EnableEnemyComponents(false);
        }
    }

    public void UpdateItemDespawnTimers()
    {
        // Debug.Log("CoinSpawner.UpdateCoinDespawnTimers() invoked!");
        for(int i = 0; i < _pooledObjsList.Count; i ++)
        {
            if(_pooledObjsList[i].activeInHierarchy)
                _pooledObjsList[i].GetComponent<ItemHourGlass>().UpdateDespawnTimer();
        }
    }
}

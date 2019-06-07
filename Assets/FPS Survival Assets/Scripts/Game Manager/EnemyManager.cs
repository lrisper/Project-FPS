using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] private GameObject _boarPrefab, _cannibalPrefab;

    public Transform[] cannibalSpawnPoints, boarSpawnPoints;

    [SerializeField] private int _cannibalEnemyCount;
    [SerializeField] private int _boarEnemyCount;

    private int _intialCannibalCount;
    private int _intialBoarCount;

    public float waitBeforeSpawnEnemyTime = 10f;

    void Awake()
    {
        MakeInstance();
    }

    public void Start()
    {
        _intialCannibalCount = _cannibalEnemyCount;
        _intialBoarCount = _boarEnemyCount;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void SpawnEnemies()
    {
        SpawnCannibal();
        SpawnBoar();
    }

    public void SpawnCannibal()
    {
        int index = 0;

        for (int i = 0; i < _cannibalEnemyCount; i++)
        {
            if (index >= cannibalSpawnPoints.Length)
            {
                index = 0;
            }
            Instantiate(_cannibalPrefab, cannibalSpawnPoints[index].position, Quaternion.identity);
            index++;
        }
        _cannibalEnemyCount = 0;
    }

    public void SpawnBoar()
    {
        int index = 0;

        for (int i = 0; i < _boarEnemyCount; i++)
        {
            if (index >= boarSpawnPoints.Length)
            {
                index = 0;
            }
            Instantiate(_boarPrefab, boarSpawnPoints[index].position, Quaternion.identity);
            index++;
        }
        _boarEnemyCount = 0;
    }

    IEnumerator CheckToSpawnEnemies()
    {

        yield return new WaitForSeconds(waitBeforeSpawnEnemyTime);
        SpawnCannibal();
        SpawnBoar();

        StartCoroutine("CheckToSpawnEnemies");
    }

    public void EnemyDie(bool cannibal)
    {
        if (cannibal)
        {
            _cannibalEnemyCount++;
            if (_cannibalEnemyCount > _intialCannibalCount)
            {
                _cannibalEnemyCount = _intialCannibalCount;
            }
            else
            {
                _boarEnemyCount++;
                if (_boarEnemyCount > _intialBoarCount)
                {
                    _boarEnemyCount = _intialBoarCount;

                }
            }
        }
    }
    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }

}

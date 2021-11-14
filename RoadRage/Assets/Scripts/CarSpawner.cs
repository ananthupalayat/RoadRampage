using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private float m_CarSpawnTime = 2f;

    private float mTimerCounter=0;

    [SerializeField] List<Transform> m_SpawnLocations = new List<Transform>();

    [SerializeField] List<GameObject> m_CarPrefabs = new List<GameObject>();

    private void Update()
    {
        if (mTimerCounter < m_CarSpawnTime)
        {
            mTimerCounter += Time.deltaTime;
        }
        else
        {
            mTimerCounter = 0;
            SpawnCar();
        }
    }

    private void SpawnCar()
    {
        GameObject spawnedCar = ObjectPool.pInstance.GetObject(m_CarPrefabs[Random.Range(0,m_CarPrefabs.Count)]);
        spawnedCar.transform.position = m_SpawnLocations[Random.Range(0, m_SpawnLocations.Count)].position;
    }

}

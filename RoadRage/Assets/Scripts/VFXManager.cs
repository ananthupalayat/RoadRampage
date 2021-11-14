using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject m_ExplosionVFX;
    // Start is called before the first frame update
    void OnEnable()
    {
        Car.OnCarHit += SpawnExplosionVFX;
    }

    // Update is called once per frame
    void OnDisable()
    {
        
    }

    private void SpawnExplosionVFX(Car.CarType carType,Vector3 position)
    {
        Debug.Log(carType);
        GameObject spawnedExplosion = ObjectPool.pInstance.GetObject(m_ExplosionVFX);
        spawnedExplosion.transform.position = position;
        spawnedExplosion.GetComponent<ParticleSystem>().Play();
    }
}

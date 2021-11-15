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

   
    /// <summary>
    /// Creates Explosion VFX
    /// </summary>
    /// <param name="carType"></param>
    /// <param name="position"></param>
    private void SpawnExplosionVFX(Car.CarType carType,Vector3 position)
    {
        Debug.Log(carType);
        GameObject spawnedExplosion = ObjectPool.pInstance.GetObject(m_ExplosionVFX);
        spawnedExplosion.transform.position = position;
        spawnedExplosion.GetComponent<ParticleSystem>().Play();
        StartCoroutine(TurnOffVFX(spawnedExplosion, spawnedExplosion.GetComponent<ParticleSystem>().main.duration));
    }

    private IEnumerator TurnOffVFX(GameObject spawnedObject,float duration)
    {
        yield return new WaitForSeconds(duration);
        spawnedObject.SetActive(false);
    }
}

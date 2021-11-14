using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturn : MonoBehaviour
{
    
    /// <summary>
    /// Returns to their respective pool when disabled
    /// </summary>
    private void OnDisable()
    {
        if (ObjectPool.pInstance != null)
        {
            ObjectPool.pInstance.ReturnObject(this.gameObject);
        }
    }

}

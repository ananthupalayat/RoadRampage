using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    Dictionary<string, Queue<GameObject>> mObjectPool = new Dictionary<string, Queue<GameObject>>();

    public static ObjectPool pInstance;

    private void Awake()
    {
        if (pInstance == null)
        {
            pInstance = this;
        }
    }


    /// <summary>
    /// Produces packet when in need from respective pool
    /// if not found create a new pool for that game object
    /// </summary>
    /// <param name="requiredObject"></param>
    /// <returns></returns>
    public GameObject GetObject(GameObject requiredObject)
    {
        if(mObjectPool.TryGetValue(requiredObject.name,out Queue<GameObject> objectList))
        {
            if (objectList.Count == 0)
            {
                return CreateNewObject(requiredObject);
            }
            else
            {
                GameObject objectFound = objectList.Dequeue();
                objectFound.SetActive(true);
                return objectFound;
            }
        }
        else
        {
            return CreateNewObject(requiredObject);
        }
    }

    private GameObject CreateNewObject(GameObject objectIn)
    {
        GameObject newObject = Instantiate(objectIn,transform);
        newObject.name = objectIn.name;
        return newObject;

    }

    /// <summary>
    /// Returns packet to their respective pool
    /// </summary>
    /// <param name="objectIn"></param>
    public void ReturnObject(GameObject objectIn)
    {
        if(mObjectPool.TryGetValue(objectIn.name,out Queue<GameObject> objectList))
        {
            objectList.Enqueue(objectIn);
        }
        else
        {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(objectIn);
            mObjectPool.Add(objectIn.name, newObjectQueue);
        }

        objectIn.SetActive(false);

    }

}

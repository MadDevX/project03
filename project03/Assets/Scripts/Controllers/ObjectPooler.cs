using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {


    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Instance; //Singleton

    private void Awake()
    {
        Instance = this; //Singleton
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start () {


	}

    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            if (tag != "")
            {
                Debug.LogWarning("Pool with tag: " + tag + " doesn't exist.");
            }
            return null;
        }
        GameObject objectToSpawn;
        if (poolDictionary[tag].Peek().activeSelf)
        {
            int i = 0;
            while (i < pools.Count && pools[i].tag != tag) i++;
            objectToSpawn = Instantiate(pools[i].prefab);
        }
        else
        {
            objectToSpawn = poolDictionary[tag].Dequeue();
        }
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        if(pooledObject!=null)
        {
            pooledObject.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
	
}

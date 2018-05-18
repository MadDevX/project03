using UnityEngine;

public interface IPooledObject
{
    /// <summary>
    /// Used for initializing object pulled from a pool.
    /// </summary>
    void OnObjectSpawn();
}

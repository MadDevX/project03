using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledParticle : MonoBehaviour, IPooledObject
{
    public float timer = 5.0f;
    private ParticleSystem[] particles;

    public void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    public void OnObjectSpawn()
    {
        foreach(ParticleSystem ps in particles)
        {
            ps.Play();
        }
        Invoke("DeactivateObject", timer);
    }

    private void OnDisable()
    {
        CancelInvoke("DeactivateObject");
    }

    void DeactivateObject()
    {
        gameObject.SetActive(false);
    }


}

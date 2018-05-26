using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

public class Weapon : MonoBehaviour, IEquipment
{
    [SerializeField] private WeaponDetails stats;

    private ObjectPooler objectPooler;
    private Light shootingLight;
    private ParticleSystem particles;
    private AudioSource fireAudio;
    private bool effectsEnabled = false;
    private float timer;
    private float reattackTime;

    [HideInInspector] public Transform bulletSpawn;

    public WeaponDetails Stats
    {
        get
        {
            return stats;
        }
        set
        {
            stats = value;
            if (stats != null)
            {
                SetProperties();
            }
        }
    }

    // Use this for initialization
    void Awake()
    {
        if (Stats != null)
        {
            SetProperties();
        }
        bulletSpawn = gameObject.FindComponentInChildWithTag<Transform>("BulletSpawn");
        shootingLight = bulletSpawn.GetComponent<Light>();
        particles = bulletSpawn.GetComponent<ParticleSystem>();
        fireAudio = bulletSpawn.GetComponent<AudioSource>();
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (effectsEnabled && timer >= Stats.effectsDuration)
        {
            DisableEffects();
        }
    }

    /// <summary>
    /// Updates weapon specific properties.
    /// </summary>
    protected void SetProperties()
    {
        GetComponent<MeshRenderer>().material = Stats.material;
        timer = 0f;
        reattackTime = Stats.reattackTime;
    }

    public void Use(GameObject target=null)
    {
        Shoot();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    public void Shoot()
    {
        if (timer >= reattackTime)
        {
            timer = 0f;
            GameObject bullet = objectPooler.SpawnFromPool(Stats.projectilePrefab, bulletSpawn.position, bulletSpawn.rotation);
            EnableEffects();
            bullet.GetComponent<Bomb>().SetMultipliers(Stats.forceMult, Stats.damageMult);
        }
    }

    void EnableEffects()
    {
        shootingLight.enabled = true;
        particles.Play();
        fireAudio.Stop();
        fireAudio.Play();
        effectsEnabled = true;
    }

    void DisableEffects()
    {
        shootingLight.enabled = false;
        particles.Stop();
        effectsEnabled = false;
    }
}

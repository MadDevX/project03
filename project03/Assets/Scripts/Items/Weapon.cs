using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

public class Weapon : Item
{
    //Weapon Data
    [SerializeField] private WeaponDetails stats;

    //Components and state variables
    private Light shootingLight;
    private ParticleSystem particles;
    private bool effectsEnabled = false;
    private float timer;

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
            SetProperties();
        }
    }

    // Use this for initialization
    void Awake ()
    {
        FindReferences();
        if (stats != null) SetProperties(); //updates rendering materials if stats are already set
        bulletSpawn = gameObject.FindComponentInChildWithTag<Transform>("BulletSpawn");
        shootingLight = bulletSpawn.GetComponent<Light>();
        particles = bulletSpawn.GetComponent<ParticleSystem>();
	}

    private void Update()
    {
        timer += Time.deltaTime;
        if(effectsEnabled && timer>=stats.effectsDuration)
        {
            DisableEffects();
        }
    }

    /// <summary>
    /// Updates rendering data.
    /// </summary>
    protected override void SetProperties()
    {
        if (stats != null)
        {
            itemRenderer.material = stats.material;
            timer = stats.reattackTime;
        }
        base.SetProperties();
    }

    public void Shoot()
    {
        if (timer >= stats.reattackTime)
        {
            timer = 0f;
            GameObject bullet = Instantiate(stats.bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            EnableEffects();
            bullet.GetComponent<Bomb>().SetMultipliers(stats.forceMult, stats.damageMult);
            Destroy(bullet, stats.bulletTTL);
        }
    }

    void EnableEffects()
    {
        shootingLight.enabled = true;
        particles.Play();
        effectsEnabled = true;
    }

    void DisableEffects()
    {
        shootingLight.enabled = false;
        particles.Stop();
        effectsEnabled = false;
    }
}

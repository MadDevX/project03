using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStats : MonoBehaviour {

    public Stat health;
    public Stat armor;
    public Stat damage;
    public Stat agility;

    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    private ICharacterManager characterManager;
    private int currentHealth;
    private AudioSource characterAudio;

	void Awake ()
    {
        characterManager = GetComponent<ICharacterManager>();
        characterAudio = GetComponent<AudioSource>();
        currentHealth = health.GetValue();
        UpdateHealthBar();
	}

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            if(characterAudio!=null)
            {
                characterAudio.Play();
            }

            damage -= armor.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);

            currentHealth -= damage;
            //Debug.Log(transform.name + " takes " + damage + " damage!");
            float percentage = currentHealth / (float)health.GetValue();
            healthSlider.value = percentage;
            healthText.SetText(currentHealth + " HP");
            if (currentHealth <= 0)
            {
                //healthSlider.value = 0;
                Death();
            }
        }
    }

    void UpdateHealthBar()
    {
        healthSlider.maxValue = 1;
        float percentage = currentHealth / (float)health.GetValue();
        healthSlider.value = percentage;
        healthText.SetText(currentHealth + " HP");
    }

    void Death()
    {
        characterManager.OnDeath();
        Destroy(gameObject);
    }
}

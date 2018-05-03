using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour {

    private int maxHealth = 100;
    public Slider healthSlider;
    private ICharacterManager characterManager;
    private int currentHealth;

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
            ScaleHealthSlider();
        }
    }

	void Awake ()
    {
        characterManager = GetComponent<ICharacterManager>();
        currentHealth = maxHealth;
        ScaleHealthSlider();
	}

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            if (currentHealth <= 0)
            {
                Death();
            }
        }
    }

    void ScaleHealthSlider()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void Death()
    {
        characterManager.OnDeath();
        Destroy(gameObject);
    }
}

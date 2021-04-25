using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public int maxHealth;
    [SerializeField]
    private int currentHealth;
    public int expWhenDefeated;
    public int Health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            if (value < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth = value;
            }
        }
    }

    private SpriteRenderer _characterRenderer;
    public bool flashActive;
    public float flashLength;
    private float flashCounter;
    // Start is called before the first frame update
    void Start()
    {
        _characterRenderer = GetComponent<SpriteRenderer>();
        UpdateMaxHealth(maxHealth);
    }

    // Update is called once per frame
    private void Update()
    {
        if (flashActive)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter > flashLength * 0.66f)
            {
                ToggleColor(false);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                ToggleColor(true);
            }
            else if (flashCounter > 0)
            {
                ToggleColor(false);
            }
            else
            {
                ToggleColor(true);
                flashActive = false;
            }

        }
    }

    public void DamageCharacter(int damage)
    {

        Health -= damage;
        if (Health <= 0)
        {

            if (gameObject.tag.Equals("Enemy"))
            {
                GameObject.Find("Player").
                          GetComponent<CharacterStats>().
                          AddExperience(expWhenDefeated);
            }

            if (gameObject.name.Equals("Player"))
            {
                //TODO: implementar Game Over
            }

            gameObject.SetActive(false);
        }
        if (flashLength > 0)
        {
            //GetComponent<BoxCollider2D>().enabled = false;
            flashActive = true;
            flashCounter = flashLength;
        }
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }

    void ToggleColor(bool visible)
    {
        _characterRenderer.color = new Color(_characterRenderer.color.r,
                                             _characterRenderer.color.g,
                                             _characterRenderer.color.b,
                                             (visible ? 1.0f : 0.0f));
    }
}

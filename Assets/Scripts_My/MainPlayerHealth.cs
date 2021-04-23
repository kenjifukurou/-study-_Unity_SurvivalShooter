using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; //health
    public int currentHealth;
    
    public Slider healthSlider; //health & damaged UI
    public Image damageFlash;
    public float flashSpeed;
    public Color flashColor = new Color(1f, 0f, 0f, 0.2f);

    Animator anim; //animation
    public AudioClip deathClip; //audio
    AudioSource playerAudio;
    MainPlayerMovement mainPlayerMovement; // player script
    MainPlayerAttack mainPlayerAttack;
    bool isDead; //boolean
    bool damaged;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        mainPlayerMovement = GetComponent<MainPlayerMovement>();
        mainPlayerAttack = GetComponentInChildren<MainPlayerAttack>();
        currentHealth = maxHealth; //setup starting health
    }
    
    void Update() //indicate red screen if damaged
    {
        if (damaged)
        {
            damageFlash.color = flashColor;
        }
        else
        {
            damageFlash.color = Color.Lerp(damageFlash.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }
    
    public void TakeDamage(int amount) //player take damage
    {
        damaged = true; //call red flash screen

        currentHealth -= amount; //minues health
        healthSlider.value = currentHealth; //setup UI slider based on health amount
        
        playerAudio.Play(); //play hurt audio
        
        if (currentHealth <= 0 && !isDead) //check if the player health reached 0
        {
            Death(); //if reach 0, call Death
        }
    }
    
    void Death() //player death
    {
        isDead = true; //isDead toggle

        anim.SetTrigger("Die"); //play animation

        playerAudio.clip = deathClip; //change the audio clip
        playerAudio.Play(); //play death audio

        mainPlayerAttack.DisableEffect(); //disable effects

        mainPlayerMovement.enabled = false; //disable player movement 
        mainPlayerAttack.enabled = false; //disable player attack
    }
}

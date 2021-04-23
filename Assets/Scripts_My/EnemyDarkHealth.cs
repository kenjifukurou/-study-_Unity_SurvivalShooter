using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDarkHealth : MonoBehaviour
{
    public int maxHealth = 60; //health
    public int currentHealth;
    public float sinkSpeed; //animation & dying

    public int scoreValue; //score
    public int scoreBonus;

    public AudioClip enemyDeathClip;
    AudioSource enemyAudio;
    Animator anim;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool enemyDead;
    bool enemySinking;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = maxHealth; //setup starting health
    }
    
    private void Update() //remove the dead body
    {
        if (enemySinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
    
    public void TakeDamage(int amount, Vector3 hitPoint) //enemy take damage
    {
        if (enemyDead) return;

        enemyAudio.Play(); //audio "Hurt"

        currentHealth -= amount; //minus health
        ScoreManagerAM.score += scoreBonus; //bonus score

        hitParticles.transform.position = hitPoint; //setup particles position
        hitParticles.Play(); //show particles
        if (currentHealth <= 0) //call Death if health less than 0
        {
            Death();
        }
    }
    
    void Death() //enemy death
    {
        print("enemy is killed!");
        enemyDead = true;
        
        capsuleCollider.isTrigger = true; //disable collider
        anim.SetTrigger("Dead"); //play enemy death animation
        
        enemyAudio.clip = enemyDeathClip; //audio "Death"
        enemyAudio.Play();
    }
    
    public void Sinking() //sink down the enemy death body, called from animation
    {
        GetComponent<NavMeshAgent>().enabled = false; //disable nav mesh
        GetComponent<Rigidbody>().isKinematic = true;

        ScoreManagerAM.score += scoreValue;
        
        enemySinking = true; //turn on enemySinking
        
        Destroy(gameObject, 2f); //remove gameObject from scene after 2s
    }
}

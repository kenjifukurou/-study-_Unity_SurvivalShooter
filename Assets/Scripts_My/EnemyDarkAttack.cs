using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDarkAttack : MonoBehaviour
{
    public float attackSpeed; //attack power & speed
    public int attackDamage;
    float timer;
    
    Animator anim;
    GameObject player;
    MainPlayerHealth mainPlayerHealth; //require for TakeDamage()
    EnemyDarkHealth enemyDarkHealth;
    bool playerInRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainPlayerHealth = player.GetComponent<MainPlayerHealth>();
        enemyDarkHealth = GetComponent<EnemyDarkHealth>();
        anim = GetComponent<Animator>();
        timer = 0f;
    }
    
    private void OnTriggerEnter(Collider other) //check if collided with player
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }
    
    private void OnTriggerExit(Collider other) //check if player leave
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }
    
    private void Update() //attack the player
    {
        timer += Time.deltaTime; //timer based on time passed in game
        //attack only if: player in range & if enemy is alive
        if (timer >= attackSpeed && playerInRange && enemyDarkHealth.currentHealth > 0)
        {
            Attack();
        }
        //idle if player death
        if (mainPlayerHealth.currentHealth <= 0) 
        {
            anim.SetTrigger("PlayerDead");
        }
    }
    
    private void Attack()
    {
        timer = 0f; //reset timer
        //if player health is more than 0(not die), minus health
        if (mainPlayerHealth.currentHealth > 0)
        {
            //access MainPlayerHealth Function: TakeDamage(int amount)
            mainPlayerHealth.TakeDamage(attackDamage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDarkMovement : MonoBehaviour
{
    Transform player;
    MainPlayerHealth mainPlayerHealth;
    EnemyDarkHealth enemyDarkHealth;
    NavMeshAgent nav;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mainPlayerHealth = player.GetComponent<MainPlayerHealth>();
        enemyDarkHealth = GetComponent<EnemyDarkHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(enemyDarkHealth.currentHealth > 0 && mainPlayerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}

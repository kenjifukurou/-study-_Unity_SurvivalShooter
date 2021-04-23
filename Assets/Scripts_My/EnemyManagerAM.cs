using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerAM : MonoBehaviour
{
    public MainPlayerHealth mainPlayerHealth;
    public GameObject enemy;
    public float spawnTimeStart;
    public float spawnTimeRepeat;
    public Transform[] spawnPoints;

    private void Start()
    {
        InvokeRepeating("Spawn", spawnTimeStart, spawnTimeRepeat);
    }

    private void Spawn()
    {
        if (mainPlayerHealth.currentHealth < 0) return;

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManagerAM : MonoBehaviour
{
    public MainPlayerHealth mainPlayerHealth;
    public float restartDelay;
    float restartTimer;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        restartTimer = 0f;
    }

    private void Update()
    {
        if(mainPlayerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");

            restartTimer += Time.deltaTime;
            if(restartTimer > restartDelay)
            {
                print("Restart Level");
                SceneManager.LoadScene("Scene-My");
            }
        }
    }
}

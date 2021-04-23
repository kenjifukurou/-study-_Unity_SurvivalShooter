using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerAttack : MonoBehaviour
{
    public int damagePerShot; //attack power
    public float attackSpeed; //attack speed
    public float attackRange; //attack range
    float timer; //attack speed timer

    Ray shootRay = new Ray(); //mouse cast ray
    RaycastHit shootHit; //ray hit info

    int attackableMask; //layer mask of attackable object

    ParticleSystem gunParticles; //gun particles effect
    LineRenderer gunLine; //gun trail effect
    Light gunLight; //gun light
    AudioSource gunAudio; //gun audio
    public float effectDisplayTime; //effect display time

    private void Awake()
    {
        //get components
        attackableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        gunAudio = GetComponent<AudioSource>();
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime; //setup timer - increase overtime
        //get input & call Shoot function
        if (Input.GetButton("Fire1") && timer >= attackSpeed)
        {
            Shoot();
        }
        //disable shoot effects
        if (timer >= attackSpeed * effectDisplayTime)
        {
            DisableEffect();
        }
    }

    void Shoot()
    {
        timer = 0f; //reset timer to 0

        gunAudio.Play();
        gunLight.enabled = true;

        gunParticles.Stop(); //stop before play, always ensure do not double play
        gunParticles.Play(); //play particles effect

        gunLine.enabled = true; //enable display of LineRenderer
        gunLine.SetPosition(0, transform.position); //set line starting position 0, at 'this' position
        
        shootRay.origin = transform.position; //Ray starting position
        shootRay.direction = transform.forward; //Ray out direction

        if (Physics.Raycast(shootRay, out shootHit, attackRange, attackableMask)) //if Ray hit something
        {
            //print("Shoot!");
            //get enemy health script & call TakeDamage()
            EnemyDarkHealth enemyDarkHealth = shootHit.collider.GetComponent<EnemyDarkHealth>();
            if(enemyDarkHealth != null) //if the object have enemy health script attached
            {
                enemyDarkHealth.TakeDamage(damagePerShot, shootHit.point); //TakeDamage()
            }
            gunLine.SetPosition(1, shootHit.point); //set LineRenderer index 1 at shootHit position
        }
        else //if nothing is hit
        {
            //print("Miss!");
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * attackRange);
        }
    }

    public void DisableEffect()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}

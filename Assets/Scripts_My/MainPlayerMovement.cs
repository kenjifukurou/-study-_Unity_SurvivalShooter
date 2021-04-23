using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerMovement : MonoBehaviour
{
    public float speed; //movement speed
    private Vector3 movement;
    
    public float camRayLength; //mouse ray distance
    
    private Rigidbody rbPlayer; //animation and physics
    private Animator anim;
    
    public int maskFloor; //layer mask

    void Awake()
    {
        rbPlayer = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        maskFloor = LayerMask.GetMask("Floor");
    }
    
    void FixedUpdate() //get input
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
    }
    
    void Move(float h, float v) //character moving
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rbPlayer.MovePosition(transform.position + movement);
    }
    
    void Turning() //character turning to mouse-aim
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(camRay.origin, camRay.direction * 15, Color.red);

        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, maskFloor))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            //convert position to rotation
            Quaternion playerRotation = Quaternion.LookRotation(playerToMouse);

            rbPlayer.MoveRotation(playerRotation);
        }
    }
    
    void Animating(float h, float v) //character animation
    {
        bool walking = h != 0 || v != 0;
        anim.SetBool("isWalking", walking);
    }
}

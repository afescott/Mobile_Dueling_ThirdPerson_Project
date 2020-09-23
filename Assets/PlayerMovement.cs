using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    //public Animator animator;

    public Joystick joystick;

    public float runSpeed = 40f;
    private Vector2 pointA;
    private Vector2 pointB;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    private bool touchStart = false;

    // Update is called once per frame
    void Update()
    {

        if (joystick.Horizontal >= .2f)
        {
            horizontalMove = runSpeed;
        }
        else if (joystick.Horizontal < -.2f)
        {
            horizontalMove = -runSpeed;
        }
        else
        {
            horizontalMove = 0f;
        }

    var rigidBody = GetComponent<Rigidbody>();

        //rigidBody.velocity = new Vector3(joystick.Horizontal * 100f, rigidBody.velocity.y, joystick.Vertical * 100f);

        horizontalMove = joystick.Horizontal * runSpeed;

        //animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        Vector3 Movement = transform.forward * runSpeed * Time.deltaTime;

        //controller.Move(Movement);

        controller.Move(new Vector3(joystick.Horizontal, rigidBody.velocity.y, joystick.Vertical));

        //for (int i = 0; i < Input.touchCount; i++)
        //{


        //    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);

        //    var input = Input.touches[i];
        //var input = Input.GetTouch(0);

    }
    //if (input.position.x > 0f && input.position.x < 390f) //left side of screen 
    //{

    //if (input.phase == TouchPhase.Began)
    //{


    
        


//if (input.phase == TouchPhase.Moved)
//{


//    horizontalMove = joystick.Horizontal * runSpeed;

//    animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

//    Vector3 Movement = transform.forward * runSpeed * Time.deltaTime;
//    controller.Move(Movement);

//}

//if (Input.GetButtonDown("Jump"))
//{
//	jump = true;
//	animator.SetBool("IsJumping", true);
//}

//if (Input.GetButtonDown("Crouch"))
//{
//	crouch = true;
//} else if (Input.GetButtonUp("Crouch"))
//{
//	crouch = false;
//}



//public void OnLanding ()
//{
//	animator.SetBool("IsJumping", false);
//}

//public void OnCrouching (bool isCrouching)
//{
//	animator.SetBool("IsCrouching", isCrouching);
//}

void FixedUpdate()
    {
        // Move our character
        //controller.Move(horizontalMove * Time.fixedDeltaTime, joystick )



    }
}
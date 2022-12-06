using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Claire : MonoBehaviour {

    private Animator animation_controller;
    private CharacterController character_controller;
    public Vector3 movement_direction;
    public float walking_velocity;
    // public Text text;    
    public float velocity;
    public int num_lives;
    public bool has_won;
    public bool is_crouching;
    // public GameEnds GameEnds;
    

	// Use this for initialization
	void Start ()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        walking_velocity = 2.5f;
        velocity = 0.0f;
        num_lives = 5;
        has_won = false;
        is_crouching = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // text.text = "Lives left: " + num_lives;

        ////////////////////////////////////////////////
        // WRITE CODE HERE:
        // (a) control the animation controller (animator) based on the keyboard input. Adjust also its velocity and moving direction. 
        // (b) orient (i.e., rotate) your character with left/right arrow [do not change the character's orientation while jumping]
        // (c) check if the character is out of lives, call the "death" state, let the animation play, and restart the game
        // (d) check if the character reached the target (display the message "you won", freeze the character (idle state), provide an option to restart the game
        // feel free to add more fields in the class        
        ////////////////////////////////////////////////
        
        // Winning Logic
        if (has_won)
        {
            velocity = 0.0f;
            animation_controller.Play("Idle");
            // GameEnds.Setup("Won!");

        }

        // Death Logic
        if (num_lives == 0 && !has_won)
        {
            velocity = 0.0f;
            animation_controller.SetTrigger("death");
            // GameEnds.Setup("You Lost!");
            
        } 


        //Walking Forward Logic
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if ( Input.GetKey(KeyCode.C) )
            {   
                Debug.Log("UP + C");
                is_crouching = true;
                animation_controller.SetBool("isWalkingForward", false);
                animation_controller.SetBool("isRunningForward", false);
                animation_controller.SetBool("isCrouchingForward", true);
                


            }
            else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                Debug.Log("UP + Shift");
                animation_controller.SetBool("isRunningForward", true);
                animation_controller.SetBool("isWalkingForward", false);
                animation_controller.SetBool("isCrouchingForward", false);
                

            }
            else 
            {
                Debug.Log("UP");
                animation_controller.SetBool("isCrouchingForward", false);
                animation_controller.SetBool("isRunningForward", false);
                animation_controller.SetBool("isWalkingForward", true);

            }
        }
        else
        {
            animation_controller.SetBool("isWalkingForward", false);
            animation_controller.SetBool("isCrouchingForward", false);
            animation_controller.SetBool("isRunningForward", false);
        }



        // Walking Backward Logic
        if (Input.GetKey(KeyCode.DownArrow)) 
        {
        if (Input.GetKey(KeyCode.C)) 
            {
                Debug.Log("DOWN + C");
                is_crouching = true;
                animation_controller.SetBool("isWalkingBackward", false);
                animation_controller.SetBool("isCrouchingBackward", true);
            }
            else
            {
                Debug.Log("DOWN");
                animation_controller.SetBool("isCrouchingBackward", false);
                animation_controller.SetBool("isWalkingBackward", true);
            }
        }
        else
        {
            animation_controller.SetBool("isWalkingBackward", false);
            animation_controller.SetBool("isCrouchingBackward", false);
        }


        // Jump Over Logic
        if (Input.GetKey(KeyCode.Space) && animation_controller.GetCurrentAnimatorStateInfo(0).IsName("RunForwards"))
        {
            Debug.Log("Jump");
            animation_controller.SetTrigger("Jump");
        }

        // Rotation Logic
        if (Input.GetKey(KeyCode.LeftArrow) && !(has_won || animation_controller.GetCurrentAnimatorStateInfo(0).IsName("JumpOver") || animation_controller.GetCurrentAnimatorStateInfo(0).IsName("Death")))
        {
            transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f));
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !(has_won || animation_controller.GetCurrentAnimatorStateInfo(0).IsName("JumpOver") || animation_controller.GetCurrentAnimatorStateInfo(0).IsName("Death")))
        {
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
        }

        // 
        if (animation_controller.GetBool("death")) 
        {
            animation_controller.Play("Death");
            velocity = 0.0f;
            if (animation_controller.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7501)
            {
                animation_controller.speed = 0;
                // GameEnds.Setup("You Lost!");
                

            }
           

        }

        else if (animation_controller.GetBool("Jump"))  
        {
            
            velocity += 0.1f;
            if (velocity > walking_velocity*3.0f)
                velocity = walking_velocity*3.0f;
           
            if (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("JumpOver") && animation_controller.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.92 ){
                animation_controller.SetBool("Jump", false);
               
            }
        }

        else if (animation_controller.GetBool("isRunningForward")) 
        {
            animation_controller.Play("RunForwards");
            velocity += 0.2f;
            if (velocity > walking_velocity*2.0f)
                velocity = walking_velocity*2.0f;



        }

        else if (animation_controller.GetBool("isWalkingForward")) 
        {
            // animation_controller.Play("WalkingForward");
            velocity += 0.1f;
            if (velocity > walking_velocity)
                velocity = walking_velocity;

        }

        else if (animation_controller.GetBool("isWalkingBackward")) 
        {
            // animation_controller.Play("WalkingBackwards");
            velocity -= 0.1f;
            if (velocity < -walking_velocity/1.5f)
                velocity = -walking_velocity/1.5f;

        }

        else if (animation_controller.GetBool("isCrouchingForward")) 
        {
            animation_controller.Play("CrouchForwards");
            velocity += 0.1f;
            if (velocity > walking_velocity/2.0f)
                velocity = walking_velocity/2.0f;

        }

        else if (animation_controller.GetBool("isCrouchingBackward")) 
        {
            animation_controller.Play("CrouchBackwards");
            velocity -= 0.1f;
            if (velocity < -walking_velocity/2.0f)
                velocity = -walking_velocity/2.0f;

        }
        else 
        {
            animation_controller.Play("Idle");
            velocity = 0.0f;

        }


        // you don't need to change the code below (yet, it's better if you understand it). Name your FSM states according to the names below (or change both).
        // do not delete this. It's useful to shift the capsule (used for collision detection) downwards. 
        // The capsule is also used from turrets to observe, aim and shoot (see Turret.cs)
        // If the character is crouching, then she evades detection. 
        // bool is_crouching = false;
        if ( (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("CrouchForwards"))
         ||  (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("CrouchBackwards")) )
        {
            is_crouching = true;
        }

        if (is_crouching)
        {
            GetComponent<CapsuleCollider>().center = new Vector3(GetComponent<CapsuleCollider>().center.x, 0.0f, GetComponent<CapsuleCollider>().center.z);
        }
        else
        {
            GetComponent<CapsuleCollider>().center = new Vector3(GetComponent<CapsuleCollider>().center.x, 0.9f, GetComponent<CapsuleCollider>().center.z);
        }

        // you will use the movement direction and velocity in Turret.cs for deflection shooting 
        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        movement_direction = new Vector3(xdirection, 0.0f, zdirection);

        // character controller's move function is useful to prevent the character passing through the terrain
        // (changing transform's position does not make these checks)
        if (transform.position.y > 0.0f) // if the character starts "climbing" the terrain, drop her down
        {
            Vector3 lower_character = movement_direction * velocity * Time.deltaTime;
            lower_character.y = -100f; // hack to force her down
            character_controller.Move(lower_character);
        }
        else
        {
            character_controller.Move(movement_direction * velocity * Time.deltaTime);
        }
    }                    
}

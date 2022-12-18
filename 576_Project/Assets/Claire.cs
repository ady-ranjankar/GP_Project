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
    public float running_velocity;
        
    public float velocity;
    public int num_lives;
    public bool has_won;
    
    public float speed = 600.0f;
    public float turnSpeed = 200.0f;
    
    public float timer;
    float y_axis;
    public CreateEnvironment create;
    public int down_move;
	// Use this for initialization
	void Start ()
    {
        down_move = 0;
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        walking_velocity = 1.5f;
        running_velocity = 3.5f;
        velocity = 0.0f;
        num_lives = 5;
        has_won = false;
        timer = 0;
        GameObject create_env = GameObject.Find("Environment");
        create = create_env.GetComponent<CreateEnvironment>();
    }

    // Update is called once per frame
    void Update()
    {
        if(down_move == 1)
        {
            movement_direction = new Vector3(0.0f, -20.0f, 0.0f);
            Vector3 lower_character = movement_direction * 1.0f * Time.deltaTime;
            
            character_controller.Move(lower_character);
        }
            

        int check = 0;
        ////////////////////////////////////////////////
        // WRITE CODE HERE:
        // (a) control the animation controller (animator) based on the keyboard input. Adjust also its velocity and moving direction. 
        // (b) orient (i.e., rotate) your character with left/right arrow [do not change the character's orientation while jumping]
        // (c) check if the character is out of lives, call the "death" state, let the animation play, and restart the game
        // (d) check if the character reached the target (display the message "you won", freeze the character (idle state), provide an option to restart the game
        // feel free to add more fields in the class        
        ////////////////////////////////////////////////

        bool forward = Input.GetKey(KeyCode.UpArrow);
        bool backward = Input.GetKey(KeyCode.DownArrow);
        bool crouch = Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl);
        bool running = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);

        if(create.timeRemaining <= 0.0f){
            
            animation_controller.SetBool("isWalking", false);
            animation_controller.SetBool("isCrouch", false);
            animation_controller.SetBool("isRunning", false);
            animation_controller.SetBool("isJump", false);
            velocity = 0.0f;
            if (transform.position.y > 0.00f) // if the character starts "climbing" the terrain, drop her down
            {
                Vector3 lower_character = movement_direction * velocity * Time.deltaTime;
                lower_character.y = -2; // hack to force her down
                character_controller.Move(lower_character);
            }
        }
        
        
         
        else{ 
            if (!forward && !backward){
                velocity = 0.0f;
            }

            if (forward){
                animation_controller.SetBool("isWalking", true);
                if (!running && !jump && !crouch){
                    if (velocity >= walking_velocity){   
                        velocity = walking_velocity;
                    }
                    else{
                        velocity += 0.1f;
                    }
                }
            }
            else{
                animation_controller.SetBool("isWalking", false);
            }


            if (crouch){
                animation_controller.SetBool("isCrouch", true);
            }
            else{
                animation_controller.SetBool("isCrouch", false);
                
            }

            if (running){
                animation_controller.SetBool("isRunning", true);
            }
            else{
                animation_controller.SetBool("isRunning", false);
                
            }
        
            if (jump){
                animation_controller.SetBool("isJump", true);
            }
            else{
                animation_controller.SetBool("isJump", false);
                
            }
        


            if (forward && crouch){
                
                if (velocity >= walking_velocity/2.0f){   
                    velocity = walking_velocity/2.0f;
                }
                else{
                    velocity += 0.05f;
                }
            }


            if (forward && running){
                
                if (velocity >= walking_velocity * 2.0f){   
                    velocity = walking_velocity * 2.0f;
                }
                
                else{
                    velocity += 0.2f;
                }
            }

            if (jump){
                
                velocity = walking_velocity * 3.0f;
                
            }

            
            

            if (backward){
                animation_controller.SetBool("IsBack", true);
                if (!crouch){
                    if (velocity*-1 >= walking_velocity/1.5f){   
                        velocity = -1* walking_velocity/1.5f;
                    }
                    else{
                        velocity -= 0.05f;
                    }
                }

            }
            else{
                
                animation_controller.SetBool("IsBack", false);
            }


            if (backward && crouch){
                if (velocity*-1 >= walking_velocity/2.0f){   
                    velocity = -1 * walking_velocity/2.0f;
                }
                else{
                    velocity -= 0.05f;
                }
            }

            if(animation_controller.GetCurrentAnimatorStateInfo(0).IsName("JumpOver")){
                velocity = walking_velocity * 3.0f;
                y_axis = 0.0f;

            }
            else
                y_axis = transform.position.y;





            if (forward && backward){
                
                velocity = 0.0f;
            }
            
            
            
            
            float turn = Input.GetAxis("Horizontal");
            if (!animation_controller.GetCurrentAnimatorStateInfo(0).IsName("JumpOver")){
                transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

            }
            




            // you don't need to change the code below (yet, it's better if you understand it). Name your FSM states according to the names below (or change both).
            // do not delete this. It's useful to shift the capsule (used for collision detection) downwards. 
            // The capsule is also used from turrets to observe, aim and shoot (see Turret.cs)
            // If the character is crouching, then she evades detection. 
            bool is_crouching = false;
            if ( (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("CrouchForward"))
            ||  (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("CrouchBackward")) )
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
            movement_direction = new Vector3(xdirection,y_axis , zdirection);
            
           
           
            
            // character controller's move function is useful to prevent the character passing through the terrain
            // (changing transform's position does not make these checks)
            if (transform.position.y > -1.75f) // if the character starts "climbing" the terrain, drop her down
            {
                Vector3 lower_character = movement_direction * velocity * Time.deltaTime;
                lower_character.y = -100f; // hack to force her down
                character_controller.Move(lower_character);
            }
            
            else if(velocity > 0.0f)
            {
                character_controller.Move(movement_direction * velocity * Time.deltaTime);
                
            }
        }
    }                    
}

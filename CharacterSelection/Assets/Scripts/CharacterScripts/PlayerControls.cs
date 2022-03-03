using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    CharacterSpawn characterInfo; //this holds the size and maybe more info for character.
    private PlayerInput input;
//    private Vector2 movementVector;
    public Vector2 characterSize;
    public BoxCollider2D _collider;
    public Rigidbody2D _rigidbody;

    public GameObject player;
    float originalXScale;                   //Original scale on X axis
    
    
    [Header("Move Value")]
    public bool moveModifyPressed;
    public int direction = 1;
    private Vector2 tempMovement;

    public float horizontal;
    public float vertical;
   
    public float speed = 4.2f;                //Player speed
    public float walkSpeed = 4.2f;                //Player speed
    public float jumpForce = 25f;
    public float runSpeed = 6.2f;

    public bool isOnGround;
    public bool running = false;
    public bool walking;
    public bool atStore = false;
    public bool atSign = true;

    [Header("Jump stuff")]
    public bool jumpPressed;
    bool jumpCoolingDown = false;
    public bool isJumping;

    public float maxFallSpeed = -25f;
    public float coyoteDuration = .05f;     //How long the player can jump after falling
    float coyoteTime;                       //Variable to hold coyote duration

    [Header("Quest Stuff")]
    public bool interactPressed;
    public GameObject prompt;
    private Collider2D store;
    public GameObject money;
    public GameObject signPool;
    public int moneyCount;

    public List<GameObject> tasks;
    public List<GameObject> activejobs;
    //energy system
    public GameObject meter;
    public EnergyDrain drain;


    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        drain = meter.GetComponent<EnergyDrain>();
        running = false;
        atStore = false;
        isJumping = false;
        prompt.SetActive(false);
        drain.currentEffort = EffortType.None;
        moneyCount = int.Parse(money.GetComponent<Text>().text);
        tasks = signPool.GetComponent<JobSignPool>().tasks;
        originalXScale = transform.localScale.x;

    }

    // Update is called once per frame
void SetColliderSize()    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        tempMovement = context.ReadValue<Vector2>();
        horizontal = tempMovement.x;
        vertical = tempMovement.y;
    }


    public void OnJump(InputAction.CallbackContext context)
    {
            jumpPressed = context.ReadValueAsButton();
    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        
        interactPressed = context.ReadValueAsButton();
    }




    /**
    public void OnMove(InputValue movementValue)
    {
        if (walking)
        {
            walking = false;
            movementVector = Vector2.zero;
        }
        else
        {
            walking = true;
            movementVector = movementValue.Get<Vector2>();
        }
    }
    */



    //public void OnJump()
    //{
    //    isJumping = true;
    //    drain.currentEffort = EffortType.Jump;
    //}

    public void OnInteraction()
    {
        if (atStore)
        {
            if (moneyCount >= store.GetComponent<food>().cost)
            {
                drain.currentFood = store.GetComponent<food>().foodEat;
                drain.eat = true;
                moneyCount -= store.GetComponent<food>().cost;
            }
        }
    }

    public void OnSprint()
    {
        if (running)
        {
            running = false;
        }
        else if (walking)
        {
            running = true;
        }
    }

    void FixedUpdate()
    {
        atSign = false;
        atStore = false;


        money.GetComponent<Text>().text = moneyCount.ToString();

        horizontal = tempMovement.x;
        vertical = tempMovement.y;

        //Calculate the desired velocity based on inputs
        float xVelocity = speed * horizontal;

        if (xVelocity * direction < 0f)
            FlipCharacterDirection();

        if (isJumping)
        {
            //    float translation = 10f;
            //    player.GetComponent<Rigidbody2D>().velocity += new Vector2(0, translation);
            //    isJumping = false;
            //    drain.currentEffort = EffortType.None;
        }

        if (interactPressed && atSign)
        {
            int range = Random.Range(1, 3);
            int i = 0;
            int index = Random.Range(0, tasks.Count - 1);
            JobType jobType;
            //col.gameObject.SetActive(false);
            if (range == 1)
            {
                jobType = JobType.Great;
            }
            else if (range == 2)
            {
                jobType = JobType.Good;
            }
            else
            {
                jobType = JobType.Poor;
            }
            while (i < range)
            {
                while (tasks[index].activeSelf == true)
                {
                    index = Random.Range(0, tasks.Count - 1);
                }
                tasks[index].SetActive(true);
                tasks[index].GetComponent<Job>().jobType = jobType;
                activejobs.Add(tasks[index]);
                i++;
            }
        }
    }


    /// <summary>
    /// Change the characters direction for facing orientation
    /// </summary>
    void FlipCharacterDirection()
    {
        //Turn the character by flipping the direction
        direction *= -1;

        //Record the current scale
        Vector3 scale = transform.localScale;

        //Set the X scale to be the original times the direction
        scale.x = originalXScale * direction;

        //Apply the new scale
        transform.localScale = scale;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Food"))
        {
            prompt.SetActive(true);
            atStore = true;
            store = col;
        }
        //else {atStore = false; }

        if (col.CompareTag("Sign"))
        {
            atSign = true;
        }
        //else{ atSign = false; }

        if (col.CompareTag("Job"))
        {
            GameObject obj;
            int i = 0;
            col.gameObject.SetActive(false);
            while (i<activejobs.Count)
            {
                obj = activejobs[i];
                if(col.gameObject == obj)
                {
                    activejobs.Remove(obj);
                    if (activejobs.Count == 0)
                    {
                        moneyCount += obj.GetComponent<Job>().cost;
                    }
                }
                i++;
            }

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Food"))
        {
            prompt.SetActive(false);
            atStore = false;
            store = null;
            col.isTrigger = false;
        }
        if (col.CompareTag("Sign"))
        {
            atSign= false;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Food"))
        {
            col.collider.isTrigger = true;
        }
    }
}

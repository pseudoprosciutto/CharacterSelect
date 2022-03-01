using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction jump;
    private InputAction move;
    private InputAction interact;
    private InputAction sprint;
    private Vector2 movementVector;

    public GameObject meter;
    public EnergyDrain drain;
    public GameObject player;
    public bool running;
    public bool walking;
    public bool atStore;
    public bool isJumping;
    public GameObject prompt;
    private Collider2D store;
    public GameObject money;
    public GameObject signPool;
    public int moneyCount;

    public List<GameObject> tasks;
    public List<GameObject> activejobs;

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
        //gamepad = InputSystem.AddDevice<Gamepad>();
    }

    // Update is called once per frame
    void Update()
    {
        if (walking)
        {
            if (running)
            {
                float move = movementVector.x * 40 * Time.deltaTime;
                player.GetComponent<Rigidbody2D>().velocity += new Vector2(move, 0);
            }
            else
            {
                float move = movementVector.x * 10 * Time.deltaTime;
                player.GetComponent<Rigidbody2D>().velocity += new Vector2(move, 0);
            }
        }
    }

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

    public void OnJump()
    {
        isJumping = true;
        drain.currentEffort = EffortType.Jump;
    }

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
        money.GetComponent<Text>().text = moneyCount.ToString();
        if (isJumping)
        {
            float translation = 10f;
            player.GetComponent<Rigidbody2D>().velocity += new Vector2(0, translation);
            isJumping = false;
            drain.currentEffort = EffortType.None;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Food"))
        {
            prompt.SetActive(true);
            atStore = true;
            store = col;
        }
        if (col.CompareTag("Sign"))
        {
            int range = Random.Range(1, 3);
            int i = 0;
            int index = Random.Range(0, tasks.Count - 1);
            JobType jobType;
            col.gameObject.SetActive(false);
            if (range == 1)
            {
                jobType = JobType.Great;
            }
            else if(range == 2)
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
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Food"))
        {
            col.collider.isTrigger = true;
        }
    }
}

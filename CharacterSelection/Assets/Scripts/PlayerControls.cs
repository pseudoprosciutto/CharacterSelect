using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public GameObject meter;
    public EnergyDrain drain;
    public GameObject player;
    public bool running;
    public bool atStore;
    public bool isJumping;
    public GameObject prompt;
    private Collider2D store;
    public GameObject money;
    public GameObject signPool;
    public int moneyCount;
    public GameObject Pointer;
    public List<GameObject> activejobs;
    public List<GameObject> tasks;
    public List<GameObject> activejobs;

    //var gamepad;

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
        if (!Keyboard.current.leftShiftKey.isPressed)
        {
            running = false;
            //drain.currentEffort = EffortType.Walk;
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                drain.currentEffort = EffortType.Walk;
                float translation = -1 * 10 * Time.deltaTime;
                player.GetComponent<Rigidbody2D>().velocity += new Vector2(translation, 0);
            }
            else if (Keyboard.current.rightArrowKey.isPressed)
            {
                drain.currentEffort = EffortType.Walk;
                float translation = 1 * 10 * Time.deltaTime;
                player.GetComponent<Rigidbody2D>().velocity += new Vector2(translation, 0);
            }
        }
        else if (Keyboard.current.leftShiftKey.isPressed)
        {
            running = true;
            drain.currentEffort = EffortType.Run;
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                float translation = -1 * 40 * Time.deltaTime;
                player.GetComponent<Rigidbody2D>().velocity += new Vector2(translation, 0);
            }
            else if (Keyboard.current.rightArrowKey.isPressed)
            {
                float translation = 1 * 40 * Time.deltaTime;
                player.GetComponent<Rigidbody2D>().velocity += new Vector2(translation, 0);
            }
        }
        //else if (!gamepad.current.leftTrigger.isPressed)
        //{
        //    running = false;
        //    drain.currentEffort = EffortType.Walk;
        //    float move = gamepad.leftStick.x * 40 * Time.deltaTime;
        //    player.GetComponent<Rigidbody2D>().velocity += new Vector2(move, 0);
        //}
        //else if (gamepad.current.leftTrigger.isPressed)
        //{
        //    running = true;
        //    drain.currentEffort = EffortType.Run;
        //    float move = gamepad.leftStick.x * 40 * Time.deltaTime;
        //    player.GetComponent<Rigidbody2D>().velocity += new Vector2(move, 0);
        //}

        if (isJumping)
        {
            float translation = 12f;
            player.GetComponent<Rigidbody2D>().velocity += new Vector2(0, translation);
            isJumping = false;
            drain.currentEffort = EffortType.None;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isJumping = true;
            drain.currentEffort = EffortType.Jump;
        }
        //else if (gamepad.current.buttonSouth.wasPressedThisFrame)
        //{
        //    isJumping = true;
        //    drain.currentEffort = EffortType.Jump;
        //}

        if (Keyboard.current.eKey.wasPressedThisFrame)
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
        //else if (gamepad.current.buttonWest.wasPressedThisFrame)
        //{
        //    if (atStore)
        //    {
        //        if (moneyCount >= store.GetComponent<food>().cost)
        //        {
        //            drain.currentFood = store.GetComponent<food>().foodEat;
        //            drain.eat = true;
        //            moneyCount -= store.GetComponent<food>().cost;
        //        }
        //    }
        //}
    }

    void FixedUpdate()
    {
        money.GetComponent<Text>().text = moneyCount.ToString();
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

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

    public BoxCollider2D playerCollider;





    public List<GameObject> tasks;

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

        if (isJumping)
        {
            float translation = 12f;
            player.GetComponent<Rigidbody2D>().velocity += new Vector2(0, translation);
            Debug.Log(player.GetComponent<Rigidbody2D>().velocity);
            isJumping = false;
            drain.currentEffort = EffortType.None;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isJumping = true;
            drain.currentEffort = EffortType.Jump;
        }

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
            col.gameObject.SetActive(false);
            tasks[Random.Range(0, tasks.Count - 1)].SetActive(true);
        }
        if (col.CompareTag("Job"))
        {
            col.gameObject.SetActive(false);
            moneyCount += col.GetComponent<Job>().cost;
        }
    }

    void OnCollision2D(Collider2D col)
    {
        if (col.CompareTag("Food"))
        {
            col.isTrigger = false;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    public GameObject meter;
    public EnergyDrain drain;
    public GameObject player;
    public bool running;
    public bool atStore;
    public GameObject prompt;
    private Collider2D store;
    public GameObject money;
    public int moneyCount;

    // Start is called before the first frame update
    void Start()
    {
        drain = meter.GetComponent<EnergyDrain>();
        running = false;
        atStore = false;
        prompt.SetActive(false);
        drain.currentEffort = EffortType.None;
        moneyCount = int.Parse(money.GetComponent<Text>().text);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Horizontal") != 0)
        {
            running = false;
            drain.currentEffort = EffortType.Walk;
            float translation = Input.GetAxis("Horizontal") * 10 * Time.deltaTime;
            player.GetComponent<Rigidbody2D>().velocity += new Vector2(translation, 0);
        }
        else if (Input.GetKey(KeyCode.LeftShift)){
            running = true;
            drain.currentEffort = EffortType.Run;
            float translation = Input.GetAxis("Horizontal") * 40 * Time.deltaTime;
            player.GetComponent<Rigidbody2D>().velocity += new Vector2(translation, 0);
        }

        if (Input.GetKeyUp(KeyCode.Space)){
            drain.currentEffort = EffortType.Jump;
            float translation = 3000 * Time.deltaTime;
            player.GetComponent<Rigidbody2D>().velocity += new Vector2(0, translation);
        }

        if (Input.GetKeyUp(KeyCode.E))
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
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Food"))
        {
            prompt.SetActive(false);
            atStore = false;
            store = null;
        }
    }
}

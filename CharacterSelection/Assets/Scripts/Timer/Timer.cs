using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject timer;
    public int time;

    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Tick", 1f, 1f);
    }

    public void Tick()
    {
        time -= 1;
        timer.GetComponent<Text>().text = time.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

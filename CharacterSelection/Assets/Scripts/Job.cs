using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JobType
{
    Great,Good,Poor
}

public class Job : MonoBehaviour
{
    public JobType jobType;
    public int cost;

    // Start is called before the first frame update
    void Awake()
    {
        if (jobType == JobType.Great)
        {
            cost = 25;
        }
        else if (jobType == JobType.Good)
        {
            cost = 15;
        }
        else if (jobType == JobType.Poor)
        {
            cost = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

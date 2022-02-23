using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Total collection of task locations in the level
/// </summary>
public class TaskLocationPool : MonoBehaviour
{
    TaskLocation[] TaskLocations;
    // Start is called before the first frame update

    public int TaskCount
    {
        get { return TaskLocations.Length; }
    }
    public TaskLocation GetTaskLocation(int index)
    {
        return TaskLocations[index];
    }
    public bool TaskActive(TaskLocation taskLocation) { return taskLocation.isActive; }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Three types of quest types hard is 3, med is 2, easy is 1 task.
/// </summary>
public enum QuestType { Hard, Med, Easy  }

[System.Serializable]
public class Quest
{
   public string Title;
   public string Description;
   public bool Completed = false;
   public int Required;
   public Task[] Tasks;

    /// <summary>
    /// when a quest is created it needs to understand how difficult it is and where its tasks go to. 
    /// </summary>
    public Quest()
    {
    }

}

[System.Serializable]
public class Task
{
    bool isActive;
    Transform location;
    Waypoint waypoint;
}

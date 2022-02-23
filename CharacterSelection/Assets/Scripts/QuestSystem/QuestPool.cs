using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPool //: ScriptableObject
{

    //dictionary or list

    public Quest[] quests;
    public int QuestCount
    {
        get { return quests.Length; }
    }
    public Quest GetQuest(int index)
    {
        return quests[index];
    }
    public void RemoveQuest(int index)
    {
        
    }
    //remove by Quest;
    public void RemoveQuest(Quest quest) { }

    public void AddQuest(Quest quest)
    {

    }
}

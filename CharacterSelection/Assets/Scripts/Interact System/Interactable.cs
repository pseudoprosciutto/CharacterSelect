using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // an interactable doesnt always have to be interactable
    public bool isInteractable { get; protected set; }
    //is being interacted with
    public bool isInteractedWith { get; protected set; }

    //to be overwritten
    public virtual void Interact()
    {
        //this script wont show unless using this class as a test.
        Debug.Log("Object recognizes - interact test");
        //            character.isInteracting_Test = true;
    }
}

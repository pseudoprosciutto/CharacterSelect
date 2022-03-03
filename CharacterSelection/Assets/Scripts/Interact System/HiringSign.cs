using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiringSign : Interactable
{   // Start is called before the first frame update
    public bool isHiring;
    public bool playerIsHired;


    public override void Interact()
    {
        print("Player at Hiring sign Interacting");
        if (isHiring)
        {
            print("Player was hired for a job");
        }
        else
        {
            print("We are not hiring");
        }
    }
}

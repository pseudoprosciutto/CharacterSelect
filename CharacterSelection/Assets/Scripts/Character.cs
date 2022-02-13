using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ScriptableObject
{
    //some of these attributes are just examples or ideas, not all necessary to implement right now
    public string charName; //this is if we have a name input for select char/ save
    public CharacterModel model;

    public float jumpHeight;
    public float runSpeed;
    public float walkSpeed;
    public int Score;

    public int EnergyLevel;
    public int EnergyMod;



    public enum HungerState { Hungry, Sated, Starving, Full };
    public HungerState hungerState;

    [Space]
    public int currentLevel;
    public Waypoint CurrentWaypoint;

    PlayerInput input;
    PlayerPrefs playerPrefs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}

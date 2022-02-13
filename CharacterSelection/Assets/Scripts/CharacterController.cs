using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the players character controller for gameplay
/// </summary>
public class Character
{
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


}

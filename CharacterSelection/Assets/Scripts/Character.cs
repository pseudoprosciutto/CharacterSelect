using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the players character controller for gameplay
/// </summary>
public class Character : MonoBehaviour
{ 
   public CharacterModel model;
    public Sprite sprite;
    public int Score;
    SpriteRenderer spriteRenderer;

    public float jumpHeight;
    public float runSpeed;
    public float walkSpeed;




    [Space]
    public int currentLevel;
    public Waypoint CurrentWaypoint;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void LoadSprite()
    {
        spriteRenderer.sprite = model.characterSprite;
        
    }


}

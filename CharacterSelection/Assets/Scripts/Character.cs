using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This is the players character controller for gameplay
/// </summary>
public class Character : MonoBehaviour
{
    private Vector2 tempMovement;
    //private InputControls controls;
    float horizontal;
    float vertical;
    bool readyToClear;                              //Bool used to keep input in sync

 //   Input input;
   public CharacterModel model;
    public Sprite sprite;
    public int Score;
    SpriteRenderer spriteRenderer;


    public float jumpHeight;
    public float runSpeed;
    public float walkSpeed;

    BoxCollider2D boxCollider;


    [Space]
    public int currentLevel;
    public Waypoint CurrentWaypoint;
 
    public void OnMove(InputAction.CallbackContext context)
    {

        tempMovement = context.ReadValue<Vector2>();
        horizontal = tempMovement.x;
        vertical = tempMovement.y;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void LoadSprite()
    {
        spriteRenderer.sprite = model.characterSprite;        
    }

    
}

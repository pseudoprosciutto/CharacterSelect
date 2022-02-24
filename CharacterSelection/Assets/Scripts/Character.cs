using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the players character visual look animation and states
/// </summary>
public class Character : MonoBehaviour
{ 
   public CharacterModel model;
    public Sprite idleSprite;
    
    SpriteRenderer spriteRenderer;

    
    



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
        SetSpriteBounds();
    }

    void SetSpriteBounds() { }

    

}

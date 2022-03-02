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
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void LoadSprite()
    {
        spriteRenderer.sprite = model.characterSprite;

      			//Record the player's height from the collider
	//		playerHeight = bodyCollider.size.y;
    }
}

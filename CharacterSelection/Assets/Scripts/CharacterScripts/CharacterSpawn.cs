using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the players character Spawn
/// </summary>
public class CharacterSpawn : MonoBehaviour
{
    public CharacterModel model;
    public Sprite sprite;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    public float playerHeight;                     //Height of the player
    public Vector2 characterSize;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        
    }

    public void LoadSprite()
    {
        sprite = spriteRenderer.sprite = model.characterSprite;
        characterSize = new Vector2(sprite.bounds.size.x, sprite.bounds.size.y);
        boxCollider.transform.position = spriteRenderer.transform.position;
        boxCollider.offset.Set(0f, .3f);
        boxCollider.size.Set(characterSize.x, characterSize.y);
        boxCollider.bounds.size.Set(characterSize.x, characterSize.y, 1);
        //			//Record the player's height from the collider
		playerHeight = boxCollider.size.y;
        
    }

}

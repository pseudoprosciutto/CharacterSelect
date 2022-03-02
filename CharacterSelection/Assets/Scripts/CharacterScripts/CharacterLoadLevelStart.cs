using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grabs from Object pool get character model and saved data to begin level
/// Must be on character game object.
/// </summary>
[RequireComponent(typeof(Character))]
public class CharacterLoadLevelStart : MonoBehaviour
{
    public GameObject characterControllerPrefab;

    public CharacterPool characterPool;
    
    public Character character;

    public int characterNumber;

    //test to visually show it working

 
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
        LoadCharacter();
    }


    private void LoadCharacter()
    {
     characterNumber =  PlayerPrefs.GetInt("selectedCharacter");

       character.model =  characterPool.GetCharacterModel(characterNumber);
       character.LoadSprite();
    }
}

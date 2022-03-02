using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grabs from Object pool get character model and saved data to begin level
/// Must be on character game object.
/// </summary>
[RequireComponent(typeof(CharacterSpawn))]
public class CharacterLoadLevelStart : MonoBehaviour
{
    public GameObject characterControllerPrefab;

    public CharacterPool characterPool;
    
    public CharacterSpawn character;

    public int characterNumber;

    //test to visually show it working

 
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterSpawn>();
        LoadCharacter();
    }


    private void LoadCharacter()
    {
     characterNumber =  PlayerPrefs.GetInt("selectedCharacter");

       character.model =  characterPool.GetCharacterModel(characterNumber);
       character.LoadSprite();
    }
}

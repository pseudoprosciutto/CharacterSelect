using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// manages character pool for create character select system
/// </summary>
public class CharacterSelect : MonoBehaviour
{
    public CharacterPool characterPool;
    public Text nameText;
    public SpriteRenderer artworkSprite;


   // public CharacterModel[] characters;
    public int characterIndex = 0 ;

    public SceneChange sceneChange;
    public float charChangeCoolDownTime = 1f;
    
    public bool ChangeCharCooledDown = true;

    /// <summary>
    /// grab charactermodel from pool
    /// </summary>
    /// <param name="index"></param>
    public void UpdateCharacter(int index)
    {
        CharacterModel character = characterPool.GetCharacterModel(index);
        artworkSprite.sprite = character.characterSprite;
        nameText.text = character.characterName;

    }

    /// <summary>
    /// Summon the next character to choose
    /// </summary>
    public void NextCharacter()
    {
        if (!ChangeCharCooledDown) { return;  }
        StartCoroutine(ChangeCharCoolingDown());
        print("Next Character");
        StartCoroutine(SwitchCharacters((characterIndex + 1) % characterPool.CharacterCount));
    }

    /// <summary>
    /// Summon the previous character to choose
    /// </summary>
    public void PreviousCharacter()
    {
        if (!ChangeCharCooledDown) { return; }
        StartCoroutine(ChangeCharCoolingDown());
        print("Prev Character");
        StartCoroutine(SwitchCharacters((characterIndex + 1) % characterPool.CharacterCount));
    }

    /// <summary>
    /// Launch GamePlay Scene
    /// </summary>
    void BeginGame()
    {
        int selectedCharacter = characterIndex;
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        sceneChange.LoadNextLevel();
    }

    /// <summary>
    /// prevent spamming of change character
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeCharCoolingDown()
    {
        ChangeCharCooledDown = false;
        yield return new WaitForSeconds(charChangeCoolDownTime);
        print("character change cooled down");
        ChangeCharCooledDown = true;
    }

    //thinking about a generic fade in and out of sprites in animation maybe?
    //the process of switching characters 

    /// <summary>
    /// coroutine to switch characters in case we need time and animation
    /// </summary>
    /// <param name="nextCharacter"></param>
    /// <returns></returns>
    IEnumerator SwitchCharacters(int nextCharacter)
    {
        UpdateCharacter(nextCharacter);
        yield return null;
    }
}

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
    public Text modelNameText;
    public SpriteRenderer modelSprite;
    public int characterIndex = 0 ;
    public SceneChange sceneChange;
    public float charChangeCoolDownTime = 1f;  
    public bool changeCharCooledDown = true;

    /// <summary>
    /// grab charactermodel from pool
    /// </summary>
    /// <param name="index"></param>
    public void UpdateCharacter(int index)
    {
        CharacterModel character = characterPool.GetCharacterModel(index);
        modelSprite.sprite = character.characterSprite;
        modelNameText.text = character.characterName;

    }

    /// <summary>
    /// Summon the next character to choose
    /// </summary>
    public void NextCharacter()
    {
        if (!changeCharCooledDown) { return;  }
        StartCoroutine(ChangeCharCoolingDown());
        print("Next Character");
        StartCoroutine(SwitchCharacters((characterIndex + 1) % characterPool.CharacterCount));
    }

    /// <summary>
    /// Summon the previous character to choose
    /// </summary>
    public void PreviousCharacter()
    {
        if (!changeCharCooledDown) { return; }
        StartCoroutine(ChangeCharCoolingDown());
        print("Prev Character");
        StartCoroutine(SwitchCharacters((characterIndex + 1) % characterPool.CharacterCount));
    }

    /// <summary>
    /// Select character saves to playerprefs and change scene begins
    /// </summary>
    void ChooseCharacter()
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
        changeCharCooledDown = false;
        yield return new WaitForSeconds(charChangeCoolDownTime);
        print("character change cooled down");
        changeCharCooledDown = true;
    }

    /// <summary>
    /// switch characters using time for animation
    /// </summary>
    /// <param name="nextCharacter"></param>
    /// <returns></returns>
    IEnumerator SwitchCharacters(int nextCharacter)
    {
        UpdateCharacter(nextCharacter);
        yield return null;
    }

    //thinking about a generic fade in and out of sprites in animation maybe?
    //how make the process of switching characters not so jarring ?
}

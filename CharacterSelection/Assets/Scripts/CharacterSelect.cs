using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// manages character pool for create character select system
/// </summary>
public class CharacterSelect : MonoBehaviour
{
    public CharacterPool characterPool;
    public TextMeshProUGUI modelNameText;
    public SpriteRenderer modelSprite;
    public int characterIndex = 0;
    public SceneChange sceneChange;
    public float charChangeCoolDownTime = .02f;
    public bool changeCharCooledDown = true;


    private void Awake()
    {
        //        modelSprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
    
        UpdateCharacter(characterIndex);
    }

   

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
//        print("Next Character");
        if (!changeCharCooledDown) { return;  }
        StartCoroutine(ChangeCharCoolingDown());
        characterIndex++;
        if (characterIndex >= characterPool.CharacterCount) characterIndex = 0;
        StartCoroutine(SwitchCharacters(characterIndex));
//        StartCoroutine(SwitchCharacters((characterIndex + 1) % characterPool.CharacterCount));

    }

    /// <summary>
    /// Summon the previous character to choose
    /// </summary>
    public void PreviousCharacter()
    {
        if (!changeCharCooledDown) { return; }
        StartCoroutine(ChangeCharCoolingDown());
//        print("Prev Character");
        characterIndex--;
        if (characterIndex < 0) characterIndex = characterPool.CharacterCount - 1;
        StartCoroutine(SwitchCharacters(characterIndex));
//      StartCoroutine(SwitchCharacters((characterIndex + 1) % characterPool.CharacterCount));
    }

    /// <summary>
    /// Select character saves to playerprefs and change scene begins
    /// </summary>
    public void ChooseCharacter()
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
//        print("character change cooled down");
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

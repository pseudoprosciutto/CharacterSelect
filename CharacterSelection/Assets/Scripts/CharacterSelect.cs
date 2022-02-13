using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public CharacterModel[] characters;
    public int characterIndex;
    bool switchNext;

    public SceneChange sceneChange;

    public void NextCharacter()
    {
        switchNext = true;
        StartCoroutine(SwitchCharacters((characterIndex + 1) % characters.Length,switchNext));
    }
    public void PreviousCharacter()
    {
        switchNext = false;
        StartCoroutine(SwitchCharacters((characterIndex + 1) % characters.Length,switchNext));
    }

    void BeginGame()
    {
        int selectedCharacter = characterIndex;
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        sceneChange.LoadNextLevel();
    }
    


    IEnumerator SwitchCharacters(int nextCharacter, bool animationType)
    {
        
        yield return new WaitForSeconds(1f);

        yield return null;
    }
}

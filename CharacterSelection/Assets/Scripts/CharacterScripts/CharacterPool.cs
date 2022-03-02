using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterPool : ScriptableObject
{
    public CharacterModel[] characters;

    public int CharacterCount
    {
        get { return characters.Length; }
    }
    public CharacterModel GetCharacterModel(int index)
    {
        return characters[index];
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public Animator transition;
    public float transitionLength = 1f;
    public void LoadLastLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
        print("load Last Level");
    }
    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
        print("load Main Menu");
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        print("load Next Level");
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("CrossFade");
        yield return new WaitForSeconds(transitionLength);
        SceneManager.LoadScene(levelIndex);
    }
}

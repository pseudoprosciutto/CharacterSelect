using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public Animator transition;
    public float transitionLength = 1f;

    /// <summary>
    /// launch LoadLevel for scene before current
    /// </summary>
    public void LoadLastLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
        print("load Last Level");
    }

    /// <summary>
    /// launch LoadLevel for the Menu Scene)
    /// </summary>
    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
        print("load Main Menu");
    }

    /// <summary>
    /// launch LoadLevel for next scene after current
    /// </summary>
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        print("load Next Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    /// <summary>
    /// Trigger scene change animation and change scene.
    /// </summary>
    /// <param name="levelIndex">int Scene Number</param>
    /// <returns></returns>
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("CrossFade");
        yield return new WaitForSeconds(transitionLength);
        SceneManager.LoadScene(levelIndex);
    }
}

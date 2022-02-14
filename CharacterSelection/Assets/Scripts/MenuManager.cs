using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject AboutScreen;
    public void EnableAbout() { AboutScreen.SetActive(true); }
    public void DisableAbout() { AboutScreen.SetActive(false); }


    // Start is called before the first frame update
    void Start()
    {
        AboutScreen.SetActive(false);
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}

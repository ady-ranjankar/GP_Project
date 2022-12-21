using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void onBackButton()
    {
        SceneManager.LoadScene("GameStart");
        
    }

    
}

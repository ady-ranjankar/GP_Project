using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadExitHelpGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void onHelpButton() {
        Debug.Log("Help!!!");
    }
    public void onExitButton(){
        SceneManager.LoadScene("ExitMenu");
    }
}

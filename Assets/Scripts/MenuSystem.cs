using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("toilet");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

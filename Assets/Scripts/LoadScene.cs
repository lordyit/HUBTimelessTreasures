using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Cenas
{
    MENU,
    GAME
}

public class LoadScene : MonoBehaviour
{
    public void LoadNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(index);
    }

    public void CenaMenu()
    {
        SceneManager.LoadScene((int)Cenas.MENU);
    }

    public void CenaGame()
    {
        SceneManager.LoadScene((int)Cenas.GAME);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    public bool podeMexer;
    public bool chestOpen;

    public Text textoGuia;

    public GameObject pausePanel;
    public GameObject fimDeJogoPanel;
    public GameObject dragonVFX;
    public GameObject bauVFX;
    public GameObject navioVFX;
    public GameObject chest;
    public GameObject timeDevice;

    public Animator navioAnim;
    public Animator player;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        podeMexer = true;
        chestOpen = false;
    }

    private void Update()
    {
        if (fimDeJogoPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene((int)Cenas.MENU);
        }
    }
}

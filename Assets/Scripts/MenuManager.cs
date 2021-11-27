using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject controlesPanel;

    public void PanelControles(bool abrir)
    {
        controlesPanel.SetActive(abrir);
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Sair()
    {
        Application.Quit();
    }
}

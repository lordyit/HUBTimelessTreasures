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

    public void Sair()
    {
        Application.Quit();
    }
}

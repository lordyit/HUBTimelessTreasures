using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutManager : MonoBehaviour
{
    [Header("===============Animação Dragão========================")]
    public PlayableDirector playableDirectorCriatura;

    [Header("===============Animação Chest====================")]
    public Animator chestAnimator;

    [SerializeField] PlayerStatus playerStatus;

    [Header("===============Animação Navio==================")]
    public GameObject navioVelho;
    public GameObject navioNovo;
    public GameObject uiPoderTempo;

    public void ChamarCenaCriatura()
    {
        playableDirectorCriatura.Play();
        playableDirectorCriatura.playableGraph.GetRootPlayable(0).SetSpeed(0.5f);
    }

    public void AbrirBau()
    {
        chestAnimator.enabled = true;
        playerStatus.reliquia.color = new Color(255, 255, 255, 255);
        playerStatus.poderTempo = true;
        GameManager.Instance.textoGuia.text = "Volte ao navio!";
    }

    public void ReconstruirNavio()
    {
        navioVelho.SetActive(false);
        navioNovo.SetActive(true);
        GameManager.Instance.fimDeJogoPanel.SetActive(true);
    }
}

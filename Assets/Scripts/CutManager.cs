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

    private static CutManager instance;
    public static CutManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }

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
        GameManager.Instance.player.SetInteger("anim", 0);
    }

    public void MexerNovamente()
    {
        GameManager.Instance.podeMexer = true;
        GameManager.Instance.chestOpen = false;
    }

    public void AnimTimeDevice()
    {
        GameManager.Instance.timeDevice.SetActive(true);
    }

    public void AnimTimeDeviceOff()
    {
        GameManager.Instance.timeDevice.SetActive(false);
    }
}

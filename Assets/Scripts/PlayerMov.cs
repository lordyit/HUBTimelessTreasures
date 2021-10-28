using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject cam;
    [SerializeField] CharacterController cc;
    [SerializeField] CutManager cutManager;
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] GameObject bestiary;
    [SerializeField] GameObject textBestiary;
    [SerializeField] float movSpdWalk;
    [SerializeField] float movSpdRun;
    [SerializeField] float rotateSpd;

    public GameObject volume;
    public GameObject past;

    float _horinzontalAxys;
    float _verticalAxys;
    float gravity = 9.8f;
    float gravityForce;

    bool canUseBestiary = false;

    readonly string animKey = "anim";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("creature"))
        {
            cutManager.playableDirectorCriatura.stopped += AllowBestiaryUse;
            cutManager.ChamarCenaCriatura();
            other.gameObject.SetActive(false);
            GameManager.Instance.dragonVFX.SetActive(false);
        }
        if (other.CompareTag("chest"))
        {
            cutManager.AbrirBau();
            other.enabled = false;
            GameManager.Instance.bauVFX.SetActive(false);
            GameManager.Instance.navioVFX.SetActive(true);
        }
        if (other.CompareTag("time") && playerStatus.poderTempo)
        {
            cutManager.uiPoderTempo.SetActive(true);
            GameManager.Instance.textoGuia.text = "Ativar relíquia: Ctrl esquedo";
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("time") && Input.GetButtonDown("Fire1") && playerStatus.poderTempo)
        {
            animator.SetInteger(animKey, 3);
            GameManager.Instance.podeMexer = false;
            GameManager.Instance.navioVFX.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("time") && playerStatus.poderTempo)
        {
            cutManager.uiPoderTempo.SetActive(false);
            GameManager.Instance.textoGuia.text = "Volte ao navio!";

        }
    }

    void GetAxys()
    {
        _horinzontalAxys = Input.GetAxis("Horizontal");
        _verticalAxys = Input.GetAxis("Vertical");
    }

    bool IsRunning()
    {
        if (Input.GetButton("Jump"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void AnimatorController()
    {
        if (animator.GetInteger(animKey) != 3)
        {
            if (_verticalAxys != 0 || _horinzontalAxys != 0)
            {
                if (IsRunning())
                {
                    animator.SetInteger(animKey, 2);
                }
                else
                {
                    animator.SetInteger(animKey, 1);
                }
            }

            if (_verticalAxys == 0 && _horinzontalAxys == 0)
            {
                animator.SetInteger(animKey, 0);
            }
        }
    }

    void MoveTwo()
    {
        Vector3 _direction = Vector3.zero;
        _direction.x = _horinzontalAxys;
        _direction.z = _verticalAxys;
        float _rotarionSpd = 0;

        Vector3 _camDirection = cam.transform.rotation * _direction;
        Vector3 _targetDirection = new Vector3(_camDirection.x, 0, _camDirection.z);

        if (_horinzontalAxys > 0.2f || _horinzontalAxys < -0.2f)
        {
            _rotarionSpd = 10f;
        }
        else
        {
            _rotarionSpd = 4f;
        }

        if (_direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_targetDirection), Time.deltaTime * _rotarionSpd);
        }
        _targetDirection.y = gravityForce;
        if (IsRunning())
        {
            cc.Move(_targetDirection.normalized * movSpdRun * Time.deltaTime);
        }
        else
        {
            cc.Move(_targetDirection.normalized * movSpdWalk * Time.deltaTime);
        }
    }

    void TravelTime()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            volume.SetActive(!volume.activeInHierarchy);
            past.SetActive(!volume.activeInHierarchy);
            animator.SetInteger(animKey, 3);
        }
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            GameManager.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            GameManager.Instance.pausePanel.SetActive(false);
        }
        
    }

    void Gravidade()
    {
        if (cc.isGrounded)
        {
            gravityForce = 0;
        }
        else
        {
            gravityForce -= gravity * Time.deltaTime;
        }
    }

    void AllowBestiaryUse(UnityEngine.Playables.PlayableDirector playableDirector)
    {
        canUseBestiary = true;
        textBestiary.SetActive(true);

        playableDirector.stopped -= AllowBestiaryUse;
    }

    void OpenCloseBestiary()
    {
        if (!canUseBestiary) return;

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (textBestiary.activeInHierarchy) textBestiary.SetActive(false);
            bestiary.SetActive(!bestiary.activeInHierarchy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
        AnimatorController();
        OpenCloseBestiary();
        if (GameManager.Instance.podeMexer)
        {
            GetAxys();
            MoveTwo();
            Gravidade();
        }
        else
        {
            cc.Move(new Vector3(0, 0, 0));
            _horinzontalAxys = 0;
            _verticalAxys = 0;
        }
    }
}

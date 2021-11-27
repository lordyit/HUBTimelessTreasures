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
    float gravity = 20f;
    float gravityForce;
    float _turnSmoothTime;

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
            other.enabled = false;
            GameManager.Instance.bauVFX.SetActive(false);
            GameManager.Instance.navioVFX.SetActive(true);
            animator.SetTrigger("chest");
            GameManager.Instance.podeMexer = false;
            GameManager.Instance.chestOpen = true;
        }
        if (other.CompareTag("time") && playerStatus.poderTempo)
        {
            cutManager.uiPoderTempo.SetActive(true);
            GameManager.Instance.textoGuia.text = "Ativar relíquia: Ctrl esquerdo";
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("time") && Input.GetButtonDown("Fire1") && playerStatus.poderTempo)
        {
            animator.SetInteger(animKey, 3);
            GameManager.Instance.podeMexer = false;
            GameManager.Instance.navioAnim.enabled = true;
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

    void NewMove()
    {
        Vector3 _direction = new Vector3(_horinzontalAxys, 0f, _verticalAxys).normalized;
        float _turnSmooth = 0.15f;

        if (_direction.magnitude >= 0.1f)
        {
            float _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _turnSmoothTime, _turnSmooth);

            transform.rotation = Quaternion.Euler(0f, _angle, 0f);

            Vector3 _moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
            _moveDir.y = gravityForce;

            if (!IsRunning())
            {
                cc.Move(_moveDir.normalized * movSpdWalk * Time.deltaTime);
            }
            else
            {
                cc.Move(_moveDir.normalized * movSpdRun * Time.deltaTime);
            }
            
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

    void OpeningChest()
    {
        if (GameManager.Instance.chestOpen)
        {
            Vector3 direction = GameManager.Instance.chest.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10 * Time.time);

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-89.075f, transform.position.y, -95.285f), 5 * Time.deltaTime);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
        AnimatorController();
        OpenCloseBestiary();
        OpeningChest();
        if (GameManager.Instance.podeMexer)
        {
            GetAxys();
            NewMove();
            Gravidade();
        }
        else
        {
            if (!GameManager.Instance.chestOpen)
            cc.Move(new Vector3(0, 0, 0));
            _horinzontalAxys = 0;
            _verticalAxys = 0;
        }
    }
}

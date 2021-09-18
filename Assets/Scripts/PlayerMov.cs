using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float movSpdWalk;
    [SerializeField] float movSpdRun;
    [SerializeField] float rotateSpd;

    public GameObject volume;
    public GameObject past;

    float _horinzontalAxys;
    float _verticalAxys;

    readonly string animKey = "anim";



    // Start is called before the first frame update
    void Start()
    {
        
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
        if (_verticalAxys > 0)
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
        else
        {
            animator.SetInteger(animKey, 4);
        }
        if (_verticalAxys == 0)
        {
            animator.SetInteger(animKey, 0);
        }
    }

    void Move()
    {
        // transform.Rotate(new Vector3(0, _horinzontalAxys * rotateSpd * Time.deltaTime));

        if (IsRunning())
        {
            transform.Translate(Vector3.forward * movSpdRun * Time.deltaTime * _verticalAxys);
        }
        else
        {
            transform.Translate(Vector3.forward * movSpdWalk * Time.deltaTime * _verticalAxys);
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


    // Update is called once per frame
    void Update()
    {
        GetAxys();
        Move();
        AnimatorController();
        TravelTime();
    }
}

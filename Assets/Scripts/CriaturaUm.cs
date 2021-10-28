using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriaturaUm : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.podeMexer = false;
    }

    private void OnDisable()
    {
        GameManager.Instance.podeMexer = true;
    }
}

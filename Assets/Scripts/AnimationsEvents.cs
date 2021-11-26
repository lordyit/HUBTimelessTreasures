using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEvents : MonoBehaviour
{
    public void OpenChest()
    {
        CutManager.Instance.AbrirBau();
    }
}

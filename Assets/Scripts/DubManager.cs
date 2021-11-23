using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DubManager : MonoBehaviour
{
    private static DubManager instance;
    public static DubManager Instance => instance;

    public AudioSource aSource;

    private void Awake()
    {
        instance = this;
    }

    public void TocarDublagem(AudioClip _voz)
    {
        aSource.Stop();
        aSource.PlayOneShot(_voz);
    }
}

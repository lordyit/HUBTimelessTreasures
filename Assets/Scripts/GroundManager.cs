using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    [SerializeField]
    private List<GroundType> grounds = new List<GroundType>();
    
    public GroundType GetGroundByTag(string tag)
    {
        GroundType groundType = new GroundType();
        foreach(GroundType ground in grounds)
        {
            if (ground.Tag.Equals(tag))
            {
                groundType = ground;
                break;
            }
        }

        return groundType;
    }

    [System.Serializable]
    public struct GroundType
    {
        public string Tag;
        public AudioClip StepSound;
        public ParticleSystem ParticleMaterial;
    }
}

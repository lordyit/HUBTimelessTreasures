using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterStepEffects : MonoBehaviour
{
    private GroundManager groundManager = null;
    private GroundManager.GroundType groundType = new GroundManager.GroundType();
    private AudioSource audioSource = null;
    [SerializeField]
    private LayerMask whatIsGround = new LayerMask();
    [SerializeField]
    private float rayDistance = 5f;

    private void OnValidate()
    {
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    private void Start()
    {
        groundManager = FindObjectOfType<GroundManager>();
    }

    private void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayDistance, whatIsGround))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayDistance, Color.red);
            groundType = groundManager.GetGroundByTag(hit.transform.tag);
        }
    }

    public void PlayStepEffect()
    {
        audioSource.PlayOneShot(groundType.StepSound);
    }
}

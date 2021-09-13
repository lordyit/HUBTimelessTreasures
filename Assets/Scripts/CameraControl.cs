using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Vector3 _offset;
    [SerializeField] Vector3 _rotateMaxLimits;
    [SerializeField] Vector3 _rotateMinLimits;
    [SerializeField] float _mouseSensitivity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FollowPlayer()
    {
        transform.position = _player.transform.position - _offset;
    }

    void LookAround()
    {
        float _verticalAxys = Input.GetAxis("Mouse Y");
        float _horizontalAxys = Input.GetAxis("Mouse X");
        transform.RotateAround(_player.position, Vector3.up, _horizontalAxys * _mouseSensitivity);
        transform.RotateAround(Vector3.zero, transform.right, _verticalAxys * _mouseSensitivity);
        RotateLimits();
    }

    void RotateLimits()
    {
        if (transform.localEulerAngles.x > _rotateMaxLimits.x)
        {
            transform.localEulerAngles = new Vector3(_rotateMaxLimits.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();
        //LookAround();
    }
}

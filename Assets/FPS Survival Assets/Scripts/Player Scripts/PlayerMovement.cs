using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 _moveDirection;

    public float Speed = 5f;
    private float _gravity = 20f;
    public float jumpForce = 10f;
    private float _verticalVelocity;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        _moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
        _moveDirection = transform.TransformDirection(_moveDirection);
    }
}

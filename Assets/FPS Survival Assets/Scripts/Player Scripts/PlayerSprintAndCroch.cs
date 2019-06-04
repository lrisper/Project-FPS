using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCroch : MonoBehaviour
{
    private PlayerMovement _playerMovment;

    public float SprintSpeed = 10f;
    public float MoveSpeed = 5f;
    public float CrochSpeed = 2f;

    private Transform _lookRoot;
    private float _standHeight = 1.6f;
    private float _crouchHeight = 1f;

    private bool _isCrouching;

    // Start is called before the first frame update
    void Start()
    {
        _playerMovment = GetComponent<PlayerMovement>();
        _lookRoot = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isCrouching)
        {
            _playerMovment.Speed = SprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !_isCrouching)
        {
            _playerMovment.Speed = MoveSpeed;
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_isCrouching)
            {
                _lookRoot.localPosition = new Vector3(0f, _standHeight, 0f);
                _playerMovment.Speed = MoveSpeed;
                _isCrouching = false;
            }
            else
            {
                _lookRoot.localPosition = new Vector3(0f, _crouchHeight, 0f);
                _playerMovment.Speed = CrochSpeed;
                _isCrouching = true;
            }
        }
    }
}

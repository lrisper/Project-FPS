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

    private PlayerFootSteps _playerFootSteps;

    private float _sprintVolume = 1f;
    private float _crouchVolume = 0.1f;

    private float _walkVolumeMin = 0.2f;
    private float _walkVolumeMax = 0.6f;

    private float _walkStepDistance = 0.4f;
    private float _sprintStepDistance = 0.25f;
    private float _crouchStepDistance = 0.5f;


    // Start is called before the first frame update
    void Awake()
    {
        _playerMovment = GetComponent<PlayerMovement>();
        _lookRoot = transform.GetChild(0);

        _playerFootSteps = GetComponentInChildren<PlayerFootSteps>();
    }

    public void Start()
    {
        _playerFootSteps.VolMin = _walkVolumeMin;
        _playerFootSteps.VolMax = _walkVolumeMax;

        _playerFootSteps.StepDistance = _walkStepDistance;
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

            _playerFootSteps.StepDistance = _sprintStepDistance;
            _playerFootSteps.VolMin = _sprintVolume;
            _playerFootSteps.VolMax = _sprintVolume;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !_isCrouching)
        {
            _playerMovment.Speed = MoveSpeed;

            _playerFootSteps.StepDistance = _walkStepDistance;

            _playerFootSteps.VolMin = _walkVolumeMin;
            _playerFootSteps.VolMax = _walkVolumeMax;


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

                _playerFootSteps.StepDistance = _walkStepDistance;
                _playerFootSteps.VolMin = _sprintVolume;
                _playerFootSteps.VolMax = _sprintVolume;

                _isCrouching = false;
            }
            else
            {
                _lookRoot.localPosition = new Vector3(0f, _crouchHeight, 0f);
                _playerMovment.Speed = CrochSpeed;

                _playerFootSteps.StepDistance = _crouchStepDistance;
                _playerFootSteps.VolMin = _crouchVolume;
                _playerFootSteps.VolMax = _crouchVolume;

                _isCrouching = true;
            }
        }
    }
}

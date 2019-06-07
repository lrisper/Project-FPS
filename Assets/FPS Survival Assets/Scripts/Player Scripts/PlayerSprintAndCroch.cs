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

    private PlayerStats _playerStats;
    private float _sprintValue = 100f;
    public float sprintTreshold = 10f;


    // Start is called before the first frame update
    void Awake()
    {
        _playerMovment = GetComponent<PlayerMovement>();
        _lookRoot = transform.GetChild(0);

        _playerFootSteps = GetComponentInChildren<PlayerFootSteps>();

        _playerStats = GetComponent<PlayerStats>();
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
        // If we have stemina we can sprint
        if (_sprintValue > 0f)
        {
            // bug when holiding down sheft and not moving it is beening used
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_isCrouching)
            {
                _playerMovment.Speed = SprintSpeed;

                _playerFootSteps.StepDistance = _sprintStepDistance;
                _playerFootSteps.VolMin = _sprintVolume;
                _playerFootSteps.VolMax = _sprintVolume;
            }
        }
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
        if (Input.GetKey(KeyCode.LeftShift) && !_isCrouching)
        {
            _sprintValue -= sprintTreshold * Time.deltaTime;

            if (_sprintValue <= 0)
            {
                _sprintValue = 0;

                // reset speed and sounds 
                _playerMovment.Speed = MoveSpeed;
                _playerFootSteps.StepDistance = _sprintStepDistance;
                _playerFootSteps.VolMin = _sprintVolume;
                _playerFootSteps.VolMax = _sprintVolume;
            }
            //_playerStats.DisplayStaminaStats(_sprintValue);
            else
            {
                if (_sprintValue != 100)
                {
                    _sprintValue += (sprintTreshold / 2 * Time.deltaTime);
                    _playerStats.DisplayStaminaStats(_sprintValue);

                    if (_sprintValue >= 100)
                    {
                        _sprintValue = 100f;
                    }

                }
            }


        }

    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // if we are crouching stand up
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

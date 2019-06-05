using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFootSteps : MonoBehaviour
{
    private AudioSource _footStepSounds;

    [SerializeField] private AudioClip[] _footStepClip;

    private CharacterController _characterController;

    [HideInInspector] public float VolMin, VolMax;
    private float _accumultedDistance;
    [HideInInspector] public float StepDistance;


    void Awake()
    {
        _footStepSounds = GetComponent<AudioSource>();
        _characterController = GetComponentInParent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {
        CkeckToPlayFootStepSounds();


    }

    private void CkeckToPlayFootStepSounds()
    {
        if (!_characterController.isGrounded)
        {
            return;
        }

        if (_characterController.velocity.sqrMagnitude > 0)
        {
            _accumultedDistance += Time.deltaTime;
            if (_accumultedDistance > StepDistance)
            {
                _footStepSounds.volume = UnityEngine.Random.Range(VolMin, VolMax);
                _footStepSounds.clip = _footStepClip[UnityEngine.Random.Range(0, _footStepClip.Length)];
                _footStepSounds.Play();

                _accumultedDistance = 0;
            }
        }
        else
        {
            _accumultedDistance = 0;
        }
    }
}

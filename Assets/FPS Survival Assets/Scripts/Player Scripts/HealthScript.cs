using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{

    private EnemyAnimator _enemyAnim;
    private NavMeshAgent _navAgent;

    private EnemyController _enemyController;

    public float health = 100f;
    public bool isPlayer, isBoar, isCannibal;
    private bool _isDead;
    private EnemyAudio _enemyAudio;

    void Awake()
    {
        if (isBoar || isCannibal)
        {
            _enemyAnim = GetComponent<EnemyAnimator>();
            _enemyController = GetComponent<EnemyController>();
            _navAgent = GetComponent<NavMeshAgent>();
            _enemyAudio = GetComponentInChildren<EnemyAudio>();
        }
        if (isPlayer)
        {

        }
    }

    public void ApplyDamage(float damage)
    {
        if (_isDead)
        {
            return;
        }

        health -= damage;

        if (isPlayer)
        {
            //
        }

        if (isBoar || isCannibal)
        {
            if (_enemyController.enemyState == EnemyState.PATROL)
            {
                _enemyController.chaseDistance = 50f;
            }
        }
        if (health <= 0f)
        {
            PlayerDead();
            _isDead = true;
        }
    }

    private void PlayerDead()
    {
        if (isCannibal)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 5f);

            _enemyController.enabled = false;
            _navAgent.enabled = false;
            _enemyAnim.enabled = false;

            StartCoroutine(DeadSound());

            // Spawn more enemies

        }
        if (isBoar)
        {
            _navAgent.velocity = Vector3.zero;
            _navAgent.isStopped = true;

            _enemyController.enabled = false;

            _enemyAnim.Dead();

            StartCoroutine(DeadSound());

            // Spawn more enemies
        }

        if (isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }
            //stop spawning enemies
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }
        if (tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameobject", 3f);
        }
    }

    void TurnOffGameobject()
    {
        gameObject.SetActive(false);

    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        _enemyAudio.PlayDieSound();
    }



}

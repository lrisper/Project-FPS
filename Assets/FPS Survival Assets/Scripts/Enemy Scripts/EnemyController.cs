using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator _enemyAnim;
    private NavMeshAgent _navAgent;

    private EnemyState _enemyState;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;

    public float chaseDistance = 7f;

    private float _currentChaseDistance;
    public float attackDistance = 1.8f;
    public float chaseAfterAttackDistance = 2f;

    public float patrolRadiusMin = 20f;
    public float patrolRadiusMax = 60f;
    public float patrolForThisTime = 15f;
    private float _patrolTimer;

    public float waitBeforeAttack = 2f;
    public float attactTimer;

    private Transform _target;

    public GameObject attackPoint;

    private EnemyAudio _enemyAudio;

    public void Awake()
    {
        _enemyAnim = GetComponent<EnemyAnimator>();
        _navAgent = GetComponent<NavMeshAgent>();

        _target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;

        _enemyAudio = GetComponentInChildren<EnemyAudio>();

    }

    // Start is called before the first frame update
    void Start()
    {
        _enemyState = EnemyState.PATROL;
        _patrolTimer = patrolForThisTime;
        attactTimer = waitBeforeAttack;
        _currentChaseDistance = chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyState == EnemyState.PATROL)
        {
            patrol();
        }
        if (_enemyState == EnemyState.CHASE)
        {
            Chase();
        }
        if (_enemyState == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _navAgent.velocity = Vector3.zero;
        _navAgent.isStopped = true;

        attactTimer += Time.deltaTime;

        if (attactTimer > waitBeforeAttack)
        {
            _enemyAnim.Attack();
            attactTimer = 0;

            _enemyAudio.PlayAttackSound();

        }
        if (Vector3.Distance(transform.position, _target.position) > attackDistance + chaseAfterAttackDistance)
        {
            _enemyState = EnemyState.CHASE;
        }

    }

    private void Chase()
    {
        _navAgent.isStopped = false;
        _navAgent.speed = runSpeed;
        _navAgent.SetDestination(_target.position);

        if (_navAgent.velocity.sqrMagnitude > 0)
        {
            _enemyAnim.Run(true);

        }
        else
        {
            _enemyAnim.Run(false);
        }

        if (Vector3.Distance(transform.position, _target.position) <= attackDistance)
        {
            _enemyAnim.Run(false);
            _enemyAnim.Walk(false);
            _enemyState = EnemyState.ATTACK;

#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
            if (chaseDistance != _currentChaseDistance)
#pragma warning restore RECS0018 // Comparison of floating point numbers with equality operator
            {
                chaseDistance = _currentChaseDistance;
            }
            else if (Vector3.Distance(transform.position, _target.position) > chaseDistance)
            {
                _enemyAnim.Run(false);
                _enemyState = EnemyState.PATROL;
                _patrolTimer = patrolForThisTime;

#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
                if (chaseDistance != _currentChaseDistance)
#pragma warning restore RECS0018 // Comparison of floating point numbers with equality operator
                {
                    chaseDistance = _currentChaseDistance;
                }
            }
        }
    }

    private void patrol()
    {
        _navAgent.isStopped = false;
        _navAgent.speed = walkSpeed;
        _patrolTimer += Time.deltaTime;

        if (_patrolTimer > patrolForThisTime)
        {
            SetNewRandomDestination();
            _patrolTimer = 0;
        }

        if (_navAgent.velocity.sqrMagnitude > 0)
        {
            _enemyAnim.Walk(true);

        }
        else
        {
            _enemyAnim.Walk(false);
        }

        if (Vector3.Distance(transform.position, _target.position) <= chaseDistance)
        {
            _enemyAnim.Walk(false);
            _enemyState = EnemyState.CHASE;

            _enemyAudio.PlayScreamSound();
        }

    }

    private void SetNewRandomDestination()
    {
        float randRadius = UnityEngine.Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 randDir = UnityEngine.Random.insideUnitSphere * randRadius;
        randDir += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);

        _navAgent.SetDestination(navHit.position);
    }

    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }

    }
    public EnemyState enemyState
    { get; set; }
}

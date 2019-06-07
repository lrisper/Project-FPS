using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBowScript : MonoBehaviour
{
    private Rigidbody _myRigidbody;
    public float speed = 30f;
    public float deactivateTimer = 3f;
    public float damage = 15f;

    public void Awake()
    {
        _myRigidbody = GetComponent<Rigidbody>();

    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameobject", deactivateTimer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Lunch(Camera mainCamera)
    {
        _myRigidbody.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + _myRigidbody.velocity);
    }

    void DeactivateGameobject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }

    }

    public void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.ENEMY_TAG)
        {
            target.GetComponent<HealthScript>().ApplyDamage(damage);
        }
    }
}

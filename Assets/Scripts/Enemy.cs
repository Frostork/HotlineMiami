using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask Playerlayer;
    public float Speed;
    public int Life;
    
    private bool _isDead;
    private bool _isChangingDirection;

    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    public enum States
    {
        partol,
        detect,
        attack
    }

    public Enum currentState;

    private void Start()
    {
        currentState = States.partol;
    }

    private void Update()
    {
        CheckState();
    }

    private void CheckLife()
    {
        if(Life <= 0)
            Die();
    }

    private void CheckState()
    {
        switch (currentState)
        {
            case States.partol:
                Patrol();
                break;

            case States.detect:
                Detect();
                break;

            case States.attack:
                Attack();
                break;
        }
    }

    private void Patrol()
    {
        //transform.position += Vector3.up * (Speed * Time.deltaTime);
        if (_isChangingDirection == false)
            StartCoroutine(ChangeDirection());
    }

    IEnumerator ChangeDirection()
    {
        _isChangingDirection = true;
        yield return new WaitForSeconds(2);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        Debug.Log("change direction");
        _isChangingDirection = false;
    }

    private void Detect()
    {
        
    }

    private void Attack()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (Contains(Playerlayer, other.gameObject.layer))
        {
            currentState = States.detect;
        }
    }

    public void TakeDamage()
    {
        Life--;
        CheckLife();
    }
    
    public void Die()
    {
        _isDead = true;
        Destroy(gameObject);
    }
}
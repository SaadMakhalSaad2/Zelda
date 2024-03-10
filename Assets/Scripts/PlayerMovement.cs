using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator anim;
    private PlayState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayState.FREE_ROAM;
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Attack") && currentState != PlayState.ATTACK)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayState.FREE_ROAM)
            UpdateAnimationAndMove();
    }

    private IEnumerator AttackCo()
    {
        anim.SetBool("isAttacking", true);
        this.currentState = PlayState.ATTACK;
        yield return null;
        anim.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.33f);
        this.currentState = PlayState.FREE_ROAM;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharachter();
            anim.SetFloat("moveX", change.x);
            anim.SetFloat("moveY", change.y);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    void MoveCharachter()
    {
        myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
}

public enum PlayState
{
    FREE_ROAM,
    ATTACK,
    INTERACT
}

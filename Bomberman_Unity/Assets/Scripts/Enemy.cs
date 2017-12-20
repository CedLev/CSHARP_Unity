using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemySO _EnemyData;
    public float _MoveSpeed;

    private Animator _Animator;
    private Rigidbody2D _RigidBody;

    public bool _CanMove = true;
    private bool _CanStart = true;


    private void Start()
    {
        _Animator = GetComponent<Animator>();
        _RigidBody = GetComponent<Rigidbody2D>();

        _MoveSpeed = _EnemyData.moveSpeed;
    }


    private void Update()
    {
        if (_CanStart)
        {
            StartCoroutine(EnemyRoutine());
        }
    }


    private void RandomMovement()
    {
        if (_CanMove)
        {
            int randomMove = Random.Range(0, 4);

            if (randomMove == 0)
            {
                _Animator.SetInteger("CheckEnemyState", 0);
            }
            else if (randomMove == 1)
            {
                Vector2 direction = Vector2.up;
                _RigidBody.velocity = direction * _MoveSpeed;
                _Animator.SetInteger("CheckEnemyState", 1);
            }
            else if (randomMove == 2)
            {
                Vector2 direction = Vector2.down;
                _RigidBody.velocity = direction * _MoveSpeed;
                _Animator.SetInteger("CheckEnemyState", 2);
            }
            else if (randomMove == 3)
            {
                Vector2 direction = Vector2.right;
                _RigidBody.velocity = direction * _MoveSpeed;
                _Animator.SetInteger("CheckEnemyState", 3);
            }
            else if (randomMove == 4)
            {
                Vector2 direction = Vector2.left;
                _RigidBody.velocity = direction * _MoveSpeed;
                _Animator.SetInteger("CheckEnemyState", 4);
            }
        }
    }


    public IEnumerator EnemyRoutine()
    {
        _CanStart = false;

        RandomMovement();

        yield return new WaitForSeconds(5f);

        _CanStart = true;
    }


    private void OnTriggerEnter2D(Collider2D aCol)
    {
        if(aCol.IsTouchingLayers(10))
        {
            GameManager.Instance.PlayerHpLost();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Movement
{

    public GamePlayManager GPmanager;
    public Enemy enemy;
    Rigidbody2D rb2d;
    float h, v;
    [SerializeField]
    LayerMask blockingLayer;
    enum Direction { Up, Down, Left, Right };

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        RandomDirection();
    }

    public void RandomDirection()
    {
        CancelInvoke("RandomDirection");

        List<Direction> lottery = new List<Direction>();
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0.1f, 0), blockingLayer))
        {
            lottery.Add(Direction.Right);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(-0.1f, 0), blockingLayer))
        {
            lottery.Add(Direction.Left);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0, 0.1f), blockingLayer))
        {
            lottery.Add(Direction.Up);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0, -0.1f), blockingLayer))
        {
            lottery.Add(Direction.Down);
        }

        Direction selection = lottery[Random.Range(0, lottery.Count)];
        if (selection == Direction.Up)
        {
            v = 1;
            h = 0;
        }
        if (selection == Direction.Down)
        {
            v = -1;
            h = 0;
        }
        if (selection == Direction.Right)
        {
            v = 0;
            h = 1;
        }
        if (selection == Direction.Left)
        {
            v = 0;
            h = -1;
        }
        Invoke("RandomDirection", Random.Range(1, 4));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RandomDirection();
    }

    private void FixedUpdate()
    {
        if (GPmanager.can_move && !enemy.IsAlreadyDead())
        {
            if (v != 0 && isMoving == false) StartCoroutine(MoveVertical(v, rb2d));
            else if (h != 0 && isMoving == false) StartCoroutine(MoveHorizontal(h, rb2d));
        }
    }
}
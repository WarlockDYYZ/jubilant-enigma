using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : EnemyController
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float patrolSpeed = 0.1f;
    public Vector3 patrolTarget;

    void Awake()
    {
        patrolTarget = pointA;
    }
    protected override void move()
    {

        base.move();


        if (Vector2.Distance(transform.position, target.transform.position) > distance)
        {

            if (Vector2.Distance(transform.position, pointA) < 0.01f) 
            {
                patrolTarget = pointB;
            }
            
            if (Vector2.Distance(transform.position, pointB) < 0.01f)
            {
                patrolTarget = pointA;
            }


            Vector2 direction = (patrolTarget - transform.position).normalized;

            
            transform.Translate(direction * patrolSpeed * Time.deltaTime);


            if (direction.x < 0)
            {
                sprd.flipX = true;
            }
            else
            {
                sprd.flipX = false;
            }
        }
    }
}

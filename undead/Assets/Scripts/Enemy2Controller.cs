using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : EnemyController
{
    public float stopDistance = 1.5f;
    bool isLeaving = false;
    Vector3 startPoint;
    Vector3 leavingDirection;
    private void Awake()
    {
        isLeaving = false;
        startPoint = transform.position;
    }
    protected override void move()
    {
        
        float EPDistance = Vector2.Distance(transform.position, target.transform.position);


        if (Vector2.Distance(transform.position, startPoint) < 0.01f)
        {
            isLeaving = false;
        }


        if (EPDistance > stopDistance && !isLeaving)
        { 

            base.move();
        }


        if (EPDistance < stopDistance) 
        {

            isLeaving = true;


            leavingDirection = (startPoint - transform.position).normalized;


            transform.Translate(leavingDirection * moveSpeed * Time.deltaTime);


            sprd.flipX = leavingDirection.x < 0;
        }


        if (isLeaving)
        {

            leavingDirection = (startPoint - transform.position).normalized;

            transform.Translate(leavingDirection * moveSpeed * Time.deltaTime);

            sprd.flipX = leavingDirection.x < 0;
        }

    }
}
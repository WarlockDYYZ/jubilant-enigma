using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveLimit = 5f;
    public float movespeed = 5f;
    public float damage = 0f;
    public Vector2 position;
    public Vector3 direction;
    public float coolingTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = position;


        float angle = Vector3.Angle(direction, Vector2.up);

        if (direction.x > 0)
        {
            angle = -angle;
        }

        transform.localRotation = Quaternion.Euler(0, 0, angle);        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * movespeed * Time.deltaTime, Space.World);

        if (Vector2.Distance(position, transform.position) > moveLimit)
        {

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

}

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float distance = 5f;
    public float health = 10f;
    public float currentHealth;
    public float damage = 5f;
    public bool isDead = false;
    protected GameObject target;
    Rigidbody2D rb;
    protected SpriteRenderer sprd;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprd = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectsWithTag("Player")[0];
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isGameOver)
        {
            return;
        }

        if (isDead)
        {
            return; 
        }
        move();
    }

    protected virtual void move()
    {

        if (target.GetComponent<PlayerController>().isDead) 
        {
            return;
        }


        if (Vector2.Distance(transform.position, target.transform.position) > distance) 
        {
            return;
        }


        Vector2 direction = (target.transform.position - transform.position).normalized;


        transform.Translate(direction * moveSpeed * Time.deltaTime);


        sprd.flipX = direction.x < 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) 
        {
            return;
        }
        if (other.gameObject.tag == "Bullet")
        {
            float damage = other.gameObject.GetComponent<Bullet>().damage;
            takeDamage(damage);
        }
    }

    void takeDamage(float damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hit");
        Debug.Log("Enemy Health:" + currentHealth);
        if (currentHealth <= 0)
        {
            die();
        }
    }

    void die()
    {
        anim.SetBool("isDead", true);
        isDead = true;
        rb.bodyType = RigidbodyType2D.Static;
        Debug.Log("Enemy Die.");
        Destroy(gameObject, 2f);
        GameController.instance.minusEnemCount();
    }
}

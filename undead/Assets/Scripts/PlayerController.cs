using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;
    
public class PlayerController : MonoBehaviour
{
    SpriteRenderer sprd;
    Animator anim;
    Rigidbody2D rb;
    public float moveSpeed = 1f;
    public float h = 1;
    public float v = 0;
    public float sh = 1;
    public float sv = 0;
    Transform rightWeapon;
    Transform leftWeapon;

    public GameObject bullet;

    public GameObject tool;
    float toolCoolingTime;
    float time;
    
    public bool isDead = false;
    public float health;
    public float currentHealth;

    Transform healthBar;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprd = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rightWeapon = transform.Find("RightWeapon");
        leftWeapon = transform.Find("LeftWeapon");

        toolCoolingTime = tool.GetComponent<Bullet>().coolingTime;
        time = toolCoolingTime;

        currentHealth = health;
        healthBar = transform.Find("Canvas").Find("HealthBar");
        slider = healthBar.GetComponent<Slider>();
        initHealthBar(health);
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

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isRun", true);


            h = -1;
            v = 0;

            sh = h;
            sv = v;


            transform.localScale = new Vector3(-1, 1, 1);


            rightWeapon.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

            // 设置血条方向
            healthBar.localScale = new Vector3(-1, 1, 1);


        } else if (Input.GetKey(KeyCode.RightArrow)){

            anim.SetBool("isRun", true);


            h = 1;
            v = 0;

            sh = h;
            sv = v;
            

            transform.localScale = new Vector3(1, 1, 1);


            rightWeapon.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));


            healthBar.localScale = new Vector3(1, 1, 1);

        } else if (Input.GetKey(KeyCode.UpArrow)) {

            anim.SetBool("isRun", true);


            h = 0;
            v = 1;

            sh = h;
            sv = v;


            rightWeapon.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
        
        } else if (Input.GetKey(KeyCode.DownArrow)) {

            anim.SetBool("isRun", true);


            h = 0;
            v = -1;

            sh = h;
            sv = v;


            rightWeapon.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));


        } else if (Input.GetKeyUp(KeyCode.LeftArrow) ||
                 Input.GetKeyUp(KeyCode.RightArrow) ||
                 Input.GetKeyUp(KeyCode.UpArrow) ||
                 Input.GetKeyUp(KeyCode.DownArrow)) {
            anim.SetBool("isRun", false);
        } else {
            h = 0;
            v = 0;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            shootRight();
        }

        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z) && time > toolCoolingTime)
        {

            shootLeft();
        }

        Vector2 speed = new Vector2(h * moveSpeed * Time.deltaTime,
                                    v * moveSpeed * Time.deltaTime);

        transform.Translate(speed);

    }

    void shootLeft()
    {
        GameObject newBullet = GameObject.Instantiate(bullet);
        newBullet.GetComponent<Bullet>().direction = new Vector3(sh, sv, 0);
        newBullet.GetComponent<Bullet>().position = rightWeapon.position + 
                                                    new Vector3(sh * 0.9f, sv * 0.9f, 0);
    }
    void shootRight()
    {
        for (int i = 0; i < 6; i++)
        {

            Vector3 direction = (Quaternion.Euler(0f, 0f, i * 60f) * Vector3.right).normalized;
            Debug.Log(direction);
            GameObject newBullet = GameObject.Instantiate(tool);
            newBullet.GetComponent<Bullet>().direction = direction;
            newBullet.GetComponent<Bullet>().position = transform.position + direction * 2f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Health")
        {
            currentHealth = health;
            updateHealthBar(health);


            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" &&
            !other.gameObject.GetComponent<EnemyController>().isDead)
        {
            float damage = other.gameObject.GetComponent<EnemyController>().damage;
            takeDamage(damage);
        }
    }

    void takeDamage(float damage)
    {
        
        currentHealth -= damage;
        Debug.Log("Player Health:" + currentHealth);
        if (currentHealth <= 0)
        {
            die();
            updateHealthBar(0);
        }

        updateHealthBar(currentHealth);
    }

    void updateHealthBar(float health)
    {
        slider.value = health;
    }

    void initHealthBar(float health)
    { 
        slider.maxValue = health;
        slider.value = health;
    }

    void die()
    {

        anim.SetBool("isDead", true);
        isDead = true;
        rb.bodyType = RigidbodyType2D.Static;
        Debug.Log("Player Die.");


        Destroy(transform.GetChild(0).gameObject);
        Destroy(transform.GetChild(1).gameObject);
        Destroy(transform.GetChild(2).gameObject);

        GameController.instance.loseGame();
    }
}
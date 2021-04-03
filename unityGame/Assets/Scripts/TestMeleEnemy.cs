using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMeleEnemy : MonoBehaviour
{
    float speed;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    public Animator action;
    float health;
    int damage;

    

    float attackRange = 2;
    private enum State
    {
        Stand,
        Chase,
        Attack,

    }
    // Start is called before the first frame update
    public Transform player;
    private State state;
    void Start()
    {
        speed = StaticData.MeleeSpeed;
        health = StaticData.MeleeHP;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Vector3 position = transform.position;
        position[2] = 0;
        gameObject.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            action.SetBool("running", false);
            action.SetFloat("Horizontal", 0);
            action.SetBool("attacking", false);
            action.SetBool("death", true);

        }
        else
        {
            findTarget();
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                state = State.Attack;
            }

            switch (state)
            {
                default:
                case State.Stand:
                    action.SetBool("running", false);
                    action.SetFloat("Horizontal", 0);
                    action.SetBool("attacking", false);
                    break;
                case State.Chase:
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                    action.SetBool("running", true);
                    action.SetBool("attacking", false);
                    if (transform.position.x - player.position.x > 0)
                    {
                        action.SetFloat("Horizontal", -1);
                        //left
                    }
                    else
                    {
                        action.SetFloat("Horizontal", 1);
                    }

                    break;
                case State.Attack:

                    if (timeBtwShots <= 0)
                    {
                        action.SetBool("running", false);
                        action.SetBool("attacking", true);
                        Instantiate(projectile, transform.position, Quaternion.identity);
                        timeBtwShots = startTimeBtwShots;
                    }
                    else
                    {
                        timeBtwShots -= Time.deltaTime;
                        action.SetBool("attacking", false);
                    }
                    break;

            }
        }
        
    }
    private void findTarget()
    {
        float targetRange = 20f;
        if (Vector3.Distance(transform.position, player.position) < targetRange)
        {
            state = State.Chase;
        }
        else state = State.Stand;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public void animationEnded()
    {
        StaticData.Score += 20;
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    float health;
    public float visionDistance = 20f;
    public Animator action;
    int damage;



    // Start is called before the first frame update
    void Start()
    {
        speed = StaticData.RangeSpeed;
        health = StaticData.RangeHP;

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
            action.SetBool("death", true);
        }
        if (Vector3.Distance(transform.position, player.position) < visionDistance)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }
            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
       
    }
    public void animationEnded2()
    {
        StaticData.Score += 35;
        StaticData.Kills++;
        Destroy(gameObject);
    }
    public int getDmg()
    {
        return damage;
    }
}

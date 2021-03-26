using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;
    public Collider2D objectCollider;
    public Collider2D anotherCollider;
    public bool mele;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);



    }

    // Update is called once per frame
    void Update()
    {
        if (mele == true)
        {
            if(transform.position.x > target.x - 2 && transform.position.x < target.x + 2 && transform.position.y > target.y - 2 && transform.position.y < target.y + 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                if (transform.position.x == target.x && transform.position.y == target.y)
                {
                    DestroyProjectile();
                }
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                DestroyProjectile();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            
            player.GetComponent<Player>().playerTakeDamage(damage);

            DestroyProjectile();  
        }
        else if(other.CompareTag("Wall") || other.CompareTag("Obsticle"))
        {
            DestroyProjectile();
        }

    }



    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

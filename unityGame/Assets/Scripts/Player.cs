using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public int curHp;
    public int maxHp;
    public int coins;
    public SpriteRenderer sr;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    public LayerMask moveLayerMask;
    public Slider slider;
    private bool deds = true;

    private void Start()
    {
        slider.maxValue = maxHp;

    }
    void Update()
    {
        
        if(curHp < 0 && deds)
        {
            deds = false;
            StaticData.Deaths++;
            animator.SetBool("death", true);
            /* Vitalij Gurin */
            FindObjectOfType<GameManager>().EndGame();
        }
        else if(GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("EnemyMele").Length == 0 &&
                FindObjectOfType<EnemySpawn>().enemySpawned)
        {
            FindObjectOfType<GameManager>().LoadNewLevel();
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {

            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void playerTakeDamage(int damage)
    {
        curHp -= damage;
        if (curHp <= 0)
        {
            slider.value = 0;

        }
        else slider.value = curHp;
    }


}
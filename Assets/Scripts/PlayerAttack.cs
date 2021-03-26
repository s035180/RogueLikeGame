using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttacks;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public LayerMask whatIsEnemies2;
    public float startTimeBtwAttacks;
    public float attackRange;
    public int damage;
    public Animator attack;
    public GameObject readyAttack;
    public Animator anim;


    void Start()
    {
        readyAttack = GameObject.FindGameObjectWithTag("AttackReady");
    }
    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttacks <= 0)
        {
            readyAttack.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                attack.SetBool("Hit", true);
                readyAttack.SetActive(false);
                StartCoroutine("wait");
             
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                Collider2D[] enemiesToDamage2 = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies2);
                for (int i = 0; i < enemiesToDamage2.Length; i++)
                {
                    enemiesToDamage2[i].GetComponent<TestMeleEnemy>().TakeDamage(damage);
                }

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }

            }
            timeBtwAttacks = startTimeBtwAttacks;

        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
        if (timeBtwAttacks <= 0)
        {

            attack.SetBool("Hit", false);

        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    IEnumerable wait()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }


}



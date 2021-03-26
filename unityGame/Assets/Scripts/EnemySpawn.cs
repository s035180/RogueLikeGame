using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public int minEnemies;
    public int maxEnemies;
    public GameObject[] level1;
    public GameObject[] level2;
    public GameObject[] level3;
    public GameObject[] level4;
    private void Start()
    {
        int rand2 = Random.Range(minEnemies, maxEnemies);
        int rand = Random.Range(0, level1.Length);
        if(GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("EnemyMele").Length < rand2)
        Instantiate(level1[rand], transform.position, Quaternion.identity);

    }
}

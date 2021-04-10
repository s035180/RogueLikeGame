using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    int minEnemies = 1;
    int maxEnemies = 1;
    public GameObject[] level1;
    public GameObject[] level2;
    public GameObject[] level3;
    public GameObject[] level4;
    public bool enemySpawned = false;
    private void Start()
    {
        int rand2 = Random.Range(minEnemies, maxEnemies);
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            int rand = Random.Range(0, level1.Length);
            if (GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("EnemyMele").Length < rand2)
                Instantiate(level1[rand], transform.position, Quaternion.identity);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            int rand = Random.Range(0, level2.Length);
            if (GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("EnemyMele").Length < rand2)
                Instantiate(level2[rand], transform.position, Quaternion.identity);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            int rand = Random.Range(0, level3.Length);
            if (GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("EnemyMele").Length < rand2)
                Instantiate(level3[rand], transform.position, Quaternion.identity);
        }
        else
        {
            int rand = Random.Range(0, level4.Length);
            if (GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("EnemyMele").Length < rand2)
                Instantiate(level4[rand], transform.position, Quaternion.identity);
        }
        enemySpawned = true;
    }
}

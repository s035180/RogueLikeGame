using UnityEngine;
using UnityEngine.UI;

public class EnemyLeft : MonoBehaviour
{
    public Text enemyLeft;
    // Update is called once per frame
    void FixedUpdate()
    {
        enemyLeft.text = "Enemy Left: " + (GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("EnemyMele").Length).ToString();

        //Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);
    }
}

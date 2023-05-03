using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    
    enum EnemyType
    {
        S,
        M,
        B
    };
    public float time;
    float camH;
    float camV;
    float ran;
    int count = 0;
    bool isSpawn;
    void Start()
    {
        camV = Camera.main.orthographicSize;
        camH = camV * Camera.main.aspect;
        ran = Random.Range(-camH, camH);
        //StartCoroutine(SpawnMiddle());
        StartCoroutine(SpawnLeft());
    }

    
    void Update()
    {
        time += Time.deltaTime;
    }
    float RandomNumber()
    {
        float ran = Random.Range(-camH + 0.4f, camH - 0.4f);
        return ran;
    }
    IEnumerator Delay()
    {
        
        yield return new WaitForSeconds(0.4f);
        isSpawn = false;
    }
    IEnumerator SpawnMiddle()
    {
        yield return new WaitForSeconds(0.1f);
        
        /*Enemy enemyL = GameManager.Instance.enemyPool.GetEnemy((int)EnemyType.M);
        enemyL.transform.position = new Vector3(2, camV, 0);
        enemyL.SetEnemy(0, -1f, 0);
        Enemy enemyM = GameManager.Instance.enemyPool.GetEnemy((int)EnemyType.M);
        enemyM.transform.position = new Vector3(camH, camV, 0);
        enemyM.SetEnemy(0, -1f, 0);
        Enemy enemyR = GameManager.Instance.enemyPool.GetEnemy((int)EnemyType.M);
        enemyR.transform.position = new Vector3(camH + 2, camV, 0);
        enemyR.SetEnemy(0, -1f, 0);*/
       
    }
    IEnumerator SpawnLeft()
    {
        yield return new WaitForSeconds(1f);
        while(time < 2f)
        {
            Enemy enemy1 = GameManager.Instance.enemyPool.GetEnemy((int)EnemyType.S);
            enemy1.transform.position = new Vector3(-camH - 0.5f, camV, 0);
            enemy1.SetEnemy(1f, -1f, 50f);
            Enemy enemy2 = GameManager.Instance.enemyPool.GetEnemy((int)EnemyType.S);
            enemy2.transform.position = new Vector3(camH + 0.5f, camV, 0);
            enemy2.SetEnemy(-1f, -1f, -50f);

            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(1f);
        Enemy enemyL = GameManager.Instance.enemyPool.GetEnemy((int)EnemyType.M);
        enemyL.transform.position = new Vector3(-2f, camV, 0);
        enemyL.SetEnemy(0, -1f, 0);
        Enemy enemyM = GameManager.Instance.enemyPool.GetEnemy((int)EnemyType.M);
        enemyM.transform.position = new Vector3(0, camV, 0);
        enemyM.SetEnemy(0, -1f, 0);
        Enemy enemyR = GameManager.Instance.enemyPool.GetEnemy((int)EnemyType.M);
        enemyR.transform.position = new Vector3(2f, camV, 0);
        enemyR.SetEnemy(0, -1f, 0);


        yield return new WaitForSeconds(2f);
        Enemy enemyLB = GameManager.Instance.enemyPool.GetEnemy((int)EnemyType.B);
        enemyLB.transform.position = new Vector3(-1.5f, camV, 0);
        enemyLB.SetEnemy(0, -1f, 0);
        Enemy enemyRB = GameManager.Instance.enemyPool.GetEnemy((int)EnemyType.B);
        enemyRB.transform.position = new Vector3(1.5f, camV, 0);
        enemyRB.SetEnemy(0, -1f, 0);
    }
}

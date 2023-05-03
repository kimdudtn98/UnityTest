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
        StartCoroutine(SpawnMiddle());
        StartCoroutine(SpawnLeft());
    }

    
    void Update()
    {
        time += Time.deltaTime;
        /*if (isSpawn == false)
        {
            isSpawn = true;
            GameObject obj = GameManager.Instance.enemyPool.GetEnemy(Random.Range(0, 3));
            obj.transform.position = new Vector3(RandomNumber(), transform.position.y);
            StartCoroutine(Delay());
        }*/
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
        yield return new WaitForSeconds(1f);
        while (time < 20)
        {
            GameObject obj = GameManager.Instance.enemyPool.GetEnemy(Random.Range(0, 3));
            obj.transform.position = new Vector3(RandomNumber(), transform.position.y);
            
            yield return new WaitForSeconds(2f);
        }
        
        /*yield return new WaitForSeconds(2f);
        while(count < 5)
        {
            GameObject obj = GameManager.Instance.enemyPool.GetEnemy((int)EnemyType.S);
            obj.transform.position = new Vector3(3.45f, 5.86f);
            count++;
            yield return new WaitForSeconds(0.4f);
        }*/
    }
    IEnumerator SpawnLeft()
    {
        yield return new WaitForSeconds(1f);
        while(time < 5)
        {
            GameObject obj = GameManager.Instance.enemyPool.GetEnemy(1);
            obj.transform.position = new Vector3(-3.71f, 4.81f, 0);
            obj.transform.rotation = Quaternion.Euler(Vector3.forward * 37.225f);
            yield return new WaitForSeconds(2f);
        }
    }
}

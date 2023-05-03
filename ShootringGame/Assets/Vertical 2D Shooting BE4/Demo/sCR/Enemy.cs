using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        S,
        M,
        B
    };
    public EnemyType enemyType;
    public Sprite[] sprite;
    Rigidbody2D rigid;
    SpriteRenderer enemyspren;
    public int hp;
    public int speed;
    public int bodyDamage;
    int count;
    public float time;
    int num=1;
    bool isAttack;
    bool isleft = true;
    bool isright = false;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemyspren = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        
        if (!isAttack)
        {
            isAttack = true;
            GameObject enemybullet = GameManager.Instance.bulletPool.GetBullet(2);
            enemybullet.GetComponent<EnemyBullet>().player = GameManager.Instance.player.transform;
            enemybullet.transform.position = transform.position;
            ++count;
            StartCoroutine(Delay());
        }
    }
    private void FixedUpdate()
    {
        if (isleft)
        {
            time -= Time.deltaTime;
            if(time <= -1f)
            {
                time = 0;
                isleft = false;
                isright = true;
            }
        }
        if (isright)
        {
            time += Time.deltaTime;
            if(time >= 1f)
            {
                time = 0;
                isright = false;
                isleft = true;
            }
        }

        /*Vector2 pos;
        pos.x = Mathf.Cos(Time.time * 180 * Mathf.Deg2Rad);
        pos.y = Mathf.Sin(Time.time * 45 * Mathf.Deg2Rad);
        //rigid.velocity = pos;*/
        rigid.velocity = new Vector3(time, -1, 0);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Damaged(bodyDamage);
        }
    }
    public void Damaged(int damage)
    {
        hp -= damage;
        enemyspren.sprite = sprite[1];
        StartCoroutine(SpriteHit());
        if(hp <= 0)
        {
            gameObject.SetActive(false);
            StopCoroutine(SpriteHit());
            GameObject obj = GameManager.Instance.itemPool.GetItem(Random.Range(0, 2));
            obj.transform.position = transform.position;
        }
    }
    IEnumerator SpriteHit()
    {
        // 애니메이션으로 바꾸기
        yield return new WaitForSeconds(0.1f);
        enemyspren.sprite = sprite[0];
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        isAttack = false;
    }
}

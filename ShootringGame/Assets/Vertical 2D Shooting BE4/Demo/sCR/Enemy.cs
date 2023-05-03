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
    Animator anim;
    [SerializeField]
    Rigidbody2D rigid;
    public int hp;
    public float speed;
    public int bodyDamage;

    public float time;

    float camH, camV;

    bool isAttack;
    bool isleft = true;
    bool isright = false;

    private void OnEnable()
    {
        switch (enemyType)
        {
            case EnemyType.S:
                hp = 1;
                speed = 2;
                bodyDamage = 1;
                break;
            case EnemyType.M:
                hp = 3;
                speed = 3;
                bodyDamage = 3;
                break;
            case EnemyType.B:
                hp = 7;
                speed = 0.2f;
                bodyDamage = 5;
                break;
        }
    }
    void Init() // Start가 씹힐 수가 있기 때문에 함수로 호출을 해주는 편이 좋다
    {
        camV = Camera.main.orthographicSize;
        camH = camV * Camera.main.aspect;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {

        if (!isAttack && enemyType == EnemyType.S)
        {
            isAttack = true;
            EnemyBullet enemybullet = GameManager.Instance.enemyBulletPool.GetBullet(0);
            enemybullet.transform.position = transform.position;
            enemybullet.SmallBullet();
            
            StartCoroutine(Delay());
        }
        else if (!isAttack && enemyType == EnemyType.B)
        {
            isAttack = true;
            EnemyBullet enemybulletL = GameManager.Instance.enemyBulletPool.GetBullet(0);
            enemybulletL.transform.position = new Vector3(transform.position.x - 0.4f, transform.position.y, 0);
            enemybulletL.BigBullet();
            EnemyBullet enemybulletR = GameManager.Instance.enemyBulletPool.GetBullet(0);
            enemybulletR.transform.position = new Vector3(transform.position.x + 0.4f, transform.position.y, 0);
            enemybulletR.BigBullet();
            StartCoroutine(Delay());
        }


        if (transform.position.y <= -(camV + 0.5f) ||
            (transform.position.x <= -(camH + 0.75f) || transform.position.x >= camH + 0.75f))
        {
            gameObject.SetActive(false);
        }     
    }
    private void FixedUpdate()
    {
        /*Vector2 pos;
        pos.x = Mathf.Cos(Time.time * 180 * Mathf.Deg2Rad);
        pos.y = Mathf.Sin(Time.time * 45 * Mathf.Deg2Rad);
        //rigid.velocity = pos;*/
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
        anim.SetTrigger("isHit");
        if(hp <= 0)
        {
            gameObject.SetActive(false);
            Item obj = GameManager.Instance.itemPool.GetItem(Random.Range(0, 2));
            obj.transform.position = transform.position;
        }
    }
    
    public void SetEnemy(float x,float y, float rotationFloat)
    {
        if (rigid == null)
        {
            Init();
        }
        rigid.velocity = new Vector3(x, y, 0).normalized * speed;
        transform.rotation = Quaternion.Euler(Vector3.forward * rotationFloat);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        isAttack = false;
    }
}

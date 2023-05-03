using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum BulletType
    {
        S,
        B
    };
    public Transform bulletPos;
    public Transform parent;
    Rigidbody2D rigid;
    Animator anim;
    public int hp;
    public int power = 1;
    public float attackDelay;
    public float speed;

    public int score;

    float h, v;
    float camH;
    float camV;
    float clampH;
    float clampV;

    bool isAttack;

    Vector3 moveVec;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        camV = Camera.main.orthographicSize;
        camH = camV * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        clampH = Mathf.Clamp(transform.position.x, -camH+0.4f, camH-0.4f);
        v = Input.GetAxisRaw("Vertical");
        clampV = Mathf.Clamp(transform.position.y, -camV+0.4f, camV-0.4f);
        transform.position = new Vector3(clampH, clampV, 0);
        moveVec = new Vector3(h, v, 0).normalized;

        anim.SetInteger("isMove", (int)moveVec.x);

        Attack();
    }
    private void FixedUpdate()
    {
        rigid.velocity = moveVec * speed;
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.X) && !isAttack)
        {
            isAttack = true;
            StartCoroutine(Delay());
            switch (power)
            {
                case 1:
                    GameObject obj = GameManager.Instance.bulletPool.GetBullet((int)BulletType.S);
                    obj.transform.SetParent(parent);
                    obj.transform.position = bulletPos.transform.position;
                    break;
                case 2:
                    GameObject objL = GameManager.Instance.bulletPool.GetBullet((int)BulletType.S);
                    GameObject objR = GameManager.Instance.bulletPool.GetBullet((int)BulletType.S);
                    objL.transform.position = new Vector3(bulletPos.position.x - 0.2f, bulletPos.position.y);
                    objR.transform.position = new Vector3(bulletPos.position.x + 0.2f, bulletPos.position.y);
                    break;
                case 3:
                    GameObject objLL = GameManager.Instance.bulletPool.GetBullet((int)BulletType.S);
                    GameObject objBB = GameManager.Instance.bulletPool.GetBullet((int)BulletType.B);
                    GameObject objRR = GameManager.Instance.bulletPool.GetBullet((int)BulletType.S);
                    objLL.transform.position = new Vector3(bulletPos.position.x - 0.3f, bulletPos.position.y);
                    objBB.transform.position = new Vector3(bulletPos.position.x, bulletPos.position.y);
                    objRR.transform.position = new Vector3(bulletPos.position.x + 0.3f, bulletPos.position.y);
                    break;
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Power"))
        {
            if (power == 3)
            {
                score += 500;
                collision.gameObject.SetActive(false);
            }
            else
            {
                power += 1;
                collision.gameObject.SetActive(false);
            }
            
            
        }
    }
    public void Damaged(int damage)
    {
        hp -= damage;
        anim.SetTrigger("isHit");
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttack = false;
    }
}

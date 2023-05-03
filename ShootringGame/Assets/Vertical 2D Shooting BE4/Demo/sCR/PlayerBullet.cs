using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    Rigidbody2D rigid;
    public float speed;
    public int damage;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        rigid.velocity = Vector2.up * speed;
        if(transform.position.y >= 6f)
        {
            gameObject.SetActive(false);
        }
        //StartCoroutine(DisableBullet());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Damaged(damage);
            gameObject.SetActive(false);
        }
    }

    IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}

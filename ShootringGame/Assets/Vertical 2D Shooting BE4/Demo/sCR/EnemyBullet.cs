using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Transform player;
    Rigidbody2D rigid;
    Vector3 offset;

    public int damage;
    public float speed;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        offset = player.position - transform.position;
    }
    private void FixedUpdate()
    {
        rigid.velocity = offset.normalized * speed;
        StopCoroutine(DisableBullet());
        StartCoroutine(DisableBullet());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.player.Damaged(damage);
            gameObject.SetActive(false);
        }
    }
    IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Rigidbody2D rigid;
    float camH, camV;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        camV = Camera.main.orthographicSize;
        camH = camV * Camera.main.aspect;
    }
    private void FixedUpdate()
    {
        rigid.velocity = Vector3.down * 2f;
        if(transform.position.y <= -camV)
        {
            gameObject.SetActive(false);
        }
    }
}

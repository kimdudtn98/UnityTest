using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform[] backGrounds;
    public float speed;
    /*public int startIndex;
    public int endIndex;*/
    float camH;
    float camV;
    void Start()
    {
        camV = Camera.main.orthographicSize*2f;
        camH = camV * Camera.main.aspect;
        Debug.Log(camV);
    }
    void Update()
    {
        for (int i = 0; i < backGrounds.Length; i++)
        {
            backGrounds[i].Translate(Vector3.down * (i + speed) * Time.deltaTime);
            if (backGrounds[i].transform.localPosition.y <= -camV)
            {
                backGrounds[i].transform.localPosition = Vector3.up * camV ;
            }
        }

        /*if (backGrounds[endIndex].transform.position.y <= -camV)
        {
            Vector3 backSpritePos = backGrounds[startIndex].localPosition;
            Vector3 frontSpritePos = backGrounds[endIndex].localPosition;
            backGrounds[endIndex].transform.localPosition = backSpritePos + Vector3.up * camV;

            int save = startIndex;  // 0 -> save
            startIndex = endIndex; // 2
            endIndex = (save - 1 == -1) ? backGrounds.Length - 1 : save - 1;
        }*/
    }
}

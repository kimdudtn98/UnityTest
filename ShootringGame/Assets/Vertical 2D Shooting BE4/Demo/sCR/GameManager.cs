using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public Player player;
    public BulletPool bulletPool;
    public EnemyPool enemyPool;
    public EnemyBulletPool enemyBulletPool;
    public ItemPool itemPool;
    float halfHeight = 0;
    float halfWidth = 0;
    public int score = 0;
    public float Height
    {
        get
        {
            if (halfHeight == 0)
                Init();
            return halfHeight;
        }
    }
    public float Width
    {
        get
        {
            if (halfWidth == 0)
                Init();
            return halfWidth;
        }
    }
    void Init()
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }
    public void SetScore(int score)
    {
        this.score += score;
    }
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if(instance != null)
            {
                Destroy(this);
            }
        }
    }
}

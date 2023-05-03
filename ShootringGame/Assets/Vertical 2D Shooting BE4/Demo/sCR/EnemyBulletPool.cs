using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    public EnemyBullet[] bulletPrefab;
    public Transform parent;
    List<EnemyBullet>[] pools;
    private void Awake()
    {
        pools = new List<EnemyBullet>[bulletPrefab.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<EnemyBullet>();
        }
    }

    public EnemyBullet GetBullet(int index)
    {
        EnemyBullet obj = null;
        foreach (var item in pools[index])
        {
            if (!item.gameObject.activeSelf)
            {
                obj = item;
                obj.gameObject.SetActive(true);
                break;
            }
        }
        if (obj == null)
        {
            obj = Instantiate(bulletPrefab[index],transform);
            obj.transform.SetParent(parent);
            obj.gameObject.SetActive(true);
            pools[index].Add(obj);
        }
        return obj;
    }
}

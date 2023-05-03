using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject[] bulletPrefab;
    List<GameObject>[] pools;
    private void Awake()
    {
        pools = new List<GameObject>[bulletPrefab.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }
    
    public GameObject GetBullet(int index)
    {
        GameObject obj = null;
        foreach (var item in pools[index])
        {
            if (!item.activeSelf)
            {
                obj = item;
                obj.SetActive(true);
                break;
            }
        }
        if(obj == null)
        {
            obj = Instantiate(bulletPrefab[index]);
            obj.SetActive(true);
            pools[index].Add(obj);
        }
        return obj;
    }
}

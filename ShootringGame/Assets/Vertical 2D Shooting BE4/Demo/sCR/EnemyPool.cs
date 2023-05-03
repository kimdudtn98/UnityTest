using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    List<GameObject>[] pools;
    private void Awake()
    {
        pools = new List<GameObject>[enemyPrefabs.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject GetEnemy(int index)
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
        if (obj == null)
        {
            obj = Instantiate(enemyPrefabs[index]);
            obj.SetActive(true);
            pools[index].Add(obj);
        }
        return obj;
    }
}

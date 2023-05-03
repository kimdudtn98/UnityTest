using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public Enemy[] enemyPrefabs;
    public Transform[] parents;
    List<Enemy>[] pools;

    
    private void Awake()
    {
        
        pools = new List<Enemy>[enemyPrefabs.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<Enemy>();
        }
    }

    public Enemy GetEnemy(int index)
    {
        Enemy obj = null;
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
            obj = Instantiate(enemyPrefabs[index],transform);
            obj.transform.SetParent(parents[index]);
            obj.gameObject.SetActive(true);
            pools[index].Add(obj);
        }
        return obj;
    }
}

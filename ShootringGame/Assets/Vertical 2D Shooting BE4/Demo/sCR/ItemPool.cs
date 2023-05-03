using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    List<GameObject>[] pools;
    private void Awake()
    {
        pools = new List<GameObject>[itemPrefabs.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject GetItem(int index)
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
            obj = Instantiate(itemPrefabs[index]);
            obj.SetActive(true);
            pools[index].Add(obj);
        }
        return obj;
    }
}

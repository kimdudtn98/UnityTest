using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public Item[] itemPrefabs;
    List<Item>[] pools;
    private void Awake()
    {
        pools = new List<Item>[itemPrefabs.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<Item>();
        }
    }

    public Item GetItem(int index)
    {
        Item obj = null;
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
            obj = Instantiate(itemPrefabs[index]);
            obj.gameObject.SetActive(true);
            pools[index].Add(obj);
        }
        return obj;
    }
}

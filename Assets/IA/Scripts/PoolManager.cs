using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefab;

    List<GameObject>[] pool;

    private void Awake()
    {
        pool = new List<GameObject>[prefab.Length];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index, Vector3 pos)
    {
        GameObject select = null;

        foreach(GameObject item in pool[index])
        {
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                select.transform.position = pos;
                break;
            }
        }

        if(select == null)
        {
            select = Instantiate(prefab[index], transform);
            select.transform.position = pos;
            pool[index].Add(select);
        }


        return select;
    }
}

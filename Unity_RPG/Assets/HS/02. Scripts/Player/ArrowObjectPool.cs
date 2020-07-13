using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowObjectPool : MonoBehaviour
{
    public static ArrowObjectPool Instance;

    [SerializeField]
    private GameObject poolingObjectFactory;

    Queue<Arrow> poolingObjectQueue = new Queue<Arrow>();

    private void Awake()
    {
        Instance = this;
        Initialize(10);
    }
    private void Initialize(int initcount)
    {
        for (int i = 0; i < initcount; i++) 
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Arrow CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectFactory).GetComponent<Arrow>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Arrow GetObject(GameObject firePos)
    {
        if(Instance.poolingObjectQueue.Count>0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.position = firePos.transform.position;
            obj.transform.rotation = firePos.transform.rotation;
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.transform.position = firePos.transform.position;
            newObj.transform.rotation = firePos.transform.rotation;
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }
    public static void ReturnObject(Arrow obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;

        Instance.poolingObjectQueue.Enqueue(obj);
    }
}

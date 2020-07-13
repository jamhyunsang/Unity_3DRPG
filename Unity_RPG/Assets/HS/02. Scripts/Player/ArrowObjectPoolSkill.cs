using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowObjectPoolSkill : MonoBehaviour
{
    public static ArrowObjectPoolSkill Instance;

    [SerializeField]
    private GameObject poolingObjectSkillFactory;

    Queue<ArrowSkill> poolingObjectQueue = new Queue<ArrowSkill>();

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

    private ArrowSkill CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectSkillFactory).GetComponent<ArrowSkill>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static ArrowSkill GetObject(GameObject firePos)
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
    public static void ReturnObject(ArrowSkill obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;

        Instance.poolingObjectQueue.Enqueue(obj);
    }
}

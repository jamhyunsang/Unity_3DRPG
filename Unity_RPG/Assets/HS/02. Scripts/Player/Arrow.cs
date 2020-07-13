using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Arrow : MonoBehaviour
{
  
    public float timer;    

    private void Update()
    {
        if(gameObject.activeSelf==true)
        {
            timer += Time.deltaTime;
            transform.Translate(Vector3.forward * 10 * Time.deltaTime);
            if (timer > 10.0f)
            {
                timer = 0.0f;
                ArrowObjectPool.ReturnObject(this);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("ENEMY"))
        {
            collision.gameObject.GetComponent<EnemyState>().hitDamage(30);
            ArrowObjectPool.ReturnObject(this);
        }

        
    }
}

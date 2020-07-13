using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ArrowSkill : MonoBehaviour
{
  
    public float timer;

    
    public GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(gameObject.activeSelf==true)
        {
            timer += Time.deltaTime;
            transform.Translate(Vector3.forward * 10 * Time.deltaTime);
        }
        if (timer > 10.0f)
        {
            timer = 0.0f;
            ArrowObjectPoolSkill.ReturnObject(this);
        }
    }
   

   
   
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ENEMY"))
        {

            other.gameObject.GetComponent<EnemyState>().hitDamage(100);
            ArrowObjectPoolSkill.ReturnObject(this);
            Instantiate(bomb, other.transform.position, Quaternion.Euler(0, 0, 0));

        }
    }

}

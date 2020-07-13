using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BossCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

        if (transform.position.x>95)
        {
            StartCoroutine(bossCam());
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * 3, Space.World);
        }    
    }

    IEnumerator bossCam()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    public GameObject camera;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name=="Player")
        {
            camera.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

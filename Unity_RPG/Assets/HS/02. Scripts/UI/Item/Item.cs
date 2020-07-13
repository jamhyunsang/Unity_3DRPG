using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Rigidbody rb;
    private Potion potion;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        potion = GameObject.Find("Potion").GetComponent<Potion>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            potion.GetPotion();
            Destroy(gameObject);
        }
    }
   }

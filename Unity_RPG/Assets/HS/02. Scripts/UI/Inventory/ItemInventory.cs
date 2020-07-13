using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    public GameObject Item;

    public void OnClickButton()
    {
        if(Item.activeSelf)
        {
            Item.SetActive(false);
        }
        else
        {
            Item.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    private PlayerState ps;
    int potion;
    public Text potiontext;


    private void Start()
    {
        ps = GameObject.Find("Player").GetComponent<PlayerState>();
    }

    public void GetPotion()
    {
        potion++;
        potiontext.text = potion.ToString();
    }

    public void OnPotion()
    {
        if(potion>0)
        {
            ps.heal();
            potion--;
            potiontext.text = potion.ToString();
        }
    }
}

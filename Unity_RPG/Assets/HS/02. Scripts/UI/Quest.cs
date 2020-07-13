using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{

   public static Quest instance;

    public GameObject brigecamera;
    public GameObject quest;
    public int huntingCount;
    public Text progress;
    public GameObject brige;
    public GameObject bang;
    public GameObject button;
    public GameObject button2;

    public void Awake()
    {
        instance = this;
    }


    public void hunting()
    {
        huntingCount++;
        progress.text = "잡은수 : " + huntingCount.ToString() + "/10";
    }

    public void OnClick()
    {
        if(huntingCount>=10)
        {
            quest.SetActive(false);
            brigecamera.SetActive(true);
            bang.SetActive(true);
            brige.SetActive(true);
            StartCoroutine(brigeCreate());
        }
    }
    public void OnClick2()
    {
        quest.SetActive(false);
        button2.SetActive(true);
        button.SetActive(false);

    }

    IEnumerator brigeCreate()
    {
        yield return new WaitForSeconds(3.0f);
        brigecamera.SetActive(false);
        bang.SetActive(false);
    }


}

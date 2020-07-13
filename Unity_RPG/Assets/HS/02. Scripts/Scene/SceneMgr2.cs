using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class SceneMgr2 : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    // Update is called once per frame
    void Update()
    {
        if(player==null)
        {
            StartCoroutine(Loser());
        }
        else if(boss==null)
        {
            StartCoroutine(Victory());
        }
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("VictoryScene");
    }
    IEnumerator Loser()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("LoserScene");
    }
}

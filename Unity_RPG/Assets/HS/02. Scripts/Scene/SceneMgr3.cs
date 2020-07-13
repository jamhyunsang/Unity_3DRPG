using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr3 : MonoBehaviour
{

    // Start is called before the first frame update
 
    public void OnStartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void OnGameScene()
    {
        SceneManager.LoadScene(1);
    }
}

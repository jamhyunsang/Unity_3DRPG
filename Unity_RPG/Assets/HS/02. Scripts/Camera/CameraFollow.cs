using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //플레이어 가져오기
    public GameObject player;
    public float cameraY;
    public float cameraZ;

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        transform.position=player.transform.position - new Vector3(0, cameraY, cameraZ);
    }
}

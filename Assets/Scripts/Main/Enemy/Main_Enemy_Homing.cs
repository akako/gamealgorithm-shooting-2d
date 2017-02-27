using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransformExtension;

public class Main_Enemy_Homing : Main_Enemy_Base
{
    public float speed;

    void Update()
    {
        if (IsDestroying)
        {
            return;
        }

        if (null != Main_SceneController.Instance.player)
        {
            transform.LookAt2D(Main_SceneController.Instance.player.transform);
            rigidbodyCache.velocity = (Main_SceneController.Instance.player.transform.position - transform.position).normalized * speed;
        }
    }
}

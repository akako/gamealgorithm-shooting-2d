using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Enemy_Simple : Main_Enemy_Base
{
    public Vector2 velocity;

    void Update()
    {
        rigidbodyCache.velocity = velocity;
    }
}

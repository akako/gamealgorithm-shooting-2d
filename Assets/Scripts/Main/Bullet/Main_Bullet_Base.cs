using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Main_Bullet_Base : MonoBehaviour
{
    protected Rigidbody2D rigidbodyCache;

    void Awake()
    {
        rigidbodyCache = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        // 画面から出たら破棄
        if (Mathf.Abs(transform.position.x) > 4f || Mathf.Abs(transform.position.y) > 7f)
        {
            Destroy(gameObject);
        }
    }
}

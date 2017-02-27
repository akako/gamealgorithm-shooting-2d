using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Main_Bullet_Missile : Main_Bullet_Base
{
    Transform target;

    void Start()
    {
        if (Main_SceneController.Instance.enemies.Count > 0)
        {
            target = Main_SceneController.Instance.enemies.OrderBy(x => null == x ? 99999f : Vector3.Distance(x.transform.position, transform.position)).First().transform;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (null == gameObject)
        {
            return;
        }

        if (null != target)
        {
            rigidbodyCache.velocity = (target.transform.position - transform.position).normalized * 3f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Enemy_OneShotEscape : Main_Enemy_Base
{
    [SerializeField]
    ParticleSystem shot;

    void Start()
    {
        StartCoroutine(OneShotCoroutine());
    }

    IEnumerator OneShotCoroutine()
    {
        rigidbodyCache.velocity = Vector2.down * 0.5f;
        while (transform.position.y > 3.5f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        rigidbodyCache.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        shot.Play();
        yield return new WaitForSeconds(1f);
        rigidbodyCache.velocity = Vector2.up * 0.5f;
    }
}

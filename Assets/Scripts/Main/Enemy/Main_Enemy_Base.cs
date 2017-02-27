using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵基底クラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
abstract public class Main_Enemy_Base : Main_Mob_Base
{
    [SerializeField]
    int score;
    [SerializeField]
    ParticleSystem explosionParticle;
    [SerializeField]
    float itemDropRate;
    [SerializeField]
    Main_Item_Base itemPrefab;

    protected Rigidbody2D rigidbodyCache;

    void Awake()
    {
        rigidbodyCache = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 画面から出たら破棄
        if (Mathf.Abs(transform.position.x) > 4f || Mathf.Abs(transform.position.y) > 7f)
        {
            Destroy(gameObject);
        }
    }

    override protected IEnumerator DestroyCoroutine()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Main_SceneController.Instance.Score += score;
        explosionParticle.Play();
        Main_SoundManager.Instance.explosion.Play();
        if (null != itemPrefab)
        {
            if (Random.Range(0, 10000) <= Mathf.CeilToInt(itemDropRate * 10000))
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

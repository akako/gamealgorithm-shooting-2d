using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 動くもの基底クラス
/// </summary>
abstract public class Main_Mob_Base : MonoBehaviour
{
    [SerializeField]
    int life;

    bool isDestroying = false;

    /// <summary>
    /// ライフ
    /// </summary>
    /// <value>The life.</value>
    public virtual int Life
    {
        set
        {
            if (isDestroying)
            {
                return;
            }
            life = value; 
            if (life <= 0)
            {
                isDestroying = true;
                StartCoroutine(DestroyCoroutine());
            }
        }
        get { return life; }
    }

    /// <summary>
    /// 破壊され中かどうか
    /// </summary>
    /// <value><c>true</c> if this instance is destroying; otherwise, <c>false</c>.</value>
    protected bool IsDestroying
    {
        get { return isDestroying; }
    }

    /// <summary>
    /// パーティクルがぶつかった際の処理
    /// </summary>
    /// <param name="other">Other.</param>
    void OnParticleCollision(GameObject other)
    {
        Damage();
    }

    /// <summary>
    /// オブジェクトがぶつかった際の処理
    /// </summary>
    /// <param name="collision2D">Collision2 d.</param>
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Damage();
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    void Damage()
    {
        if (!isDestroying)
        {
            Main_SoundManager.Instance.hit.Play();
            Life--;
        }
    }

    abstract protected IEnumerator DestroyCoroutine();
}

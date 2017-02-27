using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Main_Player : Main_Mob_Base
{
    [SerializeField]
    ParticleSystem straightShot;
    [SerializeField]
    ParticleSystem sideShotLeft;
    [SerializeField]
    ParticleSystem sideShotRight;
    [SerializeField]
    Main_Bullet_Missile missilePrefab;

    int straightShotLevel = 0;
    int sideShotLevel = 0;
    int missileLevel = 0;
    Vector3 previousMousePosition = Vector3.zero;

    public override int Life
    {
        set
        { 
            base.Life = value;
            Main_SceneController.Instance.ui.Life = base.Life;
        }
        get { return base.Life; }
    }

    /// <summary>
    /// 正面ショットのレベル
    /// </summary>
    /// <value>The straight shot level.</value>
    public int StraightShotLevel
    {
        set
        {
            straightShotLevel = Math.Max(0, value);
            straightShot.emissionRate = 2 + Math.Max(0, value);
        }
        get { return straightShotLevel; }
    }

    /// <summary>
    /// 側面ショットのレベル
    /// </summary>
    /// <value>The side shot level.</value>
    public int SideShotLevel
    {
        set
        {
            sideShotLevel = Math.Max(0, value);
            sideShotLeft.emissionRate = sideShotLevel;
            sideShotRight.emissionRate = sideShotLevel;
        }
        get { return sideShotLevel; }
    }

    /// <summary>
    /// ミサイルのレベル
    /// </summary>
    /// <value>The missile level.</value>
    public int MissileLevel
    {
        set
        {
            missileLevel = Math.Max(0, value);
        }
        get { return missileLevel; }
    }

    void Awake()
    {
        missilePrefab.gameObject.SetActive(false);
    }

    void Start()
    {
        Life = Life;
        StraightShotLevel = 0;
        SideShotLevel = 0;
        MissileLevel = 0;
        straightShot.Play();
        sideShotLeft.Play();
        sideShotRight.Play();
        StartCoroutine(ShotMissileCoroutine());
    }

    void Update()
    {
        if (IsDestroying)
        {
            return;
        }

        // キャラの移動制御
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = MousePositionToWorldPosition(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            var baseMousePosition = Input.mousePosition;
            baseMousePosition.z = -Camera.main.transform.position.z;
            var mousePosition = Camera.main.ScreenToWorldPoint(baseMousePosition);
            transform.position += mousePosition - previousMousePosition;
            previousMousePosition = mousePosition;
        }
    }

    /// <summary>
    /// ミサイル発射コルーチン
    /// </summary>
    /// <returns>The missile coroutine.</returns>
    IEnumerator ShotMissileCoroutine()
    {
        while (true)
        {
            if (missileLevel > 0)
            {
                var missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
                missile.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(Math.Max(0.6f, 3f - missileLevel * 0.3f));
        }
    }

    /// <summary>
    /// マウスポジションをWorld座標に変換します
    /// </summary>
    /// <returns>The position to world position.</returns>
    /// <param name="mousePosition">Mouse position.</param>
    Vector3 MousePositionToWorldPosition(Vector3 mousePosition)
    {
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    /// <summary>
    /// 破壊された際のコルーチン
    /// </summary>
    /// <returns>The coroutine.</returns>
    protected override IEnumerator DestroyCoroutine()
    {
        Main_SoundManager.Instance.explosion.Play();

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

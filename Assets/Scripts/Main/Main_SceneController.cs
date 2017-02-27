using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Main_SceneController : MonoBehaviour
{
    static Main_SceneController instance;

    public Main_UI ui;
    public List<Main_Enemy_Base> enemies = new List<Main_Enemy_Base>();
    public Main_Player player;

    // 敵キャラのプレハブ
    [SerializeField]
    Main_Enemy_Homing bossPrefab;
    [SerializeField]
    Main_Enemy_Simple enemySimplePrefab;
    [SerializeField]
    Main_Enemy_Homing enemyHomingPrefab;
    [SerializeField]
    Main_Enemy_OneShotEscape enemyOneShotEscapePrefab;

    int score = 0;

    public static Main_SceneController Instance
    {
        get { return instance; }
    }

    public int Score
    {
        set
        {
            score = value;
            ui.Score = score;
        }
        get { return score; }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Score = 0;
        StartCoroutine(SpawnEnemyCoroutine());
    }

    /// <summary>
    /// 敵の出現コルーチン
    /// </summary>
    /// <returns>The enemy coroutine.</returns>
    IEnumerator SpawnEnemyCoroutine()
    {
        while (0 < player.Life)
        {
            enemies.RemoveAll(x => null == x);
            var rand = Random.Range(0, 31);
            if (player.StraightShotLevel >= 3 && rand == 0)
            {
                // ボス出現
                var enemy = Instantiate(bossPrefab, new Vector3(0f, 6f), Quaternion.identity);
                enemy.Life = 50 + player.StraightShotLevel * 5;
                enemy.speed = 0.5f + player.StraightShotLevel * 0.1f;
                enemies.Add(enemy);
            }
            else if (rand < 11)
            {
                // ウイルス出現（パターン１）
                for (var i = 0; i < 10; i++)
                {
                    var enemy = Instantiate(enemySimplePrefab, new Vector3(3f, 6f), Quaternion.identity);
                    enemy.Life = 1 + player.StraightShotLevel / 3;
                    enemy.velocity = new Vector2(-1f, -1f);
                    enemies.Add(enemy);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (rand < 21)
            {
                // ウイルス出現（パターン２）
                for (var i = 0; i < 10; i++)
                {
                    var enemy = Instantiate(enemySimplePrefab, new Vector3(-3f, 6f), Quaternion.identity);
                    enemy.Life = 1 + player.StraightShotLevel / 3;
                    enemy.velocity = new Vector2(1f, -1f);
                    enemies.Add(enemy);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (rand < 26)
            {
                // エビ出現
                var spawnAmount = Random.Range(3, System.Math.Min(11, 4 + player.StraightShotLevel));
                for (var i = 0; i < spawnAmount; i++)
                {
                    var enemy = Instantiate(enemyOneShotEscapePrefab, new Vector3(-2f + i * 4f / spawnAmount, 6f), Quaternion.identity);
                    enemy.Life = 10 + player.StraightShotLevel;
                    enemies.Add(enemy);
                }
            }
            else if (rand < 31)
            {
                // ダミー出現
                var spawnAmount = Random.Range(3, System.Math.Min(11, 4 + player.StraightShotLevel));
                for (var i = 0; i < spawnAmount; i++)
                {
                    var enemy = Instantiate(enemyHomingPrefab, new Vector3(Random.Range(-3f, 3f), 6f), Quaternion.identity);
                    enemy.Life = 5 + player.StraightShotLevel / 2;
                    enemy.speed = 1f + player.StraightShotLevel * 0.05f;
                    enemies.Add(enemy);
                    yield return new WaitForSeconds(0.3f);
                }
            }
            yield return new WaitForSeconds(Mathf.Min(0.5f, 3f - player.StraightShotLevel * 0.1f));
        }
    }
}

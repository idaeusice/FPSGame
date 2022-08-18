using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject iguanaPrefab;
    [SerializeField] private UIController ui;
    
    private Vector3 enemySpawn = new Vector3(0, 0, 5);
    private Vector3 friendlySpawn = new Vector3(21, 0, -17);
    private GameObject[] enemies;
    private GameObject[] friendlies;
    private GameObject enemy;
    private int enemyMax = 5;
    private int iguanaMax = 8;
    private float spawnTimer = 3.0f;
    private int score = 0;

    void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger<int>.AddListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger<int>.RemoveListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
    }

    int GetDifficulty()
    {
        return PlayerPrefs.GetInt("difficulty", 1);
    }

    void OnEnemyDead()
    {
        score++;
        ui.UpdateScore(score);
    }

    private void OnDifficultyChanged(int newDifficulty)
    {
        Debug.Log("Scene.OnDifficultyChanged(" + newDifficulty + ")");
        for (int i = 0; i < enemies.Length; i++)
        {
            WanderingAI ai = enemies[i].GetComponent<WanderingAI>();
            ai.SetDifficulty(newDifficulty);
        }
    }

    private void Start()
    {
        enemies = new GameObject[enemyMax];
        friendlies = new GameObject[iguanaMax];

        for (int i = 0; i < friendlies.Length; i++)
        {
            if (friendlies[i] == null)
            {
                friendlies[i] = Instantiate(iguanaPrefab) as GameObject;
                friendlies[i].transform.position = friendlySpawn;
                float angle = Random.Range(0, 360);
                friendlies[i].transform.Rotate(0, angle, 0);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                enemies[i] = Instantiate(enemyPrefab) as GameObject;
                enemies[i].transform.position = enemySpawn;
                float angle = Random.Range(0, 360);
                enemies[i].transform.Rotate(0, angle, 0);
                enemies[i].GetComponent<WanderingAI>().SetDifficulty(GetDifficulty());
            }
        }
    }
}

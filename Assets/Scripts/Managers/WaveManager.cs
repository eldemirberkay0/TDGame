using UnityEngine;
using FlexTimer;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    public int CurrentWave { get; private set; } = 0;

    private Timer waveTimer;
    private Wave waveData;

    private int enemyCount;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        waveTimer = new Timer(0f);
        waveTimer.OnUpdate += () => UIManager.Instance.SetWaveTimeFill(1 - waveTimer.TimeToFinishNormalized);
        waveTimer.OnFinished += () => StartWave();
    }

    private void OnEnable()
    {
        LevelManager.OnLevelStarted += PrepareWave;
    }

    private void PrepareWave()
    {
        waveData = LevelManager.Instance.LevelData.waves[CurrentWave];
        UIManager.Instance.SetWaveTimer(true);
        float timeBeforeWave = waveData.durationToStart;
        waveTimer.Restart(timeBeforeWave);
    }

    public void StartWave()
    {
        CurrentWave++;
        UIManager.Instance.UpdateWave();
        UIManager.Instance.SetWaveTimer(false);
        waveTimer.Pause();
        float goldReward = waveData.maxTimeReward * waveTimer.TimeToTickNormalized;
        PlayerStats.SetGold(PlayerStats.Gold + goldReward);
        if ((int)goldReward > 0)
        {
            UIManager.Instance.AnimateTimeReward((int)goldReward);
        }
        StartCoroutine(SpawnWave(LevelManager.Instance.LevelData, CurrentWave));
    }

    private IEnumerator SpawnWave(Level level, int wave)
    {
        foreach (EnemyGroup group in level.waves[wave - 1].enemyGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + Random.Range(-0.05f, 0.4f), transform.position.z);
                Instantiate(group.enemy, spawnPos, transform.rotation).SetActive(true);
                enemyCount++;
                yield return new WaitForSeconds(group.interval);
            }
            yield return new WaitForSeconds(group.delay);
        }
        yield return new WaitForSeconds(waveData.durationToNext);
        if (CurrentWave < LevelManager.Instance.LevelData.waves.Length) { PrepareWave(); }
    }

    public void UpdateEnemyCounter()
    {
        enemyCount--;
        if (enemyCount == 0 && CurrentWave == LevelManager.Instance.LevelData.waves.Length) { LevelManager.OnLevelVictory?.Invoke(); }
    }

    private void OnDisable()
    {
        LevelManager.OnLevelStarted -= PrepareWave;
    }
}

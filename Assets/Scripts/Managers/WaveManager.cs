using UnityEngine;
using FlexTimer;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    public int CurrentWave { get; private set; } = 0;

    private Timer waveTimer;
    private Wave waveData;

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
                Instantiate(group.enemy, transform.position, transform.rotation).SetActive(true);
                yield return new WaitForSeconds(group.interval);
            }
            yield return new WaitForSeconds(group.delay);
        }
        yield return new WaitForSeconds(waveData.durationToNext);
        if (CurrentWave < LevelManager.Instance.LevelData.waves.Length) { PrepareWave(); }
    }

    private void OnDisable()
    {
        LevelManager.OnLevelStarted -= PrepareWave;
    }
}

using System.Collections;
using FlexTimer;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private Level[] levels;
    public Level LevelData { get; private set; }
    public int CurrentWave { get; private set; } = 0;

    private Timer waveTimer = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        GameManager.OnGameStarted += SetLevel;
    }

    private void SetLevel()
    {
        Debug.Log("Level Started");
        CurrentWave = 0;
        this.LevelData = levels[PlayerStats.CurrentLevel - 1];
        PlayerStats.SetGold(this.LevelData.initialGold);
        PlayerStats.SetLive(this.LevelData.health);
        UIManager.Instance.SetWaveTimer(true);

        float timeBeforeWave = LevelData.waves[CurrentWave].time;
        if (waveTimer == null)
        {
            waveTimer = new Timer(timeBeforeWave);
            waveTimer.OnUpdate += () => UIManager.Instance.SetWaveTimeFill(1 - waveTimer.TimeToFinishNormalized);
            waveTimer.OnFinished += () => StartWave();
            waveTimer.Start();
            return;
        }
        waveTimer.Restart(timeBeforeWave);
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
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= SetLevel;
    }

    public void StartWave()
    {
        UIManager.Instance.SetWaveTimer(false);
        waveTimer.Pause();
        CurrentWave++;
        float goldReward = LevelData.waves[CurrentWave - 1].maxTimeReward * waveTimer.TimeToTickNormalized;
        if (goldReward > 0)
        {
            PlayerStats.SetGold(PlayerStats.Gold + goldReward);
        }
        StartCoroutine(SpawnWave(LevelData, CurrentWave));
    }
}

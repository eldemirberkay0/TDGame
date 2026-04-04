using System.Collections;
using FlexTimer;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] levels;
    private Level level;
    public Vector3 pos;

    private void OnEnable()
    {
        GameManager.OnLevelStarted += SetLevel;
    }

    private void SetLevel()
    {
        Debug.Log("Level Started");
        Debug.Log("Spawning waves in 1 seconds");
        this.level = levels[PlayerStats.CurrentLevel - 1];
        TimerManager.RegisterEvent(1, () => StartCoroutine(SpawnWaves(this.level)));
        PlayerStats.SetGold(this.level.initialGold);
        PlayerStats.SetLive(this.level.health);
    }

    private IEnumerator SpawnWaves(Level level)
    {
        foreach (Wave wave in level.waves)
        {
            foreach (EnemyGroup group in wave.enemyGroups)
            {
                for (int i = 0; i < group.count; i++)
                {
                    Instantiate(group.enemy, transform.position, transform.rotation).SetActive(true);
                    yield return new WaitForSeconds(group.interval);
                }
                yield return new WaitForSeconds(group.delay);
            }
        }
    }

    private void OnDisable()
    {
        GameManager.OnLevelStarted -= SetLevel;
    }
}

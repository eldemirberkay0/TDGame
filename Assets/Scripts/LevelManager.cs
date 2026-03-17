using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using FlexTimer;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] levels;

    void Awake()
    {
        GameManager.OnLevelStarted += SetLevel;
    }

    private void SetLevel(int level)
    {
        Debug.Log("Level Started");
        Debug.Log("Spawning waves in 1 seconds");
        TimerManager.RegisterEvent(1, () => StartCoroutine(SpawnWaves(levels[level - 1])));
    }

    private IEnumerator SpawnWaves(Level level)
    {
        foreach (Wave wave in level.waves)
        {
            yield return new WaitForSeconds(wave.delay);
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
}

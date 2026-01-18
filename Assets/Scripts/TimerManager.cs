using System.Collections.Generic;
using System.Threading;
using Unity.Collections;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private List<Timer> timers = new List<Timer>();
    public static TimerManager Instance;
    
    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); }
        else { Instance = this; }
    }

    void Update()
    {
        foreach (Timer  timer in timers)
        {
            timer.Tick();
        }
    }

    public void RegisterTimer(Timer timer)
    {
        timers.Add(timer);
    }
}

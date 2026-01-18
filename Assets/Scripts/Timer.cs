using System;
using UnityEngine;

public class Timer
{
    private float initialTime;
    public float CurrentTime { get; private set; }
    public bool IsRunning { get; private set; }

    public Action OnTimerFinished;

    public Timer(float duration) 
    {
        initialTime = duration;
    }

    public void Start()
    {
        if (!IsRunning)
        {
            CurrentTime = initialTime;
            IsRunning = true;
            TimerManager.Instance.RegisterTimer(this);
        }
    }

    public void Tick()
    {
        if (IsRunning) { CurrentTime -= Time.deltaTime; }
        if (CurrentTime <= 0)
        {
            OnTimerFinished?.Invoke();
        }
    }
}

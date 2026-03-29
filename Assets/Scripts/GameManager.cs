using System;
using FlexTimer;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnLevelStarted = null;

    // Change Start to an event in final
    private void Start()
    {
        Debug.Log("Starting level in 2 seconds...");
        TimerManager.RegisterEvent(1.9f, () => PlayerStats.SetCurrentLevel(1));
        TimerManager.RegisterEvent(2, () => OnLevelStarted?.Invoke());
    }
}

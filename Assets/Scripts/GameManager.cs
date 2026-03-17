using System;
using FlexTimer;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<int> OnLevelStarted = null;

    // Change Start to an event in final
    private void Start()
    {
        Debug.Log("Starting level in 2 seconds...");
        TimerManager.RegisterEvent(2, () => OnLevelStarted?.Invoke(1));
    }
}

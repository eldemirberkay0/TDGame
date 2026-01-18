using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectHandler : MonoBehaviour
{
    public List<Effect> CurrentEffects { get; private set; } = new List<Effect>();

    public void AddEffect(Effect effect)
    {
        CurrentEffects.Add(effect);
    }
}


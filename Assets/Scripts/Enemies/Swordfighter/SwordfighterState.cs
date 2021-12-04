using UnityEngine;

public class SwordfighterState : EnemyState<Swordfighter>
{
    protected Animator animator;
    protected AudioManager audioManager;
    
    protected static new T Create<T>(Swordfighter target) where T : SwordfighterState
    {
        var state = EnemyState<Swordfighter>.Create<T>(target);
        state.animator = target.GetComponentInChildren<Animator>();
        state.audioManager = target.GetComponent<AudioManager>();
        return state;
    }
}
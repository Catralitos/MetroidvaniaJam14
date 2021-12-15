using Audio;
using Enemies.Base;
using UnityEngine;

namespace Enemies.Swordfighter
{
    public class SwordfighterState : EnemyState<Swordfighter>
    {
        protected Animator animator;
    
        protected static new T Create<T>(Swordfighter target) where T : SwordfighterState
        {
            var state = EnemyState<Swordfighter>.Create<T>(target);
            state.animator = target.GetComponentInChildren<Animator>();
            return state;
        }
    }
}
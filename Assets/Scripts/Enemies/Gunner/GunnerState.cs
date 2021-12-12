using Enemies.Base;
using UnityEngine;

namespace Enemies.Gunner
{
    public class GunnerState : EnemyState<Gunner>
    {
        protected Animator animator;

        protected static new T Create<T>(Gunner target) where T : GunnerState
        {
            var state = EnemyState<Gunner>.Create<T>(target);
            state.animator = target.GetComponentInChildren<Animator>();
            return state;
        }
    
    }
}
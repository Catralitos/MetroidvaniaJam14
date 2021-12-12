using UnityEngine;

namespace Enemies.Base
{
    public abstract class EnemyState : MonoBehaviour
    {
        public bool Initialized { get; protected set; }
        
        public virtual void OnGetHit()
        {
        }
    
    }

    public abstract class EnemyState<EnemyType> : EnemyState where EnemyType : EnemyBase<EnemyType>
    {
        protected EnemyType target;

        protected static T Create<T>(EnemyType target) where T : EnemyState<EnemyType>
        {
            T state = target.gameObject.AddComponent<T>();
            state.target = target;
            return state;
        }

        public virtual void StateStart()
        {
            if (!target.IsAlive) return;
            Initialized = true;
        }

        public virtual void StateUpdate()
        {
            if (!target.IsAlive) return;

        }

        public virtual void StateFixedUpdate()
        {
            if (!target.IsAlive) return;
        }
        
        protected void SetState(EnemyState<EnemyType> state)
        {
            if (!target.IsAlive) return;
            target.SetState(state);
            Destroy(this);
        }
    }
}
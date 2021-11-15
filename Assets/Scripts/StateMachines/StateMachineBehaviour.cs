using UnityEngine;

namespace GDT.StateMachines
{
    public abstract class StateMachineBehaviourBase : MonoBehaviour
    {
        protected abstract void Start();
    }

    public abstract class StateMachineBehaviour<EnemyType> : StateMachineBehaviourBase where EnemyType : StateMachineBehaviour<EnemyType>
    {
        public State<EnemyType> state { get; protected set; }

        public void SetState(State<EnemyType> state)
        {
            this.state = state;
        }

        protected virtual void Update()
        {
            if (state)
            {
                if (!state.Initialized)
                {
                    state.StateStart();
                }
                state.StateUpdate();
            }
        }
    }
}
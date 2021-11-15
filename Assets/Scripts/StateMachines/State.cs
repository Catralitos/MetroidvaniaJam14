using UnityEngine;

namespace GDT.StateMachines
{
    public abstract class StateBase : MonoBehaviour
    {
        [HideInInspector] public bool Initialized = false;

        public virtual void StateStart()
        {
            Initialized = true;
        }

        public abstract void StateUpdate();
    }

    public abstract class State<BehaviourType> : StateBase where BehaviourType : StateMachineBehaviour<BehaviourType>
    {
        protected BehaviourType target;

        protected static T Create<T>(BehaviourType target) where T : State<BehaviourType>
        {
            T state = target.gameObject.AddComponent<T>();
            state.target = target;
            return state;
        }

        protected void SetState(State<BehaviourType> state)
        {
            target.SetState(state);
            Destroy(this);
        }

    }
}
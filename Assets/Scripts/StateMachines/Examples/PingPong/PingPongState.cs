using UnityEngine.UI;

namespace GDT.StateMachines.Examples
{
    public abstract class PingPongState : State<PingPong>
    {
        protected Text textField;

        protected static new T Create<T>(PingPong target) where T : PingPongState
        {
            T state = State<PingPong>.Create<T>(target);
            state.textField = target.GetComponent<Text>();
            return state;
        }
    }
}
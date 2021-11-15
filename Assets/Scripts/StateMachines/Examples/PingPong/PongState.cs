using UnityEngine;

namespace GDT.StateMachines.Examples
{
    public class PongState : PingPongState
    {
        float t = 0;

        public override void StateUpdate()
        {
            t += Time.deltaTime;
            textField.text = $"Pong {string.Format("{0:0.00}", t)}";

            if (t > target.TimeBetweenStates)
            {
                SetState(PingState.Create(target));
            }
        }

        public static PongState Create(PingPong target)
        {
            return PingPongState.Create<PongState>(target);
        }
    }
}
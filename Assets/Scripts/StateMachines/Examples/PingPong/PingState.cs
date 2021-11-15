using UnityEngine;

namespace GDT.StateMachines.Examples
{
    public class PingState : PingPongState
    {
        float t = 0;

        public override void StateUpdate()
        {
            t += Time.deltaTime;
            textField.text = $"Ping {string.Format("{0:0.00}", t)}";

            if (t > target.TimeBetweenStates)
            {
                SetState(PongState.Create(target));
            }
        }

        public static PingState Create(PingPong target)
        {
            return PingPongState.Create<PingState>(target);
        }
    }
}
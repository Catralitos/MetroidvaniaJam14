namespace GDT.StateMachines.Examples
{
    public class PingPong : StateMachineBehaviour<PingPong>
    {
        public float TimeBetweenStates;

        protected override void Start()
        {
            state = PingState.Create(this);
        }
    }
}

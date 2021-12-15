using Audio;
using UnityEngine;
using Audio;

namespace Enemies.Swordfighter
{
    public class SwordfighterIdle : SwordfighterState
    {
        private float _cooldownLeft;
        private AudioManager _audioManager;
        
        public static SwordfighterIdle Create(Swordfighter target)
        {
            SwordfighterIdle state = SwordfighterState.Create<SwordfighterIdle>(target);
            return state;
        }

        public override void StateStart()
        {
            base.StateStart();
            target.spriteRenderer.sprite = target.regular;
            _cooldownLeft = target.holdPositionTime;
            target.rb.velocity = Vector2.zero;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            if (target.CheckForPlayer() && target.InAttackRange())
            {
                SetState(SwordfighterAttack.Create(target));
            }

            _cooldownLeft -= Time.deltaTime;
            if (_cooldownLeft <= 0)
            {
                target.Flip();
                SetState(SwordfighterPatrol.Create(target));
            }
        }
    }
}
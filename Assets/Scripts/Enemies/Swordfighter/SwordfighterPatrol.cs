using System;
using UnityEngine;

namespace Enemies.Swordfighter
{
    public class SwordfighterPatrol : SwordfighterState
    {
        private Vector2 _patrolLeftPoint;
        private Vector2 _patrolRightPoint;

        public static SwordfighterPatrol Create(Swordfighter target)
        {
            SwordfighterPatrol state = SwordfighterState.Create<SwordfighterPatrol>(target);
            return state;
        }

        public override void StateStart()
        {
            base.StateStart();
            _patrolLeftPoint = (Vector2) target.currentPatrolAnchor + Vector2.left * target.horizontalRange;
            _patrolRightPoint = (Vector2) target.currentPatrolAnchor + Vector2.right * target.horizontalRange;
        }

        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();
            if (target.facingRight)
            {
                target.MoveInDirection(Vector2.right);
            }
            else
            {
                target.MoveInDirection(Vector2.left);
            }

            if (target.CheckForPlayer() && target.InAttackRange())
            {
                SetState(SwordfighterAttack.Create(target));
            }

            if (target.colisionDetected && (target.TouchingWall() || !target.TouchingGround() || TimeToChange()))
            {
                SetState(SwordfighterIdle.Create(target));
            }
        }

        private bool TimeToChange()
        {
            if (!target.facingRight && Math.Abs(transform.position.x - _patrolLeftPoint.x) <= 0.05f) return true;
            if (target.facingRight && Math.Abs(transform.position.x - _patrolRightPoint.x) <= 0.05f) return true;
            return false;
        }
    }
}
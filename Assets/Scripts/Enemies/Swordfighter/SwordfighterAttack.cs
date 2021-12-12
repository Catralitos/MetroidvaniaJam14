using Extensions;
using Player;
using UnityEngine;

namespace Enemies.Swordfighter
{
    public class SwordfighterAttack : SwordfighterState
    {
        public static SwordfighterAttack Create(Swordfighter target)
        {
            SwordfighterAttack state = SwordfighterState.Create<SwordfighterAttack>(target);
            return state;
        }

        public override void StateStart()
        {
            base.StateStart();
            //animator.SetTrigger("Attack");
            //audioManager.Play("Attack");
            target.rb.velocity = Vector2.zero;

            // Detect enemies and doors in range of attack
            Collider2D[] hits =
                Physics2D.OverlapCircleAll(target.attackPoint.position, target.attackRange, target.playerMask);

            // Damage enemies and unlock doors
            foreach (Collider2D hit in hits)
            {
                if (target.playerMask.HasLayer(hit.gameObject.layer)) // Hit enemy
                    PlayerEntity.Instance.Health.Hit(target.attackDamage);
            }

            Invoke(nameof(GoToNewState), target.attackCooldown);
        }

        private void GoToNewState()
        {
            SetState(SwordfighterPatrol.Create(target));
        }
    }
}
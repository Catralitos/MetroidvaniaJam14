using Extensions;
using Player;
using UnityEngine;
using Audio;

namespace Enemies.Swordfighter
{
    public class SwordfighterAttack : SwordfighterState
    {
        private AudioManager _audioManager;
        public static SwordfighterAttack Create(Swordfighter target)
        {
            SwordfighterAttack state = SwordfighterState.Create<SwordfighterAttack>(target);
            return state;
        }

        public override void StateStart()
        {
            base.StateStart();
            _audioManager = GetComponent<AudioManager>();
            //animator.SetTrigger("Attack");
            _audioManager.Play("BehemotAttacking");
            target.rb.velocity = Vector2.zero;
            target.spriteRenderer.sprite = target.attacking;
            // Detect enemies and doors in range of attack
            Collider2D[] hits =
                Physics2D.OverlapCircleAll(target.attackPoint.position, target.attackRange, target.playerMask);

            // Damage enemies and unlock doors
            foreach (Collider2D hit in hits)
            {
                if (target.playerMask.HasLayer(hit.gameObject.layer)) // Press enemy
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
using UnityEngine;
using Audio;

namespace Enemies.Gunner
{
    public class GunnerAttack : GunnerState
    {
        private int _shotsFired;
        private float _bulletCooldown;

        private AudioManager _audioManager;
        private Animator _animator;

        public static GunnerAttack Create(Gunner target)
        {
            GunnerAttack state = GunnerState.Create<GunnerAttack>(target);
            return state;
        }

        public override void StateStart()
        {
            _audioManager.Play("GunnerFiring");
            base.StateStart();
            _bulletCooldown = target.fireRate;
            target.rb.velocity = Vector2.zero;
            _animator = GetComponent<Animator>();
           _animator.SetBool("Idle", false);
           _animator.SetBool("Walking", false);
           _animator.SetBool("Shooting", true);
        }

        public override void StateUpdate()
        {
            Vector3 direction = target.facingRight ? Vector3.right : Vector3.left;
        
            _bulletCooldown -= Time.deltaTime;
            if (_bulletCooldown <= 0 && _shotsFired < target.numberOfShots)
            {
                var bullet = Instantiate(target.bulletPrefab, target.bulletSpawn.position, Quaternion.identity)
                    .GetComponent<Bullet>();
                bullet.Init(direction);
                _shotsFired++;
                _bulletCooldown = target.fireRate;
            }

            if (_shotsFired >= target.numberOfShots)
            {
                GoToNewState();
            }
        }

        private void GoToNewState()
        {
            if (target.CheckForPlayer())
            {
                //target.capsuleSprite.color = Color.green;
                SetState(GunnerChase.Create(target));
            }
            else
            {
                //target.capsuleSprite.color = Color.green;
                target.currentPatrolAnchor = transform.position;
                SetState(GunnerPatrol.Create(target));
            }
        }
    }
}
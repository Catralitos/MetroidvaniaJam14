using UnityEngine;

public class GunnerAttack : GunnerState
{
    private int _shotsFired;
    private float _bulletCooldown;

    public static GunnerAttack Create(Gunner target)
    {
        GunnerAttack state = GunnerState.Create<GunnerAttack>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        _bulletCooldown = target.fireRate;
        target.rb.velocity = Vector2.zero;
        //target.capsuleSprite.color = Color.magenta;
        //animator.SetBool("Stopped", true);
        //animator.SetBool("Patrolling", false);
        //animator.SetBool("Chasing", false);
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
        if (CheckForPlayer())
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
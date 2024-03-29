using UnityEngine;

namespace Enemies.Gunner
{
    public class GunnerChase : GunnerState
    {

        private Animator _animator;
        
        // Start is called before the first frame update
        public static GunnerChase Create(Gunner target)
        {
            GunnerChase state = GunnerState.Create<GunnerChase>(target);
            return state;
        }

        public override void StateStart()
        {
            base.StateStart();
            _animator = GetComponent<Animator>();
            _animator.SetBool("Shooting", false);
            _animator.SetBool("Idle", false);
            _animator.SetBool("Walking", true);
        }
    
        // Update is called once per frame
        public override void StateUpdate()
        {
            if (target.CheckIfFlip()) target.Flip();
            //o player escapou
            if (!target.TouchingGround() || target.TouchingWall())
            {
                target.rb.velocity = Vector2.zero;
                if (target.InAttackRange())
                {
                    SetState(GunnerAttack.Create(target));
                }
                else
                {
                    target.currentPatrolAnchor = transform.position;
                    SetState(GunnerPatrol.Create(target));
                }
            }
            else
            {
                //o player está em range de ataque
                if (target.InAttackRange())
                {
                    SetState(GunnerAttack.Create(target));
                }
                else
                {
                    //nao preciso de calculos complicados com o player, só tenho de ir para o patrol point mais perto
                    if (target.facingRight)
                    {
                        target.MoveInDirection(Vector2.right);
                    }
                    else
                    {
                        target.MoveInDirection(Vector2.left);
                    }
                }
            }
        }
    }
}
using Extensions;
using UnityEngine;

namespace Enemies.Crawler
{
    public class CrawlerCrawling : CrawlerState
    {
        private bool _hitFloor = false;
        private float _colliderSize = 0.5f;
        private Vector2 _direction = new Vector2(1, 0);

        public static CrawlerCrawling Create(Crawler target)
        {
            return CrawlerState.Create<CrawlerCrawling>(target);
        }

        public override void StateStart()
        {
            base.StateStart();
            _colliderSize = target.boxCollider.bounds.extents.y;
            //audioManager.Play("Crawling");
        }

        public override void StateUpdate()
        {
            //se o pontapé lhe tirou as contraints (meio hacky mas deve dar)
            /*if (target.rb.constraints == RigidbodyConstraints2D.FreezeRotation)
        {
            SetState(CrawlerFalling.Create(target));
            return;
        }*/


            if (!_hitFloor)
            {
                transform.localPosition +=
                    (Vector3) (_direction.normalized + Vector2.down) * target.moveSpeed * Time.deltaTime;
            }
            else
            {
                Vector3 moveVector;
                if (transform.parent != null)
                {
                    moveVector =
                        transform.parent.InverseTransformVector(target.head.right * target.moveSpeed * Time.deltaTime);
                }
                else
                {
                    moveVector = target.head.right * target.moveSpeed * Time.deltaTime;
                }

                transform.localPosition += moveVector;

                RaycastHit2D down = Physics2D.Raycast(target.head.position, -target.head.up, target.rayDistance,
                    target.groundMask);
                Debug.DrawRay(target.head.position, -target.head.up * target.rayDistance, Color.red);
                RaycastHit2D forward = Physics2D.Raycast(target.head.position, target.head.right, target.rayDistance,
                    target.groundMask);
                Debug.DrawRay(target.head.position, target.head.right * target.rayDistance, Color.blue);

                //escolher direção do down
                if (down.collider == null && forward.collider == null)
                {
                    RaycastHit2D wallHit = Physics2D.Raycast(target.wallFinder.position, -target.wallFinder.right,
                        target.rayDistance, target.groundMask);
                    Debug.DrawRay(target.wallFinder.position, -target.wallFinder.right * target.rayDistance, Color.green);
                    if (wallHit.normal != Vector2.zero)
                    {
                        transform.Rotate(Vector3.forward, -90 * Mathf.Sign(_direction.x));
                        transform.position = wallHit.point + wallHit.normal * _colliderSize;
                    }
                }
                //escolher direção oposta ao down
                else if (down.collider != null && forward.collider != null)
                {
                    transform.Rotate(Vector3.forward, 90 * Mathf.Sign(_direction.x));
                    transform.position = forward.point + forward.normal * _colliderSize;
                }
            }
        }

        protected void OnCollisionEnter2D(Collision2D other)
        {
            if (!_hitFloor && target.groundMask.HasLayer(other.gameObject.layer))
            {
                _hitFloor = true;
                Vector2 closestPoint = other.collider.ClosestPoint(target.rb.position);
                Vector2 rbPos = new Vector2(target.rb.position.x, target.rb.position.y);
                Vector2 normal = (rbPos - closestPoint).normalized;
                bool wall = Vector2.Angle(normal, Vector2.up) > 80;
                bool upsideDown = Vector2.Angle(normal, Vector2.up) > 170;
                //se for parede

                if (upsideDown)
                {
                    if (_direction.x > 0)
                    {
                        transform.Rotate(Vector3.forward, 180);
                    }
                    else
                    {
                        transform.Rotate(Vector3.forward, -180);
                    }
                }

                if (wall && !upsideDown)
                {
                    if (closestPoint.x > rbPos.x)
                    {
                        transform.Rotate(Vector3.forward, 90);
                    }
                    else
                    {
                        transform.Rotate(Vector3.forward, -90);
                    }
                }

                _direction = new Vector2(Mathf.Sign(_direction.x), 0);
                RaycastHit2D hit = Physics2D.Raycast(target.head.position, -transform.up, target.rayDistance,
                    target.groundMask);
                Vector3 newPos = hit.point + hit.normal * _colliderSize;
                transform.position = newPos;
            }
        }
    }
}
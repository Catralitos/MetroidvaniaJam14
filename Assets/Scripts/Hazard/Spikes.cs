using Extensions;
using Player;
using UnityEngine;

namespace Hazard
{
    public class Spikes : MonoBehaviour
    {
        public int contactDamage;

        public float retractSpeed;
    
        public LayerMask playerMask;
        public LayerMask blockMask;
    
        public float transitionTime;
        private float _timer;

        private Vector3 _surfaceTransform;
        private BoxCollider2D _box;
        private bool _retracted;
    
        // Start is called before the first frame update
        private void Start()
        {
            _surfaceTransform = transform.position;
            transform.position -= Vector3.up;
            _box = GetComponent<BoxCollider2D>();
            _box.enabled = false;
            _timer = transitionTime;
            _retracted = true;
        }

        private void FixedUpdate()
        {
            if (_retracted)
            {
                transform.position = Vector3.MoveTowards(transform.position, _surfaceTransform + Vector3.down, retractSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _surfaceTransform, retractSpeed);
            }
        }

        // Update is called once per frame
        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                if (_retracted)
                {
                    if (Physics2D.OverlapCircle(transform.position + Vector3.up, 1, blockMask))
                    {
                        return;
                    }
                    _box.enabled = true;
                    _retracted = false;
                }
                else
                {
                    _box.enabled = false;
                    _retracted = true;
                }

                _timer = transitionTime;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (playerMask.HasLayer(other.gameObject.layer))
            {
                PlayerEntity.Instance.Health.Hit(contactDamage);
            }
        }
    }
}

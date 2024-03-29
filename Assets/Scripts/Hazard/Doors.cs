using Audio;
using UnityEngine;

namespace Hazard
{
    public class Doors : Openable
    {
        public bool startClosed = true;
        private bool closed;
        public float closeSpeed;
        public LayerMask blockMask;
        private Vector3 startPosition;
        private BoxCollider2D _collider;
        private AudioManager _audioManager;

        private void Start()
        {
            startPosition = transform.position;
            _audioManager = GetComponent<AudioManager>();

            _collider = GetComponent<BoxCollider2D>();

            if (!startClosed)
            {
                closed = false;
                transform.position += Vector3.up * 2;
                _collider.enabled = false;
            }
            else
            {
                closed = true;
                _collider.enabled = true;
            }
        }

        private void FixedUpdate()
        {
            if (!closed)
            {
                if (Physics2D.OverlapCircle(startPosition, 1, blockMask))
                {
                    return;
                }

                transform.position =
                    Vector3.MoveTowards(transform.position, startPosition + Vector3.up * 2, closeSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPosition, closeSpeed);
            }
        }

        public override void Open()
        {
            closed = false;
            _collider.enabled = false;
            _audioManager.Play("DoorOpen");
        }

        public override void Close()
        {
            closed = true;
            _collider.enabled = true;
            _audioManager.Play("DoorOpen");
        }
    }
}
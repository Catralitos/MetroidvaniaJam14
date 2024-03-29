using UnityEngine;
using Audio;

namespace Hazard
{
    public class ThirdOfButton : PressableButton
    {
        public TripleButton parent;
    
        public Sprite beforeHitting;
        public Sprite afterHitting;
        public bool pressed;
        private float _timer = 0f;
        private AudioManager _audioManager;

        private SpriteRenderer _sprite;

        private void Start()
        {
            _audioManager = GetComponent<AudioManager>();
            _timer = parent.hitTimeframe;
            _sprite = GetComponent<SpriteRenderer>();
            _sprite.sprite = beforeHitting;
        }

        private void Update()
        {
            if (parent.pressed) return;
            if (pressed)
            {
                _timer -= Time.deltaTime;
            }

            if (_timer <= 0)
            {
                NormalSprite();
            }
        }

        private void HitSprite()
        {
            _sprite.sprite = afterHitting;
            pressed = true;
        }

        private void NormalSprite()
        {
            if (parent.pressed) return;
            _sprite.sprite = beforeHitting;
            pressed = false;
        }

        public override void Press()
        {
            _timer = parent.hitTimeframe;
            HitSprite();
            _audioManager.Play("Button");
        }

        public override void UnPress()
        {
            _timer = 0;
            NormalSprite();
        }
    }
}
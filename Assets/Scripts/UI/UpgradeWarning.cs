using UnityEngine;

namespace UI
{
    public class UpgradeWarning : MonoBehaviour
    {
        public Sprite beforeFinding;
        public Sprite afterFinding;

        private SpriteRenderer _sprite;

        // Start is called before the first frame update
        private void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _sprite.sprite = beforeFinding;
        }

        public void SwitchSprite()
        {
            if (_sprite != null && _sprite.sprite != null && afterFinding != null) _sprite.sprite = afterFinding;
        }
    }
}
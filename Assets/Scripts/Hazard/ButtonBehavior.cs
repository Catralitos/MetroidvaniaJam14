using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public Sprite beforeHitting;
    public Sprite afterHitting;
    public bool pressed;
    public float pressedCooldown = 4f;
    private float _timer = 0f;

    private SpriteRenderer _sprite;

    void Start()
    {
        _timer = pressedCooldown;
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = beforeHitting;
    }

    void Update()
    {
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
        _sprite.sprite = beforeHitting;
        pressed = false;
    }

    public void Hit()
    {
        _timer = pressedCooldown;
        HitSprite();
    }
}

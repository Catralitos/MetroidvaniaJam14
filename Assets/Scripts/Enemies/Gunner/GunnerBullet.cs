using UnityEngine;

public class GunnerBullet : Bullet
{
    private AudioManager _audioManager;
    public int bulletDamage = 60;

    public void Start()
    {
        //_audioManager = GetComponent<AudioManager>();
        //_audioManager.Play("Spawn");
    }

    protected override void Hit(GameObject target)
    {
        base.Hit(target);
        PlayerEntity.Instance.Health.Hit(bulletDamage);
        Destroy(gameObject);
    }
}

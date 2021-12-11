using Enemies.Base;
using Enemies.Crawler;
using Enemies.Gunner;
using Extensions;
using UnityEngine;

public class KickTrigger : MonoBehaviour
{
    public LayerMask hitMaskKick;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        if (hitMaskKick.HasLayer(other.gameObject.layer))
        {
            Kickable kickable = other.gameObject.GetComponent<Kickable>();
            if (kickable != null) kickable.Kick();
            EnemyBase<Crawler> crawler = other.gameObject.GetComponent<EnemyBase<Crawler>>();
            if (crawler != null) crawler.SetStunned();
            EnemyBase<Gunner> gunner = other.gameObject.GetComponent<EnemyBase<Gunner>>();
            if (gunner != null) gunner.SetStunned();
        }
    }
}

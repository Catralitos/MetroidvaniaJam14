using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class FallingRocks : MonoBehaviour
{
    public LayerMask destroyMask;
    public LayerMask hitMask;
    public int damageRock;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (destroyMask.HasLayer(other.gameObject.layer))
        {
            Destroy(gameObject);
        }
        
        if (hitMask.HasLayer(other.gameObject.layer))
        {
            PlayerEntity.Instance.Health.Hit(damageRock);
            Destroy(gameObject);
        }
    }
    protected virtual void Hit(GameObject target) { }



}


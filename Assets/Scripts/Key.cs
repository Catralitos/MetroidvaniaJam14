using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class Key : MonoBehaviour
{
    public LayerMask playerMask;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (playerMask.HasLayer(col.gameObject.layer))
        {
            Player.PlayerEntity.Instance.collectedKey = true;
            Destroy(gameObject);
        }
    }
}

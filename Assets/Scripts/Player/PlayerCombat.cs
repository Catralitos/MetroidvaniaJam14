using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public float shotCooldown;
    public LayerMask hitMaskNormal;
    public LayerMask hitMaskPiercing;
    
    private float _shotTimer;
    
    public void Shoot(bool shoot, Vector2 aimDirection)
    {
        
    }
}

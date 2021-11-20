using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float kickDuration;
    public float shotCooldown;
    public float shotRange;    
    public GameObject meleeGameObject;
    public LayerMask hitMaskNormal;
    public LayerMask hitMaskPiercing;
    //First is standing, second is crouched
    public List<GameObject> armJoints;
    //First is standing, second is crouched
    public List<Transform> shotOrigin;

    private int _pastKickFrames;
    private float _shotTimer;
    
    public void Shoot(bool shoot, Vector2 aimDirection)
    {
        //rodar o braço de acordo com o aimDirection
        //ver se ja passou cooldown para dar tiro
        //ver as bools de unlocks no player instance, procurar pelas camadas apropriadas
        //meter raycasts em cones
        
        //ver se bate em inimigo, se sim, dar disable
        //no piercing até possivelmente, dar mais raycast a partir da posição do inimigo na mesma direção, e assim sucessivamente
        //até esgotar o range, para ser piercing autentico (isto é avançado nao precisas fazer agora)
    }

    public void Kick(bool kick)
    {
        if (kick)
        {
            if (!meleeGameObject.activeSelf)
            {
                meleeGameObject.SetActive(true);
                Invoke(nameof(DisableKick), kickDuration);
            }
        }
        else
        {
            _pastKickFrames++;
        }
    }

    private void DisableKick()
    {
        meleeGameObject.SetActive(false);
    }
}

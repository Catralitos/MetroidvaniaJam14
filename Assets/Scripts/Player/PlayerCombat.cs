using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float kickDuration;
    public float shotCooldown = 0.7f;
    public float shotRange;
    public float normalShotDamage;
    public float boostedShotDamage;
    public GameObject meleeGameObject;
    public LayerMask hitMaskNormal;
    public LayerMask hitMaskPiercing;
    private float _currentShotDamage;
    [HideInInspector] public float currentShotTimer;

    //First is standing, second is crouched
    public List<Transform> armJoints;
    //First is standing, second is crouched
    public List<Transform> shotOrigin;
    
    public LineRenderer lineRenderer;
    private float _shotTimer = 0.0f;

    private int _pastKickFrames;
    
    public void Start()
    {
        _shotTimer = shotCooldown;

    }

    public void Update()
    {
        _shotTimer += Time.deltaTime;
        currentShotTimer += Time.deltaTime;
        if (currentShotTimer < 0)
        {
            _currentShotDamage = normalShotDamage;
            
        }
        else
        {
            _currentShotDamage = boostedShotDamage;
        }
    }

    public void Shoot(bool shoot, Vector2 aimDirection)
    {
        if (PlayerEntity.Instance.isMorphed) return;
        
        int i = PlayerEntity.Instance.isCrouched ? 1 : 0;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        armJoints[i].rotation = Quaternion.Euler(0,0,angle);

        lineRenderer.SetPosition(0, armJoints[i].position);
        lineRenderer.SetPosition(1, armJoints[i].position + (Vector3)aimDirection * shotRange);


        if (!PlayerEntity.Instance.frozeControls && shoot && _shotTimer > shotCooldown)
        {
            LayerMask mask = PlayerEntity.Instance.unlockedPiercingBeam ? hitMaskPiercing : hitMaskNormal;
            RaycastHit2D hitInfo = Physics2D.Raycast(armJoints[i].position, aimDirection, shotRange, mask);
            
            if (hitInfo)
            {
                //Debug.Log(hitInfo.transform.name);  
            }
            
            _shotTimer = 0.0f;
        }
  
            // fazer um if extra para parar de adicionar tempo quando _shotTimer > shootCooldown ?
        

        /*
        public float shotrange
        if (hitInfo) {
        (...)

        _range 


        }
        
        
        
        */
        
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
    
    
    public void IncreaseShotTimer(float timer)
    {

        if (currentShotTimer < 0)
        {
            currentShotTimer = 0;
            
        }
        currentShotTimer += timer;
        
    }
}

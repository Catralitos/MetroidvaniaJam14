using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Enemies.Base;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public float kickDuration;
        public float shotCooldown = 0.7f;
        public float shotRange;
        public int normalShotDamage;
        public int boostedShotDamage;
        public GameObject meleeGameObject;
        public LayerMask hitMaskNormal;
        public LayerMask hitMaskPiercing;
        private int _currentShotDamage;
        [HideInInspector] public float currentShotTimer;

        //First is standing, second is crouched
        public List<Transform> armJoints;

        //First is standing, second is crouched
        public List<Transform> shotOrigin;

        public LineRenderer lineRenderer;
        private float _shotTimer = 0.0f;

        private int _pastKickFrames;
        public LayerMask enemies;
        public LayerMask buttons;
        private RaycastHit2D _hitInfo2;
        private RaycastHit2D _hitInfo3;

        public int damageIncreasePerUpgrade;

        public void Start()
        {
            _shotTimer = shotCooldown;

        }

        public void IncreaseMaxDamage()
        {
            normalShotDamage += damageIncreasePerUpgrade;
        }

        public void Update()
        {
            _shotTimer += Time.deltaTime;
            currentShotTimer += Time.deltaTime;
            if (currentShotTimer < 0)
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
        }

        public void Shoot(bool shoot, Vector2 aimDirection)
        {
            if (PlayerEntity.Instance.isMorphed) return;

            int i = PlayerEntity.Instance.isCrouched ? 1 : 0;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            armJoints[i].rotation = Quaternion.Euler(0, 0, angle);

            lineRenderer.SetPosition(0, armJoints[i].position);
            lineRenderer.SetPosition(1, armJoints[i].position + (Vector3) aimDirection * shotRange);


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

            if (shoot && _shotTimer > shotCooldown)
            {
                LayerMask mask = PlayerEntity.Instance.unlockedPiercingBeam ? hitMaskPiercing : hitMaskNormal;
                RaycastHit2D hitInfo = Physics2D.Raycast(armJoints[i].position, aimDirection, shotRange, mask);

                if (PlayerEntity.Instance.unlockedTripleBeam)
                {
                    Vector2 aimDirection2 = rotate(aimDirection, 45f);
                    Vector2 aimDirection3 = rotate(aimDirection, -45f);

                    _hitInfo2 = Physics2D.Raycast(armJoints[i].position, aimDirection2, shotRange, mask);
                    _hitInfo3 = Physics2D.Raycast(armJoints[i].position, aimDirection3, shotRange, mask);
                }

                if (hitInfo)
                {
                    if (enemies.HasLayer(hitInfo.collider.gameObject.layer))
                    {
                        //hitInfo.collider.gameObject.GetComponent<EnemyBase>.Hit(20);
                        hitInfo.collider.gameObject.GetComponent<EnemyBase>().Hit(_currentShotDamage);
                    }

                    if (buttons.HasLayer(hitInfo.collider.gameObject.layer))
                    {
                        hitInfo.collider.gameObject.GetComponent<EnemyBase>().Hit(_currentShotDamage);
                    }

                    Debug.Log(hitInfo.transform.name);
                }

                if (PlayerEntity.Instance.unlockedTripleBeam)
                {
                    if (_hitInfo2)
                    {
                        if (enemies.HasLayer(hitInfo.collider.gameObject.layer))
                        {
                            hitInfo.collider.gameObject.GetComponent<EnemyBase>().Hit(_currentShotDamage);
                        }

                        if (buttons.HasLayer(hitInfo.collider.gameObject.layer))
                        {
                            hitInfo.collider.gameObject.GetComponent<EnemyBase>().Hit(_currentShotDamage);
                        }

                        Debug.Log(hitInfo.transform.name);
                    }

                    if (_hitInfo3)
                    {
                        if (enemies.HasLayer(hitInfo.collider.gameObject.layer))
                        {
                            hitInfo.collider.gameObject.GetComponent<EnemyBase>().Hit(_currentShotDamage);
                        }

                        if (buttons.HasLayer(hitInfo.collider.gameObject.layer))
                        {
                            hitInfo.collider.gameObject.GetComponent<EnemyBase>().Hit(_currentShotDamage);
                            Debug.Log(hitInfo.transform.name);
                        }
                    }

                    _shotTimer = 0.0f;
                }

                //rodar o braço de acordo com o aimDirection
                //ver se ja passou cooldown para dar tiro
                //ver as bools de unlocks no player instance, procurar pelas camadas apropriadas
                //meter raycasts em cones
                //ver se bate em inimigo, se sim, dar disable
                //no piercing até possivelmente, dar mais raycast a partir da posição do inimigo na mesma direção, e assim sucessivamente
                //até esgotar o range, para ser piercing autentico (isto é avançado nao precisas fazer agora)
            }
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


        private Vector2 rotate(Vector2 vector, float degrees)
        {
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);

            float x = vector.x;
            float y = vector.y;
            float newX = (cos * x) - (sin * y);
            float newY = (sin * x) + (cos * y);

            return new Vector2(newX, newY);
        }



        
            /* private void pierceShot(Vector2 posInicial, Vector2 direcao, float rangeSum, int layerMask)   
        {
                if (rangeSum < shotRange && rangeSum > 0)
                {
                RaycastHit2D hitInfoExtra = Physics2D.Raycast(posInicial, direcao, rangeSum, layerMask);
    
                if (hitInfoExtra)
                {
                    if (enemies.HasLayer(hitInfoExtra.collider.gameObject.layer))
                    {
                            hitInfoExtra.collider.gameObject.GetComponent<EnemyBase>().Hit(_currentShotDamage);
                    }
    
                    if (buttons.HasLayer(hitInfoExtra.collider.gameObject.layer))
                    {
                            hitInfoExtra.collider.gameObject.GetComponent<EnemyBase>().Hit(_currentShotDamage);
                    }
    
                    Debug.Log(hitInfoExtra.transform.name);
                    
                    rangeSum += hitInfoExtra.magnitude;
                    
                    return pierceShot(posInicial + direcao.normalized * hitInfoExtra.magnitude, direcao, rangeSum - hitInfoExtra.magnitude, layerMask);
                  } 
            } 
        } */
           
    }
}

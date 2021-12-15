using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Enemies.Base;
using Hazard;
using Audio;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public float kickDuration;

        public float shotCooldown = 0.7f;
        private float _shotTimer = 0.0f;

        public float shotRange;
        public int normalShotDamage;
        public int boostedShotDamage;
        public GameObject meleeGameObject;
        public LayerMask hitMaskNormal;
        public LayerMask hitMaskPiercing;
        public float tripleShotAngle = 30f;

        private int _currentShotDamage;
        [HideInInspector] public float currentShotTimer;

        //First is standing, second is crouched
        public List<Transform> armJoints;

        //First is standing, second is crouched
        public List<Transform> shotOrigin;

        public LineRenderer lineRenderer;
        public LineRenderer lineRendererUp;
        public LineRenderer lineRendererDown;

        private int _pastKickFrames;
        public LayerMask enemies;
        public LayerMask buttons;

        public int damageIncreasePerUpgrade;

        private Material _material;
        private AudioManager _audioManager;
        public void Start()
        {
            
            _shotTimer = shotCooldown;
            _audioManager = GetComponent<AudioManager>();
        }

        public void IncreaseMaxDamage()
        {
            normalShotDamage += damageIncreasePerUpgrade;
        }

        public void Update()
        {
            _shotTimer += Time.deltaTime;
            currentShotTimer -= Time.deltaTime;
            if (currentShotTimer < 0)
            {
                _currentShotDamage = normalShotDamage;
            }
            else
            {
                _currentShotDamage = boostedShotDamage;
            }
            
            if (_shotTimer < shotCooldown)
            {
                lineRenderer.startColor = Color.yellow;
                lineRenderer.endColor = Color.yellow;
                lineRendererUp.startColor = Color.yellow;
                lineRendererUp.endColor = Color.yellow;
                lineRendererDown.startColor = Color.yellow;
                lineRendererDown.endColor = Color.yellow;
            }
            else
            {
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                lineRendererUp.startColor = Color.red;
                lineRendererUp.endColor = Color.red;
                lineRendererDown.startColor = Color.red;
                lineRendererDown.endColor = Color.red;
            }
        }

        public void Shoot(bool shoot, Vector2 aimDirection)
        {
            if (PlayerEntity.Instance.isMorphed || PlayerEntity.Instance.Movement.isClimbing)
            {
                lineRenderer.enabled = false;
                lineRendererUp.enabled = false;
                lineRendererDown.enabled = false;
                return;
            }
            else
            {
                lineRenderer.enabled = true;
                lineRendererUp.enabled = true;
                lineRendererDown.enabled = true;
            }
            
            int i = PlayerEntity.Instance.isCrouched ? 1 : 0;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            armJoints[i].rotation = Quaternion.Euler(0, 0, angle);

            RaycastHit2D hitInfo;
            RaycastHit2D hitInfo2;
            RaycastHit2D hitInfo3;

            Vector2 aimDirection2 = rotate(aimDirection, tripleShotAngle);
            Vector2 aimDirection3 = rotate(aimDirection, -tripleShotAngle);

            lineRenderer.SetPosition(0, shotOrigin[i].position);
            lineRenderer.SetPosition(1, shotOrigin[i].position + (Vector3) aimDirection * shotRange);

            if (PlayerEntity.Instance.unlockedTripleBeam)
            {
                lineRendererUp.SetPosition(0, shotOrigin[i].position);
                lineRendererUp.SetPosition(1, shotOrigin[i].position + (Vector3) aimDirection2 * shotRange);
                lineRendererDown.SetPosition(0, shotOrigin[i].position);
                lineRendererDown.SetPosition(1, shotOrigin[i].position + (Vector3) aimDirection3 * shotRange);
            }

            if (!shoot) return;
            if (_shotTimer < shotCooldown) return;
            if (PlayerEntity.Instance.isMorphed) return;

            if (!PlayerEntity.Instance.frozeControls)
            {
                _shotTimer = 0.0f;

                LayerMask mask = PlayerEntity.Instance.unlockedPiercingBeam ? hitMaskPiercing : hitMaskNormal;
                hitInfo = Physics2D.Raycast(shotOrigin[i].position, aimDirection, shotRange, mask);
                _audioManager.Play("Shooting_weak");

                PlayerEntity.Instance.animators[i + 2].SetTrigger("Shoot");

                if (hitInfo)
                {
                    if (enemies.HasLayer(hitInfo.collider.gameObject.layer))
                    {
                        hitInfo.collider.gameObject.GetComponent<EnemyBase>().Hit(_currentShotDamage);
                    }

                    if (buttons.HasLayer(hitInfo.collider.gameObject.layer))
                    {
                        hitInfo.collider.gameObject.GetComponent<PressableButton>().Press();
                    }

                    // Debug.Log("Middle: " + hitInfo.transform.name);
                }

                if (PlayerEntity.Instance.unlockedTripleBeam)
                {
                    hitInfo2 = Physics2D.Raycast(shotOrigin[i].position, aimDirection2, shotRange, mask);
                    hitInfo3 = Physics2D.Raycast(shotOrigin[i].position, aimDirection3, shotRange, mask);
                    if (hitInfo2)
                    {
                        if (enemies.HasLayer(hitInfo2.collider.gameObject.layer))
                        {
                            hitInfo2.collider.gameObject.GetComponent<EnemyBase>().Hit(_currentShotDamage);
                        }

                        if (buttons.HasLayer(hitInfo2.collider.gameObject.layer))
                        {
                            hitInfo2.collider.gameObject.GetComponent<PressableButton>().Press();
                        }

                        //Debug.Log("Top: " + hitInfo2.transform.name);
                    }

                    if (hitInfo3)
                    {
                        if (enemies.HasLayer(hitInfo3.collider.gameObject.layer))
                        {
                            hitInfo3.collider.gameObject.GetComponent<EnemyBase>().Hit(_currentShotDamage);
                        }

                        if (buttons.HasLayer(hitInfo.collider.gameObject.layer))
                        {
                            hitInfo3.collider.gameObject.GetComponent<PressableButton>().Press();
                        }
                        //Debug.Log("Bottom: " + hitInfo3.transform.name);
                    }
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
                    PlayerEntity.Instance.animators[0].SetBool("Punching", true);
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
            PlayerEntity.Instance.animators[0].SetBool("Punching", false);
            meleeGameObject.SetActive(false);
        }


        public void IncreaseShotTimer(float time)
        {
            if (currentShotTimer < 0)
            {
                currentShotTimer = 0;
            }

            currentShotTimer = Mathf.Clamp(currentShotTimer + time, 0, PlayerEntity.Instance.maxDamageBuffTime);
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
                        hitInfoExtra.collider.gameObject.GetComponent<EnemyBase>().Press(_currentShotDamage);
                }

                if (buttons.HasLayer(hitInfoExtra.collider.gameObject.layer))
                {
                        hitInfoExtra.collider.gameObject.GetComponent<EnemyBase>().Press(_currentShotDamage);
                }

                Debug.Log(hitInfoExtra.transform.name);
                
                rangeSum += hitInfoExtra.magnitude;
                
                return pierceShot(posInicial + direcao.normalized * hitInfoExtra.magnitude, direcao, rangeSum - hitInfoExtra.magnitude, layerMask);
              } 
        } 
    } */
    }
}
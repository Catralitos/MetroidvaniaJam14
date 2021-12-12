using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerEntity : MonoBehaviour
    {
        [HideInInspector]public static PlayerEntity Instance { get; private set; }    
        [HideInInspector]public PlayerHealth Health { get; private set; }
        [HideInInspector]public PlayerControls Controller { get; private set; }
        [HideInInspector]public PlayerMovement Movement { get; private set; }
        [HideInInspector]public PlayerCombat Combat { get; private set; }
        [HideInInspector]public PlayerUI UI { get; private set; }
    
        //1 - standing
        //2 - crouching
        //3 - morphed up
        [Header("Positions")]public List<GameObject> states;

        [Header("Global Bools")]public bool displayingTooltip;
        public bool frozeControls;
        public bool collectedKey;
        public bool destroyedDoor;

        [Header("Current State")]public bool facingRight;
        public bool isCrouched;
        public bool isMorphed;
        public bool isUnderwater;
        public bool dying;
        
        [Header("Unlocks")]public bool unlockedDash;
        public bool unlockedDoubleJump;
        public bool unlockedGravitySuit;
        public bool unlockedMorphBall;
        public bool unlockedTripleBeam;
        public bool unlockedPiercingBeam;
        public bool[] combatRoomsBeaten;
        public bool[] healthUpgradesCollected;
        public bool[] damageUpgradesCollected;
        public bool[] threeButtonDoorsOpened;
        
        [Header("Normal/Underwater Movement")]public float defaultDrag;
        public float defaultGravity;
        public float defaultMass;
        public float underwaterDrag;
        public float underwaterGravity;
        public float underwaterMass;

        [Header("Timer Caps")] public float maxDamageBuffTime;
        public float maxSpeedBuffTime;
        public float maxJumpBuffTime;
        
        private Rigidbody2D _rb;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("Multiple players present in scene! Destroying...");
                Destroy(gameObject);
            }
            Movement = GetComponent<PlayerMovement>();
            Health = GetComponent<PlayerHealth>();
            Controller = GetComponent<PlayerControls>();
            Combat = GetComponent<PlayerCombat>();
            UI = GetComponent<PlayerUI>();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (isUnderwater && !unlockedGravitySuit)
            {
                _rb.drag = underwaterDrag;
                _rb.gravityScale = underwaterGravity;
                _rb.mass = underwaterMass;
            }
            else
            {
                _rb.drag = defaultDrag;
                _rb.gravityScale = defaultGravity;
                _rb.mass = defaultMass;
            }


            if (!isCrouched && !isMorphed)
            {
                states[1].SetActive(false);
                states[2].SetActive(false);
                states[0].SetActive(true);
            } else if (isCrouched)
            {
                states[0].SetActive(false);
                states[2].SetActive(false);
                states[1].SetActive(true);
            } else if (isMorphed)
            {
                states[0].SetActive(false);
                states[1].SetActive(false);
                states[2].SetActive(true);
            }
        }

        public void DisableAllCollisions()
        {
            Collider2D[] collidersObj = gameObject.GetComponentsInChildren<Collider2D>();
            for (var index = 0; index < collidersObj.Length; index++)
            {
                var colliderItem = collidersObj[index];
                colliderItem.enabled = false;
            }
        }
    }
}
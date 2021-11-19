using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [HideInInspector]public static PlayerEntity Instance { get; private set; }    
    [HideInInspector]public PlayerHealth Health { get; private set; }
    [HideInInspector]public PlayerControls Controller { get; private set; }
    [HideInInspector]public PlayerMovement Movement { get; private set; }

    [HideInInspector]public PlayerCombat Combat { get; private set; }

    
    //1 - standing
    //2 - crouching
    //3 - morphed up
    public List<GameObject> states;
    
    public bool frozeControls;

    public bool isCrouched;
    public bool isMorphed;

    public bool unlockedDash;
    public bool unlockedDoubleJump;
    public bool unlockedMorphBall;
    public bool unlockedTripleBeam;
    public bool unlockedPiercingBeam;
  
    
    
    public SpriteRenderer testCylinder;
    
    void Awake()
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
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Update()
    {
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
}
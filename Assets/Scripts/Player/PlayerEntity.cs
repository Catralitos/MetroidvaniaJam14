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

    //vou meter aqui os colliders porque vou precisar de fazer um trigger e não é o player em si com colliders
    public List<GameObject> colliders;

    public bool frozeControls;
    
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
}
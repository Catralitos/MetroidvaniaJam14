using System;
using Extensions;
using Player;
using UnityEngine;
using Audio;

public class DoorWithLock : MonoBehaviour
{
    public LayerMask playerMask;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = GetComponent<AudioManager>();

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
       if (playerMask.HasLayer(col.gameObject.layer) && PlayerEntity.Instance.collectedKey)
       {
           PlayerEntity.Instance.destroyedDoor = true;
           Destroy(gameObject);
           _audioManager.Play("DoorOpen");
       }
    }
}

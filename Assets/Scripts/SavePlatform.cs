using System;
using Extensions;
using GameManagement;
using Player;
using UnityEngine;

public class SavePlatform : MonoBehaviour
{
    
    public LayerMask playerMask;
    private bool _canSave;

    private void Start()
    {
        Invoke(nameof(CanSave), 3f);
    }

    private void CanSave()
    {
        _canSave = true;
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (_canSave && playerMask.HasLayer(other.gameObject.layer) && !LevelManager.Instance.countingDown)
        {
            SaveSystem.SavePlayer(PlayerEntity.Instance);
            PlayerEntity.Instance.UI.DisplaySaveTooltip();
        }
    }
}

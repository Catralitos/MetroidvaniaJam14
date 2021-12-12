using Extensions;
using GameManagement;
using Player;
using UnityEngine;

public class SavePlatform : MonoBehaviour
{
    public LayerMask playerMask;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (playerMask.HasLayer(other.gameObject.layer) && !LevelManager.Instance.countingDown)
        {
            SaveSystem.SavePlayer(PlayerEntity.Instance);
        }
    }
}

using Extensions;
using UnityEngine;

public class SavePlatform : MonoBehaviour
{
    public LayerMask playerMask;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (playerMask.HasLayer(other.gameObject.layer))
        {
            SaveSystem.SavePlayer(PlayerEntity.Instance);
        }
    }
}

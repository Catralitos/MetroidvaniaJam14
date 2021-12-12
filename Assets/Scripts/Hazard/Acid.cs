using Extensions;
using Player;
using UnityEngine;

public class Acid : MonoBehaviour
{
    public LayerMask playerMask;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerMask.HasLayer(other.gameObject.layer))
        {
            PlayerEntity.Instance.isUnderwater = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerMask.HasLayer(other.gameObject.layer))
        {
            PlayerEntity.Instance.isUnderwater = false;
        }
    }
}

using Extensions;
using Player;
using UnityEngine;


public class DoorWithLock : MonoBehaviour
{
    public LayerMask playerMask;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
       if (playerMask.HasLayer(col.gameObject.layer) && PlayerEntity.Instance.collectedKey)
       {
           PlayerEntity.Instance.destroyedDoor = true;
           Destroy(gameObject);
       }
    }
}

using Extensions;
using Player;
using UnityEngine;

public class DisplayTooltip : MonoBehaviour
{

    public string tooltipFunction;

    public LayerMask playerMask;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerMask.HasLayer(other.gameObject.layer))
        {
            PlayerEntity.Instance.UI.Invoke(tooltipFunction, 0f);
            Destroy(gameObject);
        }
    }
}

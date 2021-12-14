using Extensions;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

public class DisplayTooltip : MonoBehaviour
{

    [FormerlySerializedAs("tooltipFunction")] public string tooltipText;

    public LayerMask playerMask;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerMask.HasLayer(other.gameObject.layer))
        {
            PlayerEntity.Instance.UI.DisplayTooltip(tooltipText);
            Destroy(gameObject);
        }
    }
}

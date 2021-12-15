using Extensions;
using GameManagement;
using UnityEngine;

public class GameEnder : MonoBehaviour
{
    public LayerMask playerMask;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerMask.HasLayer(other.gameObject.layer))
        {
            LevelManager.Instance.StopFinalCountown();
        }
    }
}

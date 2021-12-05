using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisplayMinimap : MonoBehaviour
{
    public LayerMask playerMask;
    private TilemapRenderer _renderer;

    private UpgradeWarning[] _childUpgradeWarnings;
    
    // Start is called before the first frame update
    private void Start()
    {
        _renderer = GetComponent<TilemapRenderer>();
        _renderer.enabled = false;
        _childUpgradeWarnings = GetComponentsInChildren<UpgradeWarning>();
        foreach (var child in _childUpgradeWarnings)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerMask.HasLayer(other.gameObject.layer))
        {
            _renderer.enabled = true;
            foreach (var child in _childUpgradeWarnings)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}

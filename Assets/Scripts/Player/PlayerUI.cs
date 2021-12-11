using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    
    [Header("Tooltips")] public GameObject movementTooltip;
    public GameObject saveTooltip;


    [HideInInspector] public bool canCancelTooltip;
    
    public void DisplaySaveTooltip()
    {
        PlayerEntity.Instance.frozeControls = true;
        PlayerEntity.Instance.displayingTooltip = true;
        saveTooltip.SetActive(true);
        Invoke(nameof(SetCancel), 5f);
    }

    public void CloseTooltip()
    {
        PlayerEntity.Instance.frozeControls = false;
        PlayerEntity.Instance.displayingTooltip = false;
        movementTooltip.SetActive(false);
        saveTooltip.SetActive(false);
        canCancelTooltip = false;
    }

    private void SetCancel()
    {
        canCancelTooltip = true;
    }
    
    
}

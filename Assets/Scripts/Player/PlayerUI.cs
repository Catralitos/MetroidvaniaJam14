using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerUI : MonoBehaviour
    {
        [HideInInspector] public int healthPipsCollected;

        [Header("Health")] public Image healthBar;
        public List<Image> healthPips;

        [Header("Buffs")] public Image damageBuff;
        public Image jumpBuff;
        public Image speedBuff;

        [Header("Tooltips")] public GameObject movementTooltip;
        public GameObject saveTooltip;

        [HideInInspector] public bool canCancelTooltip;

        private void Update()
        {
            int numberOfColoredPips = PlayerEntity.Instance.Health.currentHealth <=
                                      PlayerEntity.Instance.Health.healthPerMaxIncrement
                ? 0
                : Mathf.RoundToInt(Mathf.Floor(1.0f * PlayerEntity.Instance.Health.currentHealth /
                                               PlayerEntity.Instance.Health.healthPerMaxIncrement));

            int mod = Mathf.RoundToInt(PlayerEntity.Instance.Health.currentHealth %
                                       PlayerEntity.Instance.Health.healthPerMaxIncrement);

            healthBar.fillAmount = mod == 0 ? 1 : mod / 100f;

            for (int i = 0; i < healthPipsCollected; i++)
            {
                healthPips[i].gameObject.SetActive(true);
                healthPips[i].color = i <= numberOfColoredPips ? Color.red : Color.gray;
            }

            for (int i = healthPipsCollected; i < healthPips.Count; i++)
            {
                healthPips[i].gameObject.SetActive(false);
            }

            damageBuff.fillAmount = PlayerEntity.Instance.Combat.currentShotTimer <= 0
                ? 0f
                : PlayerEntity.Instance.Combat.currentShotTimer / PlayerEntity.Instance.maxDamageBuffTime;
            jumpBuff.fillAmount = PlayerEntity.Instance.Movement.currentJumpTimer <= 0
                ? 0f
                : PlayerEntity.Instance.Movement.currentJumpTimer / PlayerEntity.Instance.maxJumpBuffTime;
            speedBuff.fillAmount = PlayerEntity.Instance.Movement.currentMoveTimer <= 0
                ? 0f
                : PlayerEntity.Instance.Movement.currentMoveTimer / PlayerEntity.Instance.maxSpeedBuffTime;
        }

        
        //para fazer mais tooltips só copiar este método e manter tudo excepto a terceira linha
        //em vez de saveTooltip, meter a tooltip certa;
        public void DisplaySaveTooltip()
        {
            PlayerEntity.Instance.frozeControls = true;
            PlayerEntity.Instance.displayingTooltip = true;
            saveTooltip.SetActive(true);
            Invoke(nameof(SetCancel), 3f);
        }

        //ADICIONAR AQUI TOOLTIPS NOVAS
        public void CloseTooltip()
        {
            PlayerEntity.Instance.frozeControls = false;
            PlayerEntity.Instance.displayingTooltip = false;
            if (movementTooltip != null) movementTooltip.SetActive(false);
            if (saveTooltip != null) saveTooltip.SetActive(false);
            canCancelTooltip = false;
        }

        private void SetCancel()
        {
            canCancelTooltip = true;
        }
    }
}
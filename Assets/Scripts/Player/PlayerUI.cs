using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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

        [Header("Tooltips")] [FormerlySerializedAs("saveTooltip")] public TextMeshProUGUI tooltip;

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

        
        public void DisplayTooltip(string text)
        {
            PlayerEntity.Instance.frozeControls = true;
            PlayerEntity.Instance.displayingTooltip = true;
            tooltip.text = text;
            tooltip.gameObject.SetActive(true);
            Invoke(nameof(SetCancel), 3f);
        }

        public void CloseTooltip()
        {
            PlayerEntity.Instance.frozeControls = false;
            PlayerEntity.Instance.displayingTooltip = false;
            tooltip.gameObject.SetActive(false);
            canCancelTooltip = false;
        }

        private void SetCancel()
        {
            canCancelTooltip = true;
        }
    }
}
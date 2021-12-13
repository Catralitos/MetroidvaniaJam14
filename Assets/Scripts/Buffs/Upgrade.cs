using Extensions;
using UI;
using UnityEngine;

namespace Buffs
{
    public abstract class Upgrade : MonoBehaviour
    {
        public LayerMask playerMask;
        public UpgradeWarning upgradeWarning;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (playerMask.HasLayer(other.gameObject.layer))
            {
                if(upgradeWarning != null) upgradeWarning.SwitchSprite();
                SetUpgrade();
                Destroy(gameObject);
            }
        }

        protected virtual void SetUpgrade()
        {
            //do nothing, cada classe filho vai desbloquear a bool certa
        }
    }
}

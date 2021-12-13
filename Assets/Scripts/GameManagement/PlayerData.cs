using Player;

namespace GameManagement
{
    [System.Serializable]
    public class PlayerData 
    {
        public int healthPipsCollected;
        public int maxHealth;
        public int currentHealth;
        public float elapsedTime;
        public bool[] combatRoomsBeaten;
        public bool[] healthUpgradesCollected;
        public bool[] damageUpgradesCollected;
        public bool[] unlockedPowers;
        public float[] buffTimers;
        public float[] playerPosition;
        public bool collectedKey;
        public bool destroyedDoor;
        public bool[] threeButtonDoorsOpened;
        
        public PlayerData(PlayerEntity playerEntity)
        {
            currentHealth = playerEntity.Health.currentHealth;
            maxHealth = playerEntity.Health.maxHealth;
            healthPipsCollected = playerEntity.UI.healthPipsCollected;
            unlockedPowers = new bool[6];
            unlockedPowers[0] = playerEntity.unlockedDash;
            unlockedPowers[1] = playerEntity.unlockedDoubleJump;
            unlockedPowers[2] = playerEntity.unlockedGravitySuit;
            unlockedPowers[3] = playerEntity.unlockedMorphBall;
            unlockedPowers[4] = playerEntity.unlockedPiercingBeam;
            unlockedPowers[5] = playerEntity.unlockedTripleBeam;
            playerPosition = new float[3];
            playerPosition[0] = playerEntity.gameObject.transform.position.x;
            playerPosition[1] = playerEntity.gameObject.transform.position.y;
            playerPosition[2] = playerEntity.gameObject.transform.position.z;
            buffTimers = new float[3];
            buffTimers[0] = playerEntity.Movement.currentJumpTimer;
            buffTimers[1] = playerEntity.Movement.currentMoveTimer;
            buffTimers[2] = playerEntity.Combat.currentShotTimer;
            combatRoomsBeaten = new bool[playerEntity.combatRoomsBeaten.Length];
            damageUpgradesCollected = new bool[playerEntity.damageUpgradesCollected.Length];
            healthUpgradesCollected = new bool[playerEntity.healthUpgradesCollected.Length];
            threeButtonDoorsOpened = new bool[playerEntity.threeButtonDoorsOpened.Length];
            for (int i = 0; i < combatRoomsBeaten.Length; i++)
            {
                combatRoomsBeaten[i] = playerEntity.combatRoomsBeaten[i];
            }
            for (int i = 0; i < damageUpgradesCollected.Length; i++)
            {
                damageUpgradesCollected[i] = playerEntity.damageUpgradesCollected[i];
            }
            for (int i = 0; i < healthUpgradesCollected.Length; i++)
            {
                healthUpgradesCollected[i] = playerEntity.healthUpgradesCollected[i];
            }
            
            for (int i = 0; i < threeButtonDoorsOpened.Length; i++)
            {
                threeButtonDoorsOpened[i] = playerEntity.threeButtonDoorsOpened[i];
            }

            collectedKey = playerEntity.collectedKey;
            destroyedDoor = playerEntity.destroyedDoor;
        }
    }
}

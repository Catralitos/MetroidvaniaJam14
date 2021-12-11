[System.Serializable]
public class PlayerData 
{
    public int maxHealth;
    public int currentHealth;
    public float elapsedTime;
    public bool[] healthUpgradesCollected;
    public bool[] damageUpgradesCollected;
    public bool[] unlockedPowers;
    public float[] buffTimers;
    public float[] playerPosition;

    public PlayerData(PlayerEntity playerEntity)
    {
        currentHealth = playerEntity.Health.currentHealth;
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
        damageUpgradesCollected = new bool[playerEntity.damageUpgradesCollected.Length];
        healthUpgradesCollected = new bool[playerEntity.healthUpgradesCollected.Length];
        for (int i = 0; i < damageUpgradesCollected.Length; i++)
        {
            damageUpgradesCollected[i] = playerEntity.damageUpgradesCollected[i];
        }
        for (int i = 0; i < healthUpgradesCollected.Length; i++)
        {
            healthUpgradesCollected[i] = playerEntity.healthUpgradesCollected[i];
        }
    }
}

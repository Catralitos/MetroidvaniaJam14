using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [HideInInspector] public static LevelManager Instance { get; private set; }

    [HideInInspector] public bool countingDown;
    public float finalCountdownTime;
    private float _finalCountdown;

    public EnemySpawner[] combatRooms;
    
    public Upgrade[] healthUpgrades;
    public Upgrade[] damageUpgrades;

    public Upgrade dashUpgrade;
    public Upgrade doubleJumpUpgrade;
    public Upgrade gravitySuitUpgrade;
    public Upgrade morphBallUpgrade;
    public Upgrade piercingBeamUpgrade;
    public Upgrade tripleBeamUpgrade;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple level managers present in scene! Destroying...");
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        PlayerData savedData = SaveSystem.LoadPlayer();
        if (savedData != null)
        {
            PlayerEntity.Instance.Health.currentHealth = savedData.currentHealth;
            PlayerEntity.Instance.unlockedDash = savedData.unlockedPowers[0];
            PlayerEntity.Instance.unlockedDoubleJump = savedData.unlockedPowers[1];
            PlayerEntity.Instance.unlockedGravitySuit = savedData.unlockedPowers[2];
            PlayerEntity.Instance.unlockedMorphBall = savedData.unlockedPowers[3];
            PlayerEntity.Instance.unlockedPiercingBeam = savedData.unlockedPowers[4];
            PlayerEntity.Instance.unlockedTripleBeam = savedData.unlockedPowers[5];
            PlayerEntity.Instance.gameObject.transform.position = new Vector3(savedData.playerPosition[0],
                savedData.playerPosition[1], savedData.playerPosition[2]);
            PlayerEntity.Instance.Movement.currentJumpTimer = savedData.buffTimers[0];
            PlayerEntity.Instance.Movement.currentMoveTimer = savedData.buffTimers[1];
            PlayerEntity.Instance.Combat.currentShotTimer = savedData.buffTimers[2];
            
            for (int i = 0; i < savedData.combatRoomsBeaten.Length; i++)
            {
                PlayerEntity.Instance.combatRoomsBeaten[i] = savedData.combatRoomsBeaten[i];
            }
            
            for (int i = 0; i < savedData.damageUpgradesCollected.Length; i++)
            {
                PlayerEntity.Instance.damageUpgradesCollected[i] = savedData.damageUpgradesCollected[i];
            }

            for (int i = 0; i < savedData.healthUpgradesCollected.Length; i++)
            {
                PlayerEntity.Instance.healthUpgradesCollected[i] = savedData.healthUpgradesCollected[i];
            }
        }

        if (PlayerEntity.Instance.unlockedDash)
        {
            dashUpgrade.upgradeWarning.SwitchSprite();
            Destroy(dashUpgrade.gameObject);
        }

        if (PlayerEntity.Instance.unlockedDoubleJump)
        {
            doubleJumpUpgrade.upgradeWarning.SwitchSprite();
            Destroy(doubleJumpUpgrade.gameObject);
        }

        if (PlayerEntity.Instance.unlockedGravitySuit)
        {
            gravitySuitUpgrade.upgradeWarning.SwitchSprite();
            Destroy(gravitySuitUpgrade.gameObject);
        }

        if (PlayerEntity.Instance.unlockedMorphBall)
        {
            morphBallUpgrade.upgradeWarning.SwitchSprite();
            Destroy(gravitySuitUpgrade.gameObject);
        }

        if (PlayerEntity.Instance.unlockedPiercingBeam)
        {
            piercingBeamUpgrade.upgradeWarning.SwitchSprite();
            Destroy(piercingBeamUpgrade.gameObject);
        }

        if (PlayerEntity.Instance.unlockedTripleBeam)
        {
            tripleBeamUpgrade.upgradeWarning.SwitchSprite();
            Destroy(tripleBeamUpgrade.gameObject);
        }

        for (int i = 0; i < combatRooms.Length; i++)
        {
            if (combatRooms[i] != null && combatRooms[i])
            {
                Destroy(combatRooms[i].gameObject);
            }
        }
        
        for (int i = 0; i < damageUpgrades.Length; i++)
        {
            if (damageUpgrades[i] != null && damageUpgrades[i])
            {
                damageUpgrades[i].upgradeWarning.SwitchSprite();
                Destroy(damageUpgrades[i].gameObject);
            }
        }

        for (int i = 0; i < healthUpgrades.Length; i++)
        {
            if (healthUpgrades[i] != null && healthUpgrades[i])
            {
                healthUpgrades[i].upgradeWarning.SwitchSprite();
                Destroy(healthUpgrades[i].gameObject);
            }
        }

        _finalCountdown = finalCountdownTime;
        GameManager.Instance.StartCountingTime();
    }

    public void Update()
    {
        if (countingDown)
        {
            _finalCountdown -= Time.deltaTime;
            if (_finalCountdown <= 0)
            {
                PlayerEntity.Instance.Health.Die();
            }
        }
    }

    public void StartFinalCountdown()
    {
        countingDown = true;
    }

    public void StopFinalCountown()
    {
        PlayerEntity.Instance.frozeControls = true;
        countingDown = false;
        GameManager.Instance.StopCountingTime();
        Invoke(nameof(LoadCredits), 3f);
    }

    private void LoadCredits()
    {
        GameManager.Instance.LoadNextScene();
    }
}
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject playerProjectile;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private int numSimultaneousProjectiles = 1;
    [SerializeField] private float shootingCooldown = 0.16f;

    private int currentHealth;
    private int numCurrentProjectiles;
    private float currentCooldown;

    // Behaviour
    void Start()
    {
        currentHealth = startingHealth;
        currentCooldown = shootingCooldown;
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;

        if (Input.GetButton("Fire"))
        {
            if (numCurrentProjectiles < numSimultaneousProjectiles && currentCooldown < 0.0f)
            {
                currentCooldown = shootingCooldown;
                numCurrentProjectiles++;

                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = transform.position.z;
                Vector3 shootingDirection = (mousePosition - transform.position).normalized;

                GameObject currentProjectile = Instantiate<GameObject>(playerProjectile);
                currentProjectile.SendMessage("OnInstantiate", shootingDirection);
            }
        }
        float healthFraction = (float)currentHealth / (float)startingHealth;
        healthBar.SendMessage("SetCurrentFraction", healthFraction);
    }

    // Events
    void OnHitByEnemy(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            FindObjectOfType<GameRulesBehaviour>().SendMessage("OnPlayerDead");
        }
    }
    void OnProjectileDestroyed()
    {
        numCurrentProjectiles--;
    }
    void OnNextDifficultyLevel()
    {
        currentHealth += 10;
    }
}

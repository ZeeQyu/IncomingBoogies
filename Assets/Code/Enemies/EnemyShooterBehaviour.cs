using UnityEngine;

public class EnemyShooterBehaviour : EnemyBehaviour
{
    [SerializeField] private GameObject loadingBar;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float reloadTime;

    private float reloadMultiplier;
    private float currentReloadFraction;

    // Behaviours
    void Start()
    {
        currentHealth = startingHealth;
        isProjectile = false;

        reloadMultiplier = 1.0f / reloadTime;
        currentReloadFraction = 0.0f;
    }
    void Update()
    {
        currentReloadFraction += Time.deltaTime * reloadMultiplier;
        loadingBar.GetComponent<SpriteBarBehaviour>().SetCurrentFraction(currentReloadFraction);

        if (currentReloadFraction >= 1.0f)
        {
            currentReloadFraction -= 1.0f;

            // Spawn a projectile
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SendMessage("SetPosition", transform.position);
        }
    }
}

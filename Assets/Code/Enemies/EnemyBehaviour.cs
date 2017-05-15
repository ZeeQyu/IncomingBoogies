using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float timeToCenter;
    [SerializeField]
    protected int startingHealth;
    [SerializeField]
    protected int damage;

    protected float movementSpeed;
    protected int currentHealth;
    protected bool isProjectile;

    // Behaviour
    void Start()
    {
        isProjectile = false;
        StartMoving();
    }
    void Awake()
    {
        currentHealth = startingHealth;
    }

    protected void StartMoving()
    // SetPosition really should be used before this function
    // SetPosition will happen before Start if it is called directly after the enemy is instantiated
    {
        float travelDistance = FindObjectOfType<GameRulesBehaviour>().GetEnemyTravelDistance();
        movementSpeed = travelDistance / timeToCenter;

        // Rotation
        Vector2 lookDirection = (FindObjectOfType<PlayerBehaviour>().transform.position - transform.position).normalized;
        transform.Rotate(0.0f, 0.0f, Mathf.Rad2Deg * Mathf.Atan2(lookDirection.y, lookDirection.x));

        // Velocity 
        GetComponent<Rigidbody2D>().velocity = new Vector3(lookDirection.x, lookDirection.y, 0.0f) * movementSpeed;
    }

    // Getters/Setters
    void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    // Events
    void OnHitByPlayerProjectile(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (!isProjectile)

                FindObjectOfType<GameRulesBehaviour>().SendMessage("OnEnemyDestroyed");
            GameObject.Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider == FindObjectOfType<PlayerBehaviour>().GetComponent<Collider2D>())
        {
            collider.gameObject.SendMessage("OnHitByEnemy", damage, SendMessageOptions.DontRequireReceiver);
            FindObjectOfType<GameRulesBehaviour>().SendMessage("OnPlayerHit");
            if (!isProjectile)
                FindObjectOfType<GameRulesBehaviour>().SendMessage("OnEnemyDestroyed");
            GameObject.Destroy(this.gameObject);
        }
    }
}
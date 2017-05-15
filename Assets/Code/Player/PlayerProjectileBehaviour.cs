using UnityEngine;

public class PlayerProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float timeUntilWallHit = 0.25f; // In seconds
    [SerializeField] private int damage = 1; // In hitpoints

    private Vector3 movementDirection;
    private float movementSpeed;

    // Events
    void OnInstantiate(Vector3 newDirection)
    {
        // Direction
        movementDirection = newDirection;
        transform.Rotate(0.0f, 0.0f, Mathf.Rad2Deg * Mathf.Atan2(movementDirection.y, movementDirection.x));

        // Speed
        float arenaRadius = FindObjectOfType<GameRulesBehaviour>().GetArenaRadius();
        movementSpeed = arenaRadius / timeUntilWallHit;

        GetComponent<Rigidbody2D>().velocity = movementDirection * movementSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != FindObjectOfType<PlayerBehaviour>().GetComponent<Collider2D>())
        {
            collider.gameObject.SendMessage("OnHitByPlayerProjectile", damage, SendMessageOptions.DontRequireReceiver);
            FindObjectOfType<PlayerBehaviour>().SendMessage("OnProjectileDestroyed");
            GameObject.Destroy(this.gameObject);
        }
    }
}


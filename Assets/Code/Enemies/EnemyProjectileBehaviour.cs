
public class EnemyProjectileBehaviour : EnemyBehaviour
{
    // Behaviour
    void Start()
    {
        isProjectile = true;
        StartMoving();
    }
}

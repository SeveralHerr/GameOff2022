public class GreenAppleProjectile : Projectile<GreenAppleProjectile>
{
    void Update()
    {
        MoveTowardsTarget();
        Timer.RunTimer(10f, () => Destroy(gameObject));
    }
}
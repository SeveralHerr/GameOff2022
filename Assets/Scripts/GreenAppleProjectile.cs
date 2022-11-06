public class GreenAppleProjectile : Projectile<GreenAppleProjectile>
{
    void Update()
    {
        MoveTowardsTarget();
    }
}
using static Zenject.CheatSheet;


public class AppleProjectile : Projectile<AppleProjectile> 
{
    void Update()
    {
        MoveTowardsTarget();
        Timer.RunTimer(10f, () => Destroy(gameObject));
    }
}

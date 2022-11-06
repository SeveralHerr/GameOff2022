using static Zenject.CheatSheet;


public class AppleProjectile : Projectile<AppleProjectile> 
{ 

    void Update()
    {
        MoveTowardsTarget();
    }
}

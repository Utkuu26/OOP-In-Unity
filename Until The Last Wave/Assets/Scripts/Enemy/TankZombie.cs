public class TankZombie : EnemyBase
{
    protected override float ContactDamage => 30f;
    protected override void Start()
    {
        maxHealth = 60f;
        base.Start();
        agent.speed = 1f;
    }
}

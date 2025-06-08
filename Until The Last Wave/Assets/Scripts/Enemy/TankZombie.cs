public class TankZombie : EnemyBase
{
    protected override void Start()
    {
        maxHealth = 200f;
        base.Start();
        agent.speed = 1f;
    }
}

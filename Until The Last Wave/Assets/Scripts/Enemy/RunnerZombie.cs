public class RunnerZombie : EnemyBase
{
    protected override float ContactDamage => 10f;

    protected override void Start()
    {
        maxHealth = 20f;
        base.Start();
        agent.speed = 4.5f;
    }
}

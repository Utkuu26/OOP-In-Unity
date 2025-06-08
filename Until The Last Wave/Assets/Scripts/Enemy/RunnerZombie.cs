public class RunnerZombie : EnemyBase
{
    protected override void Start()
    {
        maxHealth = 60f;
        base.Start();
        agent.speed = 4.5f;
    }
}

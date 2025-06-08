public class Zombie : EnemyBase
{
    protected override void Start()
    {
        maxHealth = 100f;
        base.Start();
        agent.speed = 2f;
    }
}

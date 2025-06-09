public class Zombie : EnemyBase
{
    protected override void Start()
    {
        maxHealth = 35f;
        base.Start();
        agent.speed = 2f;
    }
}

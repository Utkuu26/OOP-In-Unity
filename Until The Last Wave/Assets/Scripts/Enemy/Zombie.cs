public class Zombie : EnemyBase
{
    protected override float ContactDamage => 10f;

    protected override void Start()
    {
        maxHealth = 35f;
        base.Start();
        agent.speed = 2f;
    }
}

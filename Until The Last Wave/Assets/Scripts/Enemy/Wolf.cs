public class Wolf : EnemyBase
{
    protected override float ContactDamage => 20f;

    protected override void Start()
    {
        maxHealth = 20f;
        base.Start();
        agent.speed = 5.5f;
    }
}

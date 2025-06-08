public class Wolf : EnemyBase
{
    protected override void Start()
    {
        maxHealth = 50f;
        base.Start();
        agent.speed = 5.5f;
    }
}

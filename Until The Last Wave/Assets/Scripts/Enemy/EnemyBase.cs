using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Transform player;

    public float maxHealth = 100f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f; // arka arkaya saldırmayı engellemek için

    public float currentHealth;
    protected bool isDead = false;
    protected bool isAttacking = false;
    private float lastAttackTime = -Mathf.Infinity;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player")?.transform;
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            if (!isAttacking && Time.time >= lastAttackTime + attackCooldown)
            {
                agent.isStopped = true;
                animator.SetFloat("Speed", 0f);

                isAttacking = true;
                animator.SetBool("IsAttacking", true);

                lastAttackTime = Time.time;
            }
        }
        else
        {
            if (isAttacking)
            {
                animator.SetBool("IsAttacking", false);
                isAttacking = false;
            }

            agent.isStopped = false;
            agent.SetDestination(player.position);

            float currentSpeed = agent.velocity.magnitude;
            animator.SetFloat("Speed", currentSpeed);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void StopMovement() // animasyon eventiyle tetiklenebilir
    {
        if (agent != null)
        {
            agent.isStopped = true;
        }
    }

    public void ResumeMovement()
    {
        if (agent != null && !isDead)
        {
            agent.isStopped = false;
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
        agent.isStopped = true;
        animator.SetFloat("Speed", 0f);
        animator.SetTrigger("Die");
        Destroy(gameObject, 3f);
    }
}

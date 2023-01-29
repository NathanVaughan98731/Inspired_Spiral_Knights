using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour, IDamageable
{
    public NavMeshAgent agent;

    public GameObject weapon;
    public Punch punch;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health = 100f;
    public ParticleSystem hitParticleSystem;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        punch = weapon.GetComponent<Punch>();
        // Automatically find the attackRange based on the weapon
        //attackRange = 1;
        timeBetweenAttacks = 3f / punch.punchData.speed;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Make sure enemy does not move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack Code
            punch.Hit();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void Damage(int damage)
    {
        GameObject particles = Instantiate(hitParticleSystem.gameObject, transform);
        particles.GetComponent<ParticleSystem>().Play();

        this.health -= damage;
        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
            Destroy(particles.gameObject, 2f);
        }
        Destroy(particles.gameObject, 2f);
    }

    private void DestroyEnemy()
    {
        GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent("GameController") as GameController;
        controller.KilledEnemy();
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeAI : MonoBehaviour, IDamageable
{
    public Transform target;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health = 100f;

    // Slime
    public Face faces;
    public GameObject SlimeBody;
    public SlimeAnimationState currentState;
    public enum SlimeAnimationState { Idle, Walk, Jump, Attack, Damage }

    public Animator animator;
    public NavMeshAgent agent;

    private Material faceMaterial;
    private Vector3 originPos;

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
        target = GameObject.Find("Player").transform;
        faceMaterial = SlimeBody.GetComponent<Renderer>().materials[1];
        agent = GetComponent<NavMeshAgent>();
    }
    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        switch (currentState)
        {
            case SlimeAnimationState.Idle:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;
                StopAgent();
                SetFace(faces.Idleface);
                break;

            case SlimeAnimationState.Walk:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) return;
                SetFace(faces.WalkFace);
                agent.isStopped = false;
                agent.updateRotation = true;
                animator.SetFloat("Speed", agent.velocity.magnitude);

                break;

            case SlimeAnimationState.Jump:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) return;

                StopAgent();
                SetFace(faces.jumpFace);
                animator.SetTrigger("Jump");
                break;

            case SlimeAnimationState.Attack:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;
                StopAgent();
                SetFace(faces.attackFace);
                animator.SetTrigger("Attack");
                break;

            case SlimeAnimationState.Damage:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage0")
                    || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage1")
                    || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage2")) return;

                StopAgent();
                SetFace(faces.damageFace);
                animator.SetTrigger("Damage");

                break;
        }
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void StopAgent()
    {
        agent.isStopped = true;
        animator.SetFloat("Speed", 0);
        agent.updateRotation = false;
    }

    // Animation Event
    public void AlertObservers(string message)
    {

        if (message.Equals("AnimationDamageEnded"))
        {
            // When Animation ended check distance between current position and first position 
            //if it > 1 AI will back to first position 

            float distanceOrg = Vector3.Distance(transform.position, originPos);
            if (distanceOrg > 1f)
            {
                currentState = SlimeAnimationState.Walk;
            }
            else currentState = SlimeAnimationState.Idle;

            //Debug.Log("DamageAnimationEnded");
        }

        if (message.Equals("AnimationAttackEnded"))
        {
            currentState = SlimeAnimationState.Idle;
        }

        if (message.Equals("AnimationJumpEnded"))
        {
            currentState = SlimeAnimationState.Idle;
        }
    }
    void OnAnimatorMove()
    {
        // apply root motion to AI
        Vector3 position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
        agent.nextPosition = transform.position;
    }

    private void Patrolling()
    {
        currentState = SlimeAnimationState.Walk;

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
        currentState = SlimeAnimationState.Walk;
        agent.SetDestination(target.position);
    }

    private void AttackPlayer()
    {
        // Make sure enemy does not move
        agent.SetDestination(transform.position);

        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            // Attack Code
            currentState = SlimeAnimationState.Attack;
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
        currentState = SlimeAnimationState.Damage;
        this.health -= damage;
        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}

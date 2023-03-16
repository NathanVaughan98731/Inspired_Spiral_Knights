using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public Vector3 PlayerMovementInput;

    [SerializeField] private Transform camera;

    [SerializeField] private Rigidbody PlayerBody;
    public CinemachineImpulseSource impulse;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Jumpforce;
    private AbilityHolder abilityHolder;

    [SerializeField] private int health = 100;
    [SerializeField] private Slider healthBar;
    public ParticleSystem hitParticleSystem;

    private const string AXIS_HORIZONTAL = "Horizontal";
    private const string AXIS_VERTICAL = "Vertical";

    private const int MAX_HEALTH = 100;

    private bool isWalking;
    private bool isJumping;
    private bool isFalling;

    public float FallingThreshold = -10f;

    void Start()
    {
        //abilityHolder = GetComponent<AbilityHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = MAX_HEALTH;
            healthBar.value = health;
        }

        if (PlayerBody.velocity.y == 0)
        {
            PlayerJump();
        }

        if (PlayerBody.velocity.y < FallingThreshold)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }


    }

    public void Shake()
    {
        impulse.GenerateImpulse(2f);
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = PlayerMovementInput * Speed;

        // Camera direction
        Vector3 camForward = camera.forward;
        Vector3 camRight = camera.right;

        camForward.y = 0;
        camRight.y = 0;

        // Creating relative camera direction
        Vector3 forwardRelative = MoveVector.z * camForward;
        Vector3 rightRelative = MoveVector.x * forwardRelative;

        Vector3 moveDirection = forwardRelative + rightRelative;

        PlayerBody.velocity = new Vector3(moveDirection.x, PlayerBody.velocity.y, moveDirection.z);


        //if (abilityHolder != null)
        //{
        //    if (abilityHolder.state != AbilityHolder.AbilityState.active)
        //    {
        //        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);
        //    }
        //}

        //if (abilityHolder == null)
        //{
        //    PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);
        //}


        isWalking = PlayerBody.velocity.x != 0 || PlayerBody.velocity.z != 0;
    }

    private void RotatePlayer()
    {

        //transform.rotation = new Quaternion(transform.rotation.x, camera.rotation.y, transform.rotation.z, transform.rotation.w);
        //int layerMask1 = 1 << LayerMask.NameToLayer("WhatIsGround");
        //int layerMask2 = 1 << LayerMask.NameToLayer("WhatIsEnemy");
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        //// Checking if the mouse is on an enemy, if it is then lock the aim to the center of the enemy.
        //// Makes it easier for the player to shoot the appropriate enemy.
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask2))
        //{
        //    var target = hit.point;
        //    target.y = transform.position.y;
        //    Vector3 newTargetPos = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
        //    transform.LookAt(newTargetPos);

        //}
        //// Otherwise, allow the player to rotate according to where the mouse is on the map.
        //else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask1))
        //{
        //    var target = hit.point;
        //    target.y = transform.position.y;
        //    transform.LookAt(target);
        //}


    }

    private void FixedUpdate()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis(AXIS_HORIZONTAL), 0f, Input.GetAxis(AXIS_VERTICAL));
        MovePlayer();
        RotatePlayer();

    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            isJumping = true;
            PlayerBody.velocity += new Vector3(0, Jumpforce, 0);
        }
        if (PlayerBody.velocity.y < 4)
        {
            isJumping = false;
        }
    }

    public void Damage(int damage)
    {
        Shake();

        GameObject particles = Instantiate(hitParticleSystem.gameObject, this.transform);
        particles.GetComponent<ParticleSystem>().Play();

        Shield playerShield = this.gameObject.GetComponentInChildren<Shield>();
        if (playerShield != null)
        {
            if (playerShield.shieldActivated)
            {
                playerShield.Damage(damage);
            }
            else
            {
                this.health -= damage;
                if (health <= 0)
                {
                    this.health = 0;
                    particles.gameObject.transform.parent = null;
                    Debug.Log("Dead");
                }
            }
            Destroy(particles.gameObject, 2f);

        }
        else
        {
            this.health -= damage;
            if (health <= 0)
            {
                this.health = 0;
                particles.gameObject.transform.parent = null;
                Debug.Log("Dead");
            }
            Destroy(particles.gameObject, 2f);
        }
        
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    public bool IsFalling()
    {
        return isFalling;
    }
}

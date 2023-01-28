using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public Vector3 PlayerMovementInput;

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


    void Start()
    {
        abilityHolder = GetComponent<AbilityHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = MAX_HEALTH;
        healthBar.value = health;

    }

    public void Shake()
    {
        impulse.GenerateImpulse(2f);
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = PlayerMovementInput * Speed;
        if (abilityHolder.state != AbilityHolder.AbilityState.active)
        {
            PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerBody.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
        }
    }

    private void RotatePlayer()
    {
        int layerMask = 1 << LayerMask.NameToLayer("WhatIsGround");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            var target = hit.point;
            target.y = transform.position.y;
            transform.LookAt(target);
            //transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        }
        

    }

    private void FixedUpdate()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis(AXIS_HORIZONTAL), 0f, Input.GetAxis(AXIS_VERTICAL));
        MovePlayer();
        RotatePlayer();
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
}

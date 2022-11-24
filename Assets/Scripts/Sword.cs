using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sword : MonoBehaviour
{
    // Should make these readable
    [SerializeField] public SwordData swordData;

    [Header("Sword Properties")]
    [SerializeField] public int damage;
    [SerializeField] public int speed;




    [SerializeField] private Transform swordEdge;

    [SerializeField] private bool chargingSword;
    [SerializeField] private float timeCharged;

    [SerializeField] private GameObject swordSlashPrefab;
    [SerializeField] private GameObject swordAttackArea;
    [SerializeField] private GameObject swordModel;
    [SerializeField] private GameObject swordParticleSystem;
    [SerializeField] private Animator swordAnimator;
    [SerializeField] private TextMeshProUGUI ammoDisplay;
    [SerializeField] private TextMeshProUGUI weaponNameDisplay;

    public float swordSlashSpeed;

    float timeSinceLastSlash;
    float timer;

    private void Start()
    {
        damage = swordData.damage;
        PlayerSlash.slashInput += Slash;
        PlayerSlash.deactivate += Deactivate;
        PlayerSlash.chargeInput += Charge;
        swordParticleSystem.SetActive(false);
    }

    private bool CanSlash() => timeSinceLastSlash > 1f / swordData.speed;

    public void Slash()
    {
        if (CanSlash() && this.gameObject.activeSelf)
        {
            //var swordSlash = Instantiate(swordSlashPrefab, swordEdge.position, swordEdge.rotation);
            //swordSlash.GetComponent<>
            swordAttackArea.SetActive(true);
            timeSinceLastSlash = 0;
            OnSwordSlash();
        }
        chargingSword = false;

    }

    public void Deactivate()
    {
        swordAttackArea.SetActive(false);
    }



    private void Update()
    {
        timeSinceLastSlash += Time.deltaTime;
        if (swordAttackArea.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer >= 0.1f)
            {
                timer = 0;
                swordAttackArea.SetActive(false);
            }
        }

        if (chargingSword)
        {
            timeCharged += Time.deltaTime;
        }
        else
        {
            timeCharged = 0;
        }

        if (ammoDisplay != null)
        {
            ammoDisplay.SetText("0 / 0");
        }        
        if (weaponNameDisplay != null)
        {
            weaponNameDisplay.SetText(swordData.name);
        }

    }

    public void Charge()
    {
        chargingSword = true;
        Debug.Log(damage);

        if (timeCharged >= 2f)
        {
            damage = swordData.damage + 10;
        }
        else
        {
            damage = swordData.damage;
        }
    }

    private void OnSwordSlash()
    {
        StartCoroutine(SwordSwing());
    }

    IEnumerator SwordSwing()
    {
        swordParticleSystem.SetActive(true);
        swordAnimator.SetFloat("SwordSwingSpeed", swordData.speed);
        swordAnimator.Play("SwordSwing");
        yield return new WaitForSeconds(1f / swordData.speed);
        swordAnimator.Play("New State");
        swordParticleSystem.SetActive(false);
    }
}

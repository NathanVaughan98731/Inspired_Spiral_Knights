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
    [SerializeField] public int bonusDamage;
    [SerializeField] public float bonusChargeTime;
    [SerializeField] public int bonusChargeDamage;
    [SerializeField] public float bonusSpeed;

    [SerializeField] public int totalDamage;
    [SerializeField] public float totalChargeTime;
    [SerializeField] public int totalChargeDamage;
    [SerializeField] public float totalSpeed;


    [SerializeField] private float timeCharged;

    [Header("Sword Objects and Animation")]
    [SerializeField] private Transform swordEdge;
    [SerializeField] private GameObject swordSlashPrefab;
    [SerializeField] private GameObject swordAttackArea;
    [SerializeField] private GameObject swordModel;
    [SerializeField] private GameObject swordParticleSystem;
    [SerializeField] private GameObject swordChargeParticleSystem;
    [SerializeField] private Animator swordAnimator;
    [SerializeField] private TextMeshProUGUI ammoDisplay;
    [SerializeField] private TextMeshProUGUI weaponNameDisplay;

    public float swordSlashSpeed;

    float timeSinceLastSlash;
    float timer;
    float chargeTimer;

    private void Start()
    {
        totalDamage = swordData.damage + bonusDamage;
        totalChargeTime = swordData.chargeTime + bonusChargeTime;
        totalChargeDamage = swordData.chargeDamage + bonusChargeDamage;
        totalSpeed = swordData.speed + bonusSpeed;
        PlayerSlash.slashInput += Slash;
        PlayerSlash.chargeInput += Charge;
        PlayerSlash.countChargeTime += CountChargeTime;
        swordParticleSystem.SetActive(false);
        swordChargeParticleSystem.SetActive(false);
    }

    private void CountChargeTime()
    {
        chargeTimer += Time.deltaTime;
        if (chargeTimer >= this.totalChargeTime)
        {
            swordChargeParticleSystem.SetActive(true);
        }
    }

    private bool CanSlash() => timeSinceLastSlash > 1f / swordData.speed;

    public void Slash()
    {
        if (CanSlash() && this.gameObject.activeSelf)
        {
            swordAttackArea.SetActive(true);
            timeSinceLastSlash = 0;
            OnSwordSlash();
        }
        swordChargeParticleSystem.SetActive(false);
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
        if (chargeTimer >= this.totalChargeTime)
        {
            totalDamage = swordData.damage + bonusDamage + (int)totalChargeDamage;
            Slash();
            Discharge();
        }
        else
        {
            totalDamage = swordData.damage + bonusDamage;
            Slash();
        }
        chargeTimer = 0;
        swordChargeParticleSystem.SetActive(false);
    }

    public void Discharge()
    {
        var slashProjectile = Instantiate(swordSlashPrefab, swordEdge.position, swordEdge.rotation);
        slashProjectile.GetComponent<SlashProjectile>().direction = swordEdge.forward * swordData.slashProjectileSpeed;
        slashProjectile.GetComponent<SlashProjectile>().setSlashDamage(swordData.chargeDamage);
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

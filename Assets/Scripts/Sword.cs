using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Should make these readable
    [SerializeField] public SwordData swordData;
    [SerializeField] private Transform swordEdge;
    [SerializeField] private GameObject swordSlashPrefab;
    [SerializeField] private GameObject swordAttackArea;
    [SerializeField] private GameObject swordModel;
    [SerializeField] private GameObject swordParticleSystem;
    [SerializeField] private Animator swordAnimator;

    public float swordSlashSpeed;

    float timeSinceLastSlash;
    float timer;

    private void Start()
    {
        PlayerSlash.slashInput += Slash;
        PlayerSlash.deactivate += Deactivate;
        PlayerSlash.chargeInput += StartCharge;
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

    }

    public void StartCharge()
    {

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

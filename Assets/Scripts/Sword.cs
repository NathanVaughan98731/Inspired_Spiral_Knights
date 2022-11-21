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

    public float swordSlashSpeed;

    float timeSinceLastSlash;
    float timer;

    private void Start()
    {
        PlayerSlash.slashInput += Slash;
        PlayerSlash.deactivate += Deactivate;
        PlayerSlash.chargeInput += StartCharge;
    }

    private bool CanSlash() => timeSinceLastSlash > 1f / swordData.speed;

    public void Slash()
    {
        Debug.Log(CanSlash());
        if (CanSlash())
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
            if (timer >= 1f / swordData.speed)
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
        
    }
}

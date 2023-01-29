using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField] public GameObject holder;
    [SerializeField] public PunchData punchData;

    [Header("Punch Properties")]
    [SerializeField] public GameObject punchAttackArea;

    float timeSinceLastPunch;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        punchAttackArea.GetComponent<AttackArea>().damage = punchData.damage;
        punchAttackArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastPunch += Time.deltaTime;
        if (punchAttackArea.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer >= 0.1f)
            {
                timer = 0;
                punchAttackArea.SetActive(false);
            }
        }
    }

    private bool CanPunch() => timeSinceLastPunch > 1f / punchData.speed;


    public void Hit()
    {
        if (CanPunch() && this.gameObject.activeSelf)
        {
            Debug.Log("hi");

            punchAttackArea.SetActive(true);
            timeSinceLastPunch = 0;
        }
    }

}

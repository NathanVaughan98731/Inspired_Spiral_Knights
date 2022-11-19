using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 PlayerMovementInput;

    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Camera PlayerCamera;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Jumpforce;
    private AbilityHolder abilityHolder;

    private const string AXIS_HORIZONTAL = "Horizontal";
    private const string AXIS_VERTICAL = "Vertical";

    void Start()
    {
        abilityHolder = GetComponent<AbilityHolder>();
    }

    // Update is called once per frame
    void Update()
    {


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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(hit.point);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
            //Debug.Log(hit.point);
        }
        

    }

    private void FixedUpdate()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis(AXIS_HORIZONTAL), 0f, Input.GetAxis(AXIS_VERTICAL));
        MovePlayer();
        RotatePlayer();
    }
}

using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public enum InputAction { Up, Down, Left, Right, Grab, Interact, Run }

    [Header("키 바인딩")]
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode grabKey = KeyCode.Z;
    [SerializeField] private KeyCode interactionKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode runKey = KeyCode.LeftShift;

    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float runMultiplier = 2.0f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float gravity = -9.8f;

    [Header("잡기 설정")]
    [SerializeField] private Transform holdPoint;
    [SerializeField] private float grabRange = 1.2f;
    [SerializeField] private LayerMask foodLayer;
    [SerializeField] private float dropForward = 1f;

    private CharacterController controller;
    private float verticalVelocity;
    private Dictionary<InputAction, KeyCode> binds;
    private Food heldFood;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        binds = new Dictionary<InputAction, KeyCode>
        {
            { InputAction.Up,       upKey },
            { InputAction.Down,     downKey },
            { InputAction.Left,     leftKey },
            { InputAction.Right,    rightKey },
            { InputAction.Grab,     grabKey },
            { InputAction.Interact, interactionKey },
            { InputAction.Run,      runKey },
        };
    }

    private bool IsHeld(InputAction a) => Input.GetKey(binds[a]);
    private bool WasPressed(InputAction a) => Input.GetKeyDown(binds[a]);

    void Update()
    {
        HandleMovement();
        HandleActions();
    }

    private void HandleMovement()
    {
        float x = (IsHeld(InputAction.Right) ? 1f : 0f) - (IsHeld(InputAction.Left) ? 1f : 0f);
        float z = (IsHeld(InputAction.Up) ? 1f : 0f) - (IsHeld(InputAction.Down) ? 1f : 0f);
        Vector3 moveInput = new Vector3(x, 0f, z).normalized;

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        float speed = IsHeld(InputAction.Run) ? moveSpeed * runMultiplier : moveSpeed;

        Vector3 velocity = moveInput * speed;
        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
        if (moveInput.sqrMagnitude > 0.01f)
        {
            Quaternion target = Quaternion.LookRotation(moveInput, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleActions()
    {
        if (WasPressed(InputAction.Grab))
        {
            if (heldFood == null)
                TryGrab();
            else
                DropFood();
        }

        if (WasPressed(InputAction.Interact))
        {
            Debug.Log("썰기 or 굽기 or etc");
        }
    }

    private void TryGrab()
    {
        Vector3 center = transform.position + transform.forward * 0.5f;
        Collider[] hits = Physics.OverlapSphere(center, grabRange, foodLayer);

        Food nearest = null;
        float minDist = float.MaxValue;
        foreach (var h in hits)
        {
            Food f = h.GetComponent<Food>();
            if (f == null || f.IsHeld) 
                continue;

            float d = (h.transform.position - center).sqrMagnitude;
            if (d < minDist) { minDist = d; nearest = f; }
        }

        if (nearest != null)
        {
            heldFood = nearest;
            heldFood.PickUp(holdPoint);
        }
    }

    private void DropFood()
    {
        Vector3 dropPos = transform.position + transform.forward * dropForward;
        heldFood.Drop(dropPos);
        heldFood = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = transform.position + transform.forward * 0.5f;
        Gizmos.DrawWireSphere(center, grabRange);
    }
}
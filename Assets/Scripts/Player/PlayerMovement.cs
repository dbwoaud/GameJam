using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("키 바인딩")]
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode runKey = KeyCode.LeftShift;

    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float runMultiplier = 2.0f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float gravity = -9.8f;

    private CharacterController controller;
    private float verticalVelocity;
    private Dictionary<MoveInputAction, KeyCode> binds;
    private PlayerInput playerInput;
   

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        binds = new Dictionary<MoveInputAction, KeyCode>
        {
            { MoveInputAction.Up,       upKey },
            { MoveInputAction.Down,     downKey },
            { MoveInputAction.Left,     leftKey },
            { MoveInputAction.Right,    rightKey },
            { MoveInputAction.Run, runKey }
        };
    }

    private bool IsHeld(MoveInputAction a) => Input.GetKey(binds[a]);
 

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = (IsHeld(MoveInputAction.Right) ? 1f : 0f) - (IsHeld(MoveInputAction.Left) ? 1f : 0f);
        float z = (IsHeld(MoveInputAction.Up) ? 1f : 0f) - (IsHeld(MoveInputAction.Down) ? 1f : 0f);
        Vector3 moveInput = new Vector3(x, 0f, z).normalized;

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        float speed = IsHeld(MoveInputAction.Run) ? moveSpeed * runMultiplier : moveSpeed;

        Vector3 velocity = moveInput * speed;
        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
        if (moveInput.sqrMagnitude > 0.01f)
        {
            Quaternion target = Quaternion.LookRotation(moveInput, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
        }
    }
}
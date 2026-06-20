using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Ű ���ε�")]
    [SerializeField] private KeyCode grabKey = KeyCode.Z;
    [SerializeField] private KeyCode interactionKey = KeyCode.LeftControl;

    [Header("��� ����")]
    [SerializeField] private Transform holdPoint;
    [SerializeField] private float grabRange = 1.2f;
    [SerializeField] private float facingThreshold = 0.3f;
    [SerializeField] private float dropForward = 1f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask carryableLayer;

    private Dictionary<InteractionInputAction, KeyCode> binds;
    private Carryable heldItem;

    public Carryable HeldItem => heldItem;
    public bool IsHolding => heldItem != null;

    [SerializeField] Animator animator;

    public void Hold(Carryable item)
    {
        if (item == null) 
            return;
        heldItem = item;
        item.PickUp(holdPoint);

        animator.SetBool("Grap", true);
    }

    public Carryable TakeFromHands()
    {
        Carryable item = heldItem;
        heldItem = null;

        animator.SetBool("Grap", false);

        return item;
    }

    public void DropToWorld()
    {
        if (heldItem == null) 
            return;

        heldItem.Drop(transform.position + transform.forward * dropForward);
        heldItem = null;

        animator.SetBool("Grap", false);
    }

    void Awake()
    {
        binds = new Dictionary<InteractionInputAction, KeyCode>
        {
            { InteractionInputAction.Grab,     grabKey },
            { InteractionInputAction.Interact, interactionKey },
        };
    }

    private bool WasPressed(InteractionInputAction a) => Input.GetKeyDown(binds[a]);

    void Update()
    {
        HandleActions();
    }

    private void HandleActions()
    {
        if (WasPressed(InteractionInputAction.Grab)) 
            OnGrabPressed();
        if (WasPressed(InteractionInputAction.Interact)) 
            OnInteractionPressed();
    }

    private void OnGrabPressed()
    {
        IInteractable interactable = FindFrontInteractable();
        if (interactable != null) 
        { 
            interactable.OnGrab(this); 
            return; 
        }


        if (IsHolding)
        {
            Carryable front = FindFrontCarryable();
            if (heldItem is CookingTool tool && tool.IsDone && front is Plate target)
            {
                if (tool.TryServeTo(target))
                    return;
            }

            if (heldItem is Plate plate && front is CookingTool placedTool)
            {
                if (placedTool.IsDone && placedTool.TryServeTo(plate))
                    return;
            }

            if (heldItem is Plate heldPlate && front is Plate other)
            {
                if (heldPlate.TryStack(other))
                    return;
            }

            DropToWorld();
            return;
        }

        else
        {
            Carryable item = FindFrontCarryable();
            if (item != null) 
                Hold(item);
        }
    }

    private void OnInteractionPressed()
    {
        IInteractable interactable = FindFrontInteractable();
        if (interactable != null) 
            interactable.OnInteract(this);
    }

    private IInteractable FindFrontInteractable()
    {
        Vector3 center = transform.position + transform.forward * 0.5f;
        Collider[] hits = Physics.OverlapSphere(center, grabRange, interactableLayer);

        IInteractable best = null;
        float bestDot = facingThreshold;
        foreach (var h in hits)
        {
            IInteractable s = h.GetComponentInParent<IInteractable>();
            if (s == null) 
                continue;

            Vector3 dir = h.transform.position - transform.position;
            dir.y = 0f;

            if (dir.sqrMagnitude < 0.0001f) 
                return s;

            float dot = Vector3.Dot(transform.forward, dir.normalized);
            if (dot > bestDot) 
            { 
                bestDot = dot; 
                best = s; 
            }
        }
        return best;
    }

    private Carryable FindFrontCarryable()
    {
        Vector3 center = transform.position + transform.forward * 0.5f;
        Collider[] hits = Physics.OverlapSphere(center, grabRange, carryableLayer);

        Carryable nearest = null;
        float minDist = float.MaxValue;
        foreach (var h in hits)
        {
            Carryable c = h.GetComponentInParent<Carryable>();
            if (c == null || c.IsHeld) 
                continue;

            float d = (h.transform.position - center).sqrMagnitude;
            if (d < minDist)
            { 
                minDist = d; 
                nearest = c; 
            }
        }
        return nearest;
    }


    public void TriggerWash()
    {
        animator.SetTrigger("WashTrigger");
    }

    public void TriggerCut()
    {
        animator.SetTrigger("CutTrigger");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 0.5f, grabRange);
    }
}

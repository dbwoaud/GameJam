using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Food : MonoBehaviour
{
    public bool IsHeld { get; private set; }

    private Rigidbody rb;
    private Collider col;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void PickUp(Transform holdPoint)
    {
        IsHeld = true;

        if (rb != null) 
            rb.isKinematic = true;
        if (col != null) 
            col.enabled = false;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Drop(Vector3 position)
    {
        IsHeld = false;

        transform.SetParent(null);
        transform.position = position;

        if (col != null) 
            col.enabled = true;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
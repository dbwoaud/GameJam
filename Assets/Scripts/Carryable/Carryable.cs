using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Carryable : MonoBehaviour
{
    public bool IsHeld { get; private set; } 

    private Rigidbody rb;
    private Collider col;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public virtual void PickUp(Transform holdPoint)
    {
        PlayObjectSound();

        IsHeld = true;
        if (rb != null) 
            rb.isKinematic = true;
        if (col != null) 
            col.enabled = false;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public virtual void Drop(Vector3 position)
    {
        PlayObjectSound();

        IsHeld = false;
        transform.SetParent(null);
        transform.position = position;

        if (col != null) col.enabled = true;
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public virtual void AttachTo(Transform socket)
    {
        PlayObjectSound();

        IsHeld = false;
        if (rb != null) 
            rb.isKinematic = true;
        if (col != null)
            col.enabled = false;

        transform.SetParent(socket);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    private void PlayObjectSound()
    {
        if (gameObject.TryGetComponent(out CookingTool c))
        {
            SoundManager.Instance.PlayOneShot(ResourceManager.Instance.Load<AudioClip>("Metal"));

            //  집었을 때 요리 소리 중단
            c.StopCookingSound();
            c.UIOff();
        }

        if (gameObject.TryGetComponent(out Ingredient i))
        {
            SoundManager.Instance.PlayOneShot(ResourceManager.Instance.Load<AudioClip>("PutIngredient"));
        }

        if (gameObject.TryGetComponent(out Plate p))
        {
            SoundManager.Instance.PlayOneShot(ResourceManager.Instance.Load<AudioClip>("Plate"));
        }

    }
}
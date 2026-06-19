using UnityEngine;

public class GhostHovering : MonoBehaviour
{
    [SerializeField] Transform model;
    [SerializeField] float hoverAmplitude = 0.2f;
    [SerializeField] float hoverFrequencyMultiplier = 1f;

    private void Update()
    {
        model.transform.localPosition = new Vector3(0, 0.5f + hoverAmplitude + hoverAmplitude * Mathf.Sin(Time.time * hoverFrequencyMultiplier), 0);
    }
}

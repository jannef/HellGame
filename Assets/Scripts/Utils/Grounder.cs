using UnityEngine;

public class Grounder : MonoBehaviour {

    [SerializeField] private LayerMask _groundingLayers;
    [SerializeField] private float _groundingHeightOffset = 0.2f;
    [SerializeField] private float _maxGroundingDistance = 100f;

    public void GroundGameObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _maxGroundingDistance, _groundingLayers))
        {
            transform.position = hit.point + Vector3.up * _groundingHeightOffset;
        }
        else
        {
            throw new UnityException("Grounder class tried to ground a GameObject, but no collider of defined grounding layer was found below the object.");
        }
    }
}

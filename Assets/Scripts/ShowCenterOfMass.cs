/*
 * ShowCenterOfMass.cs
 * Permet d'afficher dans l'éditeur le centre de masse du marteau
 */

using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class ShowCenterOfMass : MonoBehaviour
{
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        if (_rb == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(_rb.centerOfMass), 0.02f);
    }
}

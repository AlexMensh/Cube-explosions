using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    public void Explode(float explosionForce, float explosionRadius)
    {
        foreach (Rigidbody explodingObject in GetExplodingObjects(explosionRadius))
        {
            explodingObject.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        Destroy(gameObject);
    }

    private List<Rigidbody> GetExplodingObjects(float explosionRadius)
    {
        return Physics.OverlapSphere(transform.position, explosionRadius).Select(hit => hit.attachedRigidbody)
                                                                         .Where(rb => rb != null)
                                                                         .ToList();
    }
}
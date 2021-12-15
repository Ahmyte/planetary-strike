using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Collider collider;
    private void Start()
    {
        collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }
}

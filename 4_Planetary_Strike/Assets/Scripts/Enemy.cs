using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Collider collider;
    [SerializeField] private GameObject deathFX;
    private void Start()
    {
        collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

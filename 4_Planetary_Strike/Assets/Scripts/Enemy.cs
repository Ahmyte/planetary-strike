using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Collider collider;
    [SerializeField] private GameObject deathFX;
    [SerializeField] private Transform parentDirectory;
    private void Start()
    {
        collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentDirectory;
        Destroy(gameObject);
    }
}

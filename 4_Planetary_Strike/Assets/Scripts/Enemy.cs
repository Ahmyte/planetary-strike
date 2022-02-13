using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Collider collider;
    [SerializeField] private GameObject deathFX;
    [SerializeField] private Transform parentDirectory;
    
    [SerializeField] private int scorePerHit = 12;
    private ScoreBoard scoreBoard;
    private void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        scoreBoard.ScoreHit(scorePerHit);
        fx.transform.parent = parentDirectory;
        Destroy(gameObject);
    }
}

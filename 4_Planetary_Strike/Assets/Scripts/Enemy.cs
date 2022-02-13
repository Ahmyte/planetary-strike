using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Collider collider;
    [SerializeField] private GameObject deathFX;
    [SerializeField] private Transform parentDirectory;
    
    [SerializeField] private int scorePerHit = 12;
    [SerializeField] private int hits = 3;
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
        scoreBoard.ScoreHit(scorePerHit);
        hits--;
        if (hits < 1)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentDirectory;
        Destroy(gameObject);
    }
}

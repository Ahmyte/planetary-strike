using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 5f;
    void Start()
    {
        Destroy(gameObject,destroyDelay);
    }
}

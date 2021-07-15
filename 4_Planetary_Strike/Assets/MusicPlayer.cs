using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Invoke("LoadFirstScene", 5f);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}

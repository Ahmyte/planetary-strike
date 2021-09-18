using System;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

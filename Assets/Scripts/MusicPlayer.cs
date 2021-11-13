using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;
    public static MusicPlayer Instance

    {
        get { return instance; }
    }
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } 
        else 
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        
    }

    void Update()
    {
        player = GameObject.Find("Player");
        _playerMovement = player.GetComponent<PlayerMovement>();
        
        if(_playerMovement.feverMode)
        {
            audioSource.pitch = 1.2f;
        }
        else
        {
            audioSource.pitch = 1f;
        }
    }
}

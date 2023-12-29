using ServiceLocator.Map;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    public PlayerService playerService { get; private set; }
    public SoundService soundService { get; private set; }
    public MapService mapService { get; private set; }

    [SerializeField] private UIService uIService;  // private reference of UIService class

    public UIService UIService => uIService;  // public getter function of UIService object

    [SerializeField] public PlayerScriptableObject playerScriptableObject;
    [SerializeField] public SoundScriptableObject soundScriptableObject;
    [SerializeField] public MapScriptableObject mapScriptableObject;

    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioSource backgroundMusic;

    private void Start()
    {
        playerService = new PlayerService(playerScriptableObject);
        soundService = new SoundService(soundScriptableObject, audioEffects, backgroundMusic);
        mapService = new MapService(mapScriptableObject);
    }

    private void Update()
    {
        playerService.Update();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetReferences : MonoBehaviour {

    [SerializeField]
    private string asteroidDataPath;
    static public AsteroidData asteroidData;

    [SerializeField]
    private string blackHoleDataPath;
    static public BlackHoleData blackHoleData;

    [SerializeField]
    private string difficultyConfigDataPath;
    static public DifficultyConfigurationData difficultyConfigData;

    [SerializeField]
    private string gameConfigDataPath;
    public static GameConfigurationData gameConfigData;

    void Awake()
    {
        blackHoleData = Resources.Load(blackHoleDataPath) as BlackHoleData;
        gameConfigData = Resources.Load(gameConfigDataPath) as GameConfigurationData;
        asteroidData = Resources.Load(asteroidDataPath) as AsteroidData;
        difficultyConfigData = Resources.Load(difficultyConfigDataPath) as DifficultyConfigurationData;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetReferences : MonoBehaviour {

    [SerializeField]
    private string asteroidDataPath;
    static public AsteroidData _asteroidData;

    [SerializeField]
    string blackHoleDataPath;
    static BlackHoleData _blackHoleData;

    [SerializeField]
    private string difficultyConfigDataPath;
    static public DifficultyConfigurationData _difficultyConfigData;

    [SerializeField]
    private string gameConfigDataPath;
    public static GameConfigurationData _gameConfigData;

    void Awake()
    {
        _blackHoleData = Resources.Load(blackHoleDataPath) as BlackHoleData;
        _gameConfigData = Resources.Load(gameConfigDataPath) as GameConfigurationData;
        _asteroidData = Resources.Load(asteroidDataPath) as AsteroidData;
        _difficultyConfigData = Resources.Load(difficultyConfigDataPath) as DifficultyConfigurationData;
    }

    static public BlackHoleData blackHoleData
    {
        get
        {
            return _blackHoleData;
        }
    }

    static public GameConfigurationData gameConfigData
    {
        get
        {
            return _gameConfigData;
        }
    }

    static public AsteroidData asteroidData
    {
        get
        {
            return _asteroidData;
        }
    }

    static public DifficultyConfigurationData difficultyConfigData
    {
        get
        {
            return _difficultyConfigData;
        }
    }
}

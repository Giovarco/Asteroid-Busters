using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetReferences : MonoBehaviour {

    [SerializeField]
    private string asteroidDataPath;
    static AsteroidData _asteroidData;

    [SerializeField]
    string blackHoleDataPath;
    static BlackHoleData _blackHoleData;

    [SerializeField]
    private string difficultyConfigDataPath;
    static DifficultyConfigurationData _difficultyConfigData;

    [SerializeField]
    private string gameConfigDataPath;
    static GameConfigurationData _gameConfigData;

    [SerializeField]
    private string spaceDistortionEffectDataPath;
    static SpaceDistortionEffectData _spaceDistortionEffectData;

    [SerializeField]
    private string levelGenerationDataPath;
    static LevelGenerationData _levelGenerationData;

    [SerializeField]
    private string playerDataPath;
    static PlayerData _playerData;

    [SerializeField]
    private string shieldControllerDataPath;
    static ShieldControllerData _shieldControllerData;

    void Awake()
    {
        _blackHoleData = Resources.Load(blackHoleDataPath) as BlackHoleData;
        _gameConfigData = Resources.Load(gameConfigDataPath) as GameConfigurationData;
        _asteroidData = Resources.Load(asteroidDataPath) as AsteroidData;
        _difficultyConfigData = Resources.Load(difficultyConfigDataPath) as DifficultyConfigurationData;
        _spaceDistortionEffectData = Resources.Load(spaceDistortionEffectDataPath) as SpaceDistortionEffectData;
        _levelGenerationData = Resources.Load(levelGenerationDataPath) as LevelGenerationData;
        _playerData = Resources.Load(playerDataPath) as PlayerData;
        _shieldControllerData = Resources.Load(shieldControllerDataPath) as ShieldControllerData;
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

    static public SpaceDistortionEffectData spaceDistortionEffectData
    {
        get
        {
            return _spaceDistortionEffectData;
        }
    }

    static public LevelGenerationData levelGenerationData
    {
        get
        {
            return _levelGenerationData;
        }
    }

    public static PlayerData playerData
    {
        get
        {
            return _playerData;
        }
    }

    public static ShieldControllerData shieldControllerData
    {
        get
        {
            return _shieldControllerData;
        }
    }
}

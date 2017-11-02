using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetReferences : MonoBehaviour {

    public AsteroidData asteroidData;

    static public BlackHoleData blackHoleData;
    [SerializeField]
    private string blackHoleDataPath;

    public DifficultyConfigurationData difficultyConfigData;

    public GameConfigurationData gameConfigData;

    void Awake()
    {
        blackHoleData = Resources.Load(blackHoleDataPath) as BlackHoleData;
    }

}

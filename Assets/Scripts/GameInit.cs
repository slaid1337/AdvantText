using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;
    public GameUI gameUI;
    public BusinessData businessData;
    public GameConfig gameConfig;
    public MoneySystem moneySystem;

    private void Start()
    {
        if (SaveController.IsFirstStart())
        {
            SaveController.SetDefaultData(gameConfig);
            SaveController.SetFirstStart();
        }

        moneySystem = new MoneySystem();

        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        _systems.Add(moneySystem);
        _systems.Add(new BusinessSystem());

        _systems.Inject(gameUI);
        _systems.Inject(businessData);
        _systems.Inject(moneySystem);

        _systems.Init();
    }

    private void Update()
    {
        _systems.Run();
    }

    private void OnApplicationQuit()
    {
        _systems.Destroy();
    }
}

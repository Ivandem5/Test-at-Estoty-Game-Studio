using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GamePlaySceneInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    [SerializeField] private ResourceManager resources;
    [SerializeField] private SoundManager soundManagerPrefab;
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<ResourceManager>().FromInstance(resources).AsSingle();
        Container.Bind<InterfaceManager>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<ISoundManager>().To<SoundManager>().FromComponentInNewPrefab(soundManagerPrefab).AsSingle().NonLazy();
    }
}

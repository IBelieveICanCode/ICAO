using PathCreation.Follower;
using UnityEngine;
using Zenject;
using PathCreation;

public class DefaultInstaller : MonoInstaller
{
    [Inject]
    private GameConfig config;
    public override void InstallBindings()
    {
        //Debug.Log("Binding");
        //Container.Bind<PathFollower>().AsTransient().NonLazy();
        Container.BindFactory<FinishSpot, FinishSpot.FinishSpotFactory>()
            .FromComponentInNewPrefab(config.FinishPrefab)
            .WithGameObjectName("Finish");
        Container.BindFactory<LevelProgress, Vector3, PathCreator, Plane, Plane.PlaneFactory>()
           .FromComponentInNewPrefab(config.PlanePrefab)
           .WithGameObjectName("Plane");

    }
}
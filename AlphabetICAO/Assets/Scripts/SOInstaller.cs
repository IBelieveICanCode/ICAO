using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SoInstaller", menuName = "Create SO Installer")]
public class SOInstaller : ScriptableObjectInstaller
{
    [SerializeField]
    private GameConfig config;

    public override void InstallBindings()
    {
        Container.BindInstance(config);
    }
}

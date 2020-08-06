using System.Collections;
using System.Collections.Generic;
using Adminka;
using UnityEngine;
using PathCreation;
using PathCreation.Follower;
using UnityEditor;


namespace FactorySpace
{
    public abstract class Factory
    {
        public abstract IPlaneCommunicator GetProduct(int type, Vector3 spawnPos, PathCreator path);
    }

    public class PlaneFactory : Factory
    {
        public override IPlaneCommunicator GetProduct(int planeType, Vector3 spawnPos, PathCreator path)
        {
            PlaneSO _planeSpawned =  Resources.Load("ScriptableObject/Plane") as PlaneSO;
            Plane _plane = Object.Instantiate(_planeSpawned.Plane);
            _plane.SetPosition(spawnPos);
            _plane.PathFollower.pathCreator = path;

            switch (planeType)
            {
                case (int)PlaneTypes.OneLetterPlane:
                    _plane.WordsAmount = 1;
                    break;
                case (int)PlaneTypes.TwoLettersPlane:
                    _plane.WordsAmount = 2;
                    break;
                case (int)PlaneTypes.ThreeLettersPlane:
                    _plane.WordsAmount = 3;
                    break;
                default:
                    return null;
            }
            return _plane;//.GetComponent<IPlaneCommunicator>();
        }
    }

    public static class FactoryProducer
    {
        public static Factory GetFactory(FactoryProductType type)
        {
            switch (type)
            {
                case FactoryProductType.Planes:
                    return new PlaneFactory();
                default:
                    return null;
            }
        }
    }
    public enum PlaneTypes
    {
        OneLetterPlane, TwoLettersPlane, ThreeLettersPlane
    }

    public enum FactoryProductType
    {
        Planes
    }
}

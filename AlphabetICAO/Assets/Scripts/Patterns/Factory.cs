using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Follower;

namespace FactorySpace
{
    public abstract class Factory
    {
        public abstract IPlaneCommunicator GetProduct(int _Type, Vector3 _spawnPos, PathCreator path);
    }

    public class PlaneFactory : Factory
    {
        public override IPlaneCommunicator GetProduct(int planeType, Vector3 _spawnPos, PathCreator path)
        {
            GameObject _planeSpawned = Resources.Load("Prefabs/Plane") as GameObject;

            GameObject _newPlane = MonoBehaviour.Instantiate(_planeSpawned, _spawnPos, Quaternion.identity);
            _newPlane.GetComponent<PathFollower>().pathCreator = path;
            Plane _plane;
            switch (planeType)
            {
                case (int)PlaneTypes.OneLetterPlane:
                    _plane = _newPlane.AddComponent(typeof(OneLetterPlane)) as Plane;                 
                    break;
                case (int)PlaneTypes.TwoLettersPlane:
                    _plane = _newPlane.AddComponent(typeof(TwoLetterPlane)) as Plane;                    
                    break;
                default:
                    return null;
            }
            HUDControl.Instance.AskPlayer();
            return _plane.GetComponent<IPlaneCommunicator>();

        }
    }

    public class FactoryProducer
    {
        public static Factory GetFactory(FactoryProductType _type)
        {
            switch (_type)
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
        OneLetterPlane, TwoLettersPlane
    }

    public enum FactoryProductType
    {
        Planes
    }
}

using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {

                    var _goName = typeof(T).ToString();

                    var go = GameObject.Find(_goName);
                    if (go == null)
                    {
                        go = new GameObject();
                        go.name = _goName;
                    }
                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }
    }
    public virtual void OnApplicationQuit()
    {
        // release reference on exit
        instance = null;
    }
}
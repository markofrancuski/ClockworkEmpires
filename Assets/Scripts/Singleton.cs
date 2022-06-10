using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    protected virtual bool ShouldDestroyOnLoad { get { return false; } }
    public static T Instance
    {
        get { return _instance; }
        private set
        {
            _instance = value;
        }
    }
    private static T _instance;

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            if (!ShouldDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Debug.Log($"Instance of a {this.GetType()} Already exists. Destroying");
            Destroy(this.gameObject);
        }
    }
}
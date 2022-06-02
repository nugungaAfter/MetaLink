using UnityEngine.SceneManagement;
using UnityEngine;

namespace Metalink.Manager
{
    public abstract class Manager_Base : MonoBehaviour
    {
        public virtual void Awake()
        {
            DeviceDetection();

            SceneManager.sceneLoaded += OnSceneLoaded;
            OnSceneLoaded(this.gameObject.scene, LoadSceneMode.Single);
        }

        public abstract void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode);

        public abstract void DeviceDetection();
    }
}

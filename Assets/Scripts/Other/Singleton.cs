using UnityEngine;

namespace Metalink.Utility
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Destroy 여부 확인용
        private static bool g_ShuttingDown = false;
        private static object g_Lock = new object();
        private static T g_Instance;

        public static T Instance
        {
            get
            {
                // 게임 종료 시 Object 보다 싱글톤의 OnDestroy 가 먼저 실행 될 수도 있다. 
                // 해당 싱글톤을 gameObject.Ondestory() 에서는 사용하지 않거나 사용한다면 null 체크를 해주자
                if (g_ShuttingDown)
                {
                    Debug.Log("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null.");
                    return null;
                }

                lock (g_Lock)    //Thread Safe
                {
                    if (g_Instance == null)
                    {
                        g_Instance = (T)FindObjectOfType(typeof(T));

                        if (g_Instance == null)
                        {
                            var singletonObject = new GameObject();
                            g_Instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";

                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return g_Instance;
                }
            }
        }

        protected virtual void OnApplicationQuit()
        {
            g_ShuttingDown = true;
        }

        protected virtual void OnDestroy()
        {
            g_ShuttingDown = true;
        }
    }
}
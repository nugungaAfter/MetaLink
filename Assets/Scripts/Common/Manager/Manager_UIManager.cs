using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

using Metalink.UI;

namespace Metalink.Manager
{
    public abstract class Manager_UserInterfaceManagerBase : Manager_Base
    {
        [SerializeField] protected GameObject m_DesktopUserInterface;
        [SerializeField] protected GameObject m_VirtualUserInterface;
    }

    public class Manager_UIManager : Manager_UserInterfaceManagerBase, IManager_Start
    {
        public static Manager_UIManager g_Instance;
        protected UI_Base[] m_UIBases;

        public override void Awake()
        {
            if(g_Instance == null)
            {
                g_Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }

            base.Awake();
        }

        public override void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            m_UIBases = FindObjectsOfType<UI_Base>();
        }

        public override void DeviceDetection()
        {
            if (XRSettings.isDeviceActive)
            {
                m_VirtualUserInterface.SetActive(true);
            }
            else
            {
                m_DesktopUserInterface.SetActive(true);
            }
        }

        public T GetUI<T>()
        {
            foreach (var ui in m_UIBases)
            {
                if(ui.GetType() == typeof(T))
                    return ui.GetComponent<T>();
            }

            throw new System.NullReferenceException();
        }

        public T[] GetUIs<T>()
        {
            List<T> uis = new List<T>();

            return uis.ToArray();
        }
    }
}

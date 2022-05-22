using System.Collections.Generic;
using System.Collections;
using UnityEngine.XR;
using UnityEngine;

namespace Metalink.Manager
{
    public interface IManager_Start
    {
        public void DeviceDetection();
    }

    public class Manager_GameManagerBase : MonoBehaviour
    {
        
    }

    public class Manager_GameManagerExtension : Manager_GameManagerBase
    {
        [SerializeField] protected GameObject m_FirstPersonController;
        [SerializeField] protected GameObject m_XRRig;
    }

    public class Manager_GameManager : Manager_GameManagerExtension, IManager_Start
    {
        public void Awake()
        {
            DeviceDetection();
        }

        public void DeviceDetection()
        {
            if (XRSettings.isDeviceActive)
            {
                m_XRRig.SetActive(true);
            }
            else
            {
                m_FirstPersonController.SetActive(true);

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}

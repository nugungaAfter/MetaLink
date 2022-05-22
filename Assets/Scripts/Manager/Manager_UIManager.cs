using System.Collections.Generic;
using System.Collections;
using UnityEngine.XR;
using UnityEngine;

namespace Metalink.Manager
{
    public class Manager_UserInterfaceManagerBase : MonoBehaviour
    {

    }

    public class Manager_UserInterfaceExtension : Manager_GameManagerBase
    {
        [SerializeField] protected GameObject m_DesktopUserInterface;
        [SerializeField] protected GameObject m_VirtualUserInterface;
    }


    public class Manager_UIManager : Manager_UserInterfaceExtension, IManager_Start
    {
        public void Awake()
        {
            DeviceDetection();
        }

        public void DeviceDetection()
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
    }
}

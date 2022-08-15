using System.Collections.Generic;
using System.Collections;

using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using UnityEngine.XR;
using UnityEngine;

using Metalink.Player;

namespace Metalink.Manager
{
    public interface IManager_Start
    {
        public void DeviceDetection();
    }

    public abstract class Manager_GameManagerBase : Manager_Base
    {
        public Player_Controller m_FirstPersonController;
        public XROrigin m_XRRig;
    }

    public class Manager_GameManager : Manager_GameManagerBase, IManager_Start
    {
        public static Manager_GameManager g_Instance;

        public override void Awake()
        {
            if (g_Instance == null)
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

        public override void DeviceDetection()
        {
            if (XRSettings.isDeviceActive)
            {
                m_XRRig.gameObject.SetActive(true);
            }
            else
            {
                m_FirstPersonController.gameObject.SetActive(true);
                CursorActive(false);
            }
        }

        public override void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            CursorActive(false);
        }

        public void CursorActive(bool p_Enable)
        {
            Cursor.visible = p_Enable;

            if (p_Enable)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

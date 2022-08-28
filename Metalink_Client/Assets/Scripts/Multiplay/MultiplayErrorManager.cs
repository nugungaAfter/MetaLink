using UnityEngine.SceneManagement;
using UnityEngine;
using BackEnd;
using UI;

namespace Multiplay
{
    public class MultiplayErrorManager : MonoBehaviour
    {
        [SerializeField] private AlterUI alterUI;
        [SerializeField] private MultiplayCommonErrorTextSetting textSetting;

        void Start()
        {
            if (Backend.IsInitialized) {
                Backend.ErrorHandler.InitializePoll(true);

                Backend.ErrorHandler.OnMaintenanceError = () => {
                    Debug.Log("점검 에러 발생");
                    alterUI.Enable(textSetting.error_OnMaintenance, GoTitle);
                };
                Backend.ErrorHandler.OnTooManyRequestError = () => {
                    Debug.Log("403 에러 발생");
                    alterUI.Enable(textSetting.error_OnTooManyRequest);
                };
                Backend.ErrorHandler.OnTooManyRequestByLocalError = () => {
                    Debug.Log("403 로컬 에러 발생");
                    alterUI.Enable(textSetting.error_OnTooManyRequestByLocal);
                };
                Backend.ErrorHandler.OnOtherDeviceLoginDetectedError = () => {
                    Debug.Log("리프레시 불가");
                    alterUI.Enable(textSetting.error_OnOtherDeviceLoginDetected, GoTitle);
                };
            }
        }
        private void GoTitle() => Scene.SceneLoadManager.LoadScene("Title");

        void Update()
        {
            Backend.ErrorHandler.Poll();
        }
    }
}
using UnityEngine.Events;
using UnityEngine;

namespace Metalink.BackEnd
{
    public class BackEnd_Register : MonoBehaviour
    {
        public UnityAction<string> m_RegisterSuccessEvent;
        public UnityAction<string> m_RegisterFailureEvent;

        public void Register(string p_Email, string p_Password, string p_Username)
        {

        }
    }
}
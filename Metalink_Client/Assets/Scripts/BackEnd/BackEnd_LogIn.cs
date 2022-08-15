using UnityEngine.Events;
using UnityEngine;

namespace Metalink.BackEnd
{
    public class BackEnd_LogIn : MonoBehaviour
    {
        public UnityAction<string> m_LoginSuccessEvent;
        public UnityAction<string> m_LoginFailureEvent;

        public void LogIn(string p_Email, string p_Password)
        {

        }
    }
}
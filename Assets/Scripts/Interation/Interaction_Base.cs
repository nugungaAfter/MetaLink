using UnityEngine.Events;
using UnityEngine;

namespace Metalink.Interaction
{
    public interface Interaction_Object
    {
        public void Interaction();
    }

    public class Interaction_Base : MonoBehaviour
    {
        // 상호작용 시 실행할 이벤트
        public UnityEvent m_InteractionEvent;

        public virtual void Interaction() => m_InteractionEvent.Invoke();
    }
}
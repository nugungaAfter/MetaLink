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
        // ��ȣ�ۿ� �� ������ �̺�Ʈ
        public UnityEvent m_InteractionEvent;

        public virtual void Interaction() => m_InteractionEvent.Invoke();
    }
}
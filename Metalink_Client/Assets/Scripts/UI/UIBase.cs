using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class UIBase : MonoBehaviour
    { 
        protected void CanvasGroupToggle(CanvasGroup canvasGroup, bool active)
        {
            canvasGroup.alpha = active ? 1 : 0;
            canvasGroup.blocksRaycasts = active;
        }

        protected async void CanvasGroupToggleToward(CanvasGroup canvasGroup, bool active, float maxDelta, CancellationToken cancellationToken)
        {
            float targetAlpha = active ? 1 : 0;
            while (canvasGroup.alpha != targetAlpha) {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, Time.deltaTime * maxDelta);

                try {
                    await Task.Delay(1, cancellationToken);
                }
                catch (TaskCanceledException e) {
                    Debug.Log($"[{e.StackTrace}]\n{e.Message}\nTask Caneled");
                }
            }
            canvasGroup.blocksRaycasts = active;
        }
    }
}

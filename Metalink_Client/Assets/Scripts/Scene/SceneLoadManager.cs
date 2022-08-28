using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

namespace Scene
{
    public class SceneLoadManager : MonoBehaviour
    {
        private static string nextSceneName;

        [SerializeField] private RectTransform progressBar;

        public void Start() => LoadSceneProgress();

        public static void LoadScene(string sceneName)
        {
            nextSceneName = sceneName;
            SceneManager.LoadScene("LoadingScene");
        }

        public async void LoadSceneProgress()
        {
            var operation = SceneManager.LoadSceneAsync(nextSceneName);
            operation.allowSceneActivation = false;

            float timer = 0;
            while (!operation.isDone) {
                await Task.Delay(1);

                if(operation.progress < 0.9f) {
                    progressBar.anchorMax = new Vector2(operation.progress, 1);
                }
                else {
                    timer += Time.unscaledDeltaTime;
                    float lerpedAnchor = Mathf.Lerp(0.9f, 1f, timer);
                    progressBar.anchorMax = new Vector2(lerpedAnchor, 1);

                    if(progressBar.anchorMax.x >= 1f) {
                        await Task.Delay(1000);
                        operation.allowSceneActivation = true;
                        return;
                    }
                }

                progressBar.offsetMax = new Vector2(0, 0);
            }
        }
    }
}

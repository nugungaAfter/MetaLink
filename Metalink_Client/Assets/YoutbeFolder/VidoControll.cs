using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
namespace YoutubePlayer
{
    public class VidoControll : MonoBehaviour
    {
        public YoutubePlayer youtubePlayer;
        VideoPlayer videoPlayer;
        public Button bt_Play;
        public Button bt_Pause;
        public Button bt_Reset;
        public Text urlText;

        private void Awake()
        {
            bt_Play.interactable = false;
            bt_Pause.interactable = false;
            bt_Reset.interactable = false;
            videoPlayer = youtubePlayer.GetComponent<VideoPlayer>();
            videoPlayer.prepareCompleted += VideoPlayerPreparedCompleted;
        }

        void VideoPlayerPreparedCompleted(VideoPlayer sourse)
        {
            bt_Play.interactable = sourse.isPrepared;
            bt_Pause.interactable = sourse.isPrepared;
            bt_Reset.interactable = sourse.isPrepared;
        }

        public async void Prepare()
        {
            Debug.Log("cargando video..");
            youtubePlayer.youtubeUrl = urlText.text;
            try
            {
                await youtubePlayer.PrepareVideoAsync();
                Debug.Log("video cargado");
            }
            catch
            {
                Debug.Log("ERROR video  no cargado");
            }
        }
        public void PlayVideo()
        {
            videoPlayer.Play();
        }
        public void PauseVideo()
        {
            videoPlayer.Pause();
        }
        public void ResetVideo()
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }

        private void OnDestroy()
        {
            videoPlayer.prepareCompleted -= VideoPlayerPreparedCompleted;
        }
    }

}


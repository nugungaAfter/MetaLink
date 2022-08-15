using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System;

using UnityEngine;
using UnityEditor;

using TMPro;

using Metalink.Object;
using Metalink.BackEnd;

namespace Metalink.UI.Content
{
    public class UI_Content_Upload : MonoBehaviour
    {
        [SerializeField] private TMP_InputField m_UserName;
        [SerializeField] private TMP_InputField m_UploadPath;
        [SerializeField] private RectTransform m_ProgressBar;
        [SerializeField] private GameObject m_ObjectSeleterBar;
        [SerializeField] private Transform m_ContentList;
        [SerializeField] private Transform m_UploadRoot;

        private List<UploadObject> m_ObjectList = new List<UploadObject>();

        // 업로드할 오브젝트 목록 업데이트
        public void Refresh()
        {
            m_ObjectList.Clear();

            foreach (Transform child in m_ContentList) {
                Destroy(child.gameObject);
            }

            foreach (Transform child in m_UploadRoot) {
                var l_UploadObject = new UploadObject(child.gameObject);
                m_ObjectList.Add(l_UploadObject);

                // 오브젝트 선택 UI 생성 및 초기화
                UI_ContentUploader_ObjectSeleter l_ObjectSelecter =
                    Instantiate(m_ObjectSeleterBar, m_ContentList).GetComponent<UI_ContentUploader_ObjectSeleter>();
                l_ObjectSelecter.SetUI(l_UploadObject);
            }
        }

        //콘텐츠를 업로드함.
        public void UploadContent()
        {
            StartCoroutine(
            ObjectLoader.UploadWorldCO(
                m_ObjectList.FindAll(x => x.m_IsUpload == true),
                m_UserName.text,
                gameObject.scene.name,
                m_UploadPath.text,
                UpdateProgressBar
                )
            );
        }

        // 업로드 진행도 갱신 콜백 함수
        public void UpdateProgressBar(float p_Progress, int p_Succes, int p_Failed)
        {
            m_ProgressBar.anchorMax = new Vector2(p_Progress, 1);
            m_ProgressBar.offsetMax = Vector2.zero;
        }
    }
}
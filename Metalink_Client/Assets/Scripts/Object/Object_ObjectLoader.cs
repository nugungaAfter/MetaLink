using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System;

using UnityEditor.Formats.Fbx.Exporter;
using UnityEngine.Events;
using UnityEngine;
using UnityEditor;

using Newtonsoft.Json;
using Metalink.BackEnd;
using Metalink.Utility;

namespace Metalink.Object
{
    [System.Serializable]
    public struct TransfromData
    {
        public float[] m_Position;
        public float[] m_Rotation;
        public float[] m_Scale;

        public TransfromData(GameObject p_GameObject)
        {
            m_Position = new float[]{ p_GameObject.transform.position.x,  p_GameObject.transform.position.y, p_GameObject.transform.position.z };
            m_Rotation = new float[]{ p_GameObject.transform.rotation.x,  p_GameObject.transform.rotation.y, p_GameObject.transform.rotation.z };
            m_Scale = new float[]{ p_GameObject.transform.localScale.x,  p_GameObject.transform.localScale.y, p_GameObject.transform.localScale.z };
        }

        public void SetTransform(Transform p_Transform)
        {
            p_Transform.position = new Vector3(m_Position[0], m_Position[1], m_Position[2]);
            p_Transform.rotation = Quaternion.Euler(m_Rotation[0], m_Rotation[1], m_Rotation[2]);
            //p_Transform.localScale = new Vector3(m_Scale[0], m_Scale[1], m_Scale[2]);
        }

        public override string ToString()
        {
            Vector3 postion = new Vector3(m_Position[0], m_Position[1], m_Position[2]);
            Vector3 rotation = new Vector3(m_Rotation[0], m_Rotation[1], m_Rotation[2]);
            Vector3 scale = new Vector3(m_Scale[0], m_Scale[1], m_Scale[2]);
            return postion.ToString() + "\n" + rotation.ToString() + "\n" + scale.ToString();
        }
    }

    [System.Serializable]
    public struct ResourceData
    {
        public string m_Type;
        public string[] m_Script;
        public string m_Name;
        public string m_Src;
        public string m_JsonData;

        public ResourceData(UploadObject p_UploadObject, string p_Directory)
        {
            m_Type = "GameObject";
            m_Name = p_UploadObject.m_Name;
            m_Src = p_Directory;
            m_Script = new string[0];
            m_JsonData = "";
        }

        public override string ToString() => $"type : {m_Type}, script : {m_Script}, name : {m_Name}, src : {m_Src}, m_JsonData : {m_JsonData}";
    }

    [System.Serializable]
    public class ObjectLoadData
    {
        public Dictionary<string, TransfromData> m_Positions = new Dictionary<string, TransfromData>();
        public Dictionary<string, ResourceData> m_Resources = new Dictionary<string, ResourceData>();
        public string m_RowData;
    }

    /// <summary>
    /// 업로드할 오브젝트 데이터
    /// </summary>
    [System.Serializable]
    public class UploadObject
    {
        public string m_Name;
        public bool m_IsUpload;
        public GameObject m_Object;

        /// <summary>
        /// 업로드할 오브젝트 데이터 생성
        /// </summary>
        /// <param name="p_ReferenceObject">참조할 오브젝트</param>
        public UploadObject(GameObject p_ReferenceObject)
        {
            m_Name = p_ReferenceObject.name;
            m_Object = p_ReferenceObject;
            m_IsUpload = p_ReferenceObject.activeSelf;
        }
    }

    public class ObjectLoader : MonoBehaviour
    {
        /// <summary>
        /// 임시경로 참조값
        /// </summary>
        public static string TempFolder
        {
            get
            {
                string l_temp = FileUtil.GetUniqueTempPathInProject();
                if (!Directory.Exists(l_temp)) {
                    Directory.CreateDirectory(l_temp);
                }
                return l_temp;
            }
        }

        #region Upload
        /// <summary>
        /// 월드 업로드
        /// </summary>
        /// <param name="p_UploadObjects">업로드할 오브젝트 리스트</param>
        /// <param name="p_UserName">유저 이름</param>
        /// <param name="p_WorldName">업로드할 월드 이름</param>
        /// <param name="p_UploadPath">업로드할 경로</param>
        /// <param name="p_ProgressUpdateCallback">진행도 변경시 업데이트 되는 콜백함수, 매개변수는 (float: 진행도(0.0 ~ 1.0), int: 성공, int: 실패) </param>
        /// <returns></returns>
        public static IEnumerator UploadWorldCO(List<UploadObject> p_UploadObjects, string p_UserName, string p_WorldName, string p_UploadPath, UnityAction<float, int, int> p_ProgressUpdateCallback = null)
        {
            int l_success = 0, l_failed = 0, l_total = p_UploadObjects.Count;

            FileManager.DeleteDirectory("content", p_UserName + "/" + p_UploadPath);

            ObjectLoadData l_objectLoadData = new ObjectLoadData();
            foreach (UploadObject l_uploadObject in p_UploadObjects) {
                if (!l_uploadObject.m_IsUpload)
                    continue;

                try {
                    string l_directory = UploadObject(p_UserName, p_UploadPath, l_uploadObject);

                    string id = $"GameObject-{l_uploadObject.m_Name}";
                    l_objectLoadData.m_Positions.Add(id, new TransfromData(l_uploadObject.m_Object));
                    l_objectLoadData.m_Resources.Add(id, new ResourceData(l_uploadObject, l_directory));

                    l_success++;
                }
                catch {
                    l_failed++;
                }

                float progress = (float)(l_success + l_failed) / l_total;
                p_ProgressUpdateCallback?.Invoke(progress, l_success, l_failed);

                yield return null;
            }

            UploadWorldData(p_UserName, p_WorldName, l_objectLoadData);
        }

        private static string UploadObject(string p_UserName, string p_UploadPath, UploadObject p_UploadObject)
        {
            string l_localTemp = TempFolder;
            string l_filePath = Path.Combine(l_localTemp, p_UploadObject.m_Name);
            ModelExporter.ExportObject(l_filePath, p_UploadObject.m_Object);

            string[] l_files = Directory.GetFiles(l_localTemp, "*.fbx");
            FileInfo l_fileInfo = new FileInfo(l_files[0]);

            string l_directory = p_UserName + "/" + p_UploadPath;
            
            try {
                FileManager.FileUploadSFTP("content", l_directory, p_UploadObject.m_Name + ".fbx", l_fileInfo.FullName);
            }
            catch (Exception e) {
                Debug.LogException(e);
                throw e;
            }
            finally {
                Directory.Delete(l_fileInfo.DirectoryName, true);
            }

            return l_directory + "/" + l_fileInfo.Name;
        }

        private static void UploadWorldData(string p_UserName, string p_WorldName, ObjectLoadData p_ObjectLoadData)
        {
            string l_localTemp = TempFolder;
            string l_jsonData = JsonConvert.SerializeObject(p_ObjectLoadData);
            string l_filePath = l_localTemp + $"/{p_WorldName}_Data.json";

            StreamWriter l_textWrite = File.CreateText(l_filePath); //생성
            l_textWrite.WriteLine(l_jsonData);
            l_textWrite.Dispose();

            FileInfo l_fileInfo = new FileInfo(l_filePath);
            try {
                FileManager.FileUploadSFTP("worlds", p_UserName, l_fileInfo.Name, l_fileInfo.FullName);
            }
            catch (Exception e) {
                throw e;
            }
            finally {
                Directory.Delete(l_fileInfo.DirectoryName, true);
            }
        }
        #endregion

        #region Download
        /// <summary>
        /// 월드 다운로드
        /// </summary>
        /// <param name="p_UserName">유저 이름</param>
        /// <param name="p_WorldName">다운로드할 월드 이름</param>
        /// <param name="p_ProgressUpdateCallback">진행도 변경시 업데이트 되는 콜백함수, 매개변수는 (float: 진행도(0.0 ~ 1.0), int: 성공, int: 실패)</param>
        /// <returns></returns>
        public static IEnumerator DownloadWorldCO(string p_UserName, string p_WorldName, UnityAction<float, int, int> p_ProgressUpdateCallback = null)
        {
            var l_objectLoadData = DownloadWorldData(p_UserName, p_WorldName);
            int l_success = 0, l_failed = 0, l_total = l_objectLoadData.m_Positions.Count;

            foreach (var l_transformData in l_objectLoadData.m_Positions) {
                try {
                    var l_resource = l_objectLoadData.m_Resources[l_transformData.Key];
                    var l_loadObject = DownloadObject(l_resource.m_Src);

                    l_loadObject.name = l_resource.m_Name;
                    l_transformData.Value.SetTransform(l_loadObject.transform);

                    l_success++;
                }
                catch {
                    l_failed++;
                }

                float progress = (float)(l_success + l_failed) / l_total;
                p_ProgressUpdateCallback?.Invoke(progress, l_success, l_failed);

                yield return null;
            }
        }

        private static GameObject DownloadObject(string p_DownloadPath)
        {
            FileInfo l_downloadFileInfo = new FileInfo(p_DownloadPath);
            string l_filePath = TempFolder + $"/{l_downloadFileInfo.Name}";
            GameObject l_loadObject = new GameObject(l_downloadFileInfo.Name);

            try {
                FileManager.FileDownloadSFTP("content", p_DownloadPath.Replace(l_downloadFileInfo.Name, ""), l_downloadFileInfo.Name, l_filePath);
                FBXImporter.LoadFBX(l_loadObject, l_filePath);
            }
            catch (Exception e) {
                throw e;
            }

            return l_loadObject;
        }

        private static ObjectLoadData DownloadWorldData(string p_UserName, string p_WorldName)
        {
            string l_filePath = TempFolder + $"/{p_WorldName}_Data.json";
            string l_jsonData = "";

            FileInfo l_fileInfo = new FileInfo(l_filePath);
            try {
                FileManager.FileDownloadSFTP("worlds", p_UserName, p_WorldName + "_Data.json", l_fileInfo.FullName);
                l_jsonData = File.ReadAllText(l_filePath);
            }
            catch (Exception e) {
                throw e;
            }
            finally {
                Directory.Delete(l_fileInfo.DirectoryName, true);
            }

            return JsonConvert.DeserializeObject<ObjectLoadData>(l_jsonData);
        }
        #endregion
    }
}
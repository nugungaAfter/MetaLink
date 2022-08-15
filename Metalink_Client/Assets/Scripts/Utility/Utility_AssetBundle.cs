using System.Threading.Tasks;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Metalink.Utility
{
    public class Utility_AssetBundle
    {
        public void BuildBundle()
        {
            string assetBundleDirectory = "Assets/AssetBundle";
            if (!Directory.Exists(assetBundleDirectory))
                Directory.CreateDirectory(assetBundleDirectory);

            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows); //번들 제작
            AssetDatabase.Refresh(); //Asset 리프레쉬
        }

        /* 오브젝트를 번들로부터 생성하는 코루틴 함수 */
        public async Task<GameObject[]> InstantiateObject(string p_ServerPath, string p_LocalPath, string p_BundleName)
        {
            if (!File.Exists(p_LocalPath + p_BundleName)) //번들이 로컬에 존재하지 않으면 => 로컬에 번들 다운로드
            {
                if (!Directory.Exists(p_LocalPath)) //폴더가 존재하지 않으면
                {
                    Directory.CreateDirectory(p_LocalPath); //폴더 생성
                }

                UnityWebRequest I_request = UnityWebRequest.Get(p_ServerPath + "/" + p_BundleName); //서버로부터 번들 요청 생성

                await Task.Run(() => I_request.SendWebRequest()); //요청이 완료될 때까지 대기

                File.WriteAllBytes(p_LocalPath + "/" + p_BundleName, I_request.downloadHandler.data); //파일 입출력으로 서버의 번들을 로컬에 저장
                AssetDatabase.Refresh(); //Asset 리프레쉬
            }

            var I_bundle = AssetBundle.LoadFromFile(p_LocalPath + "/" + p_BundleName); //로컬로부터 번들 로드

            GameObject[] I_assets = I_bundle.LoadAllAssets<GameObject>(); //번들에서 모든 에셋 로드

            I_bundle.Unload(false); //true이면 에셋번들 언로드

            return I_assets;
        }
    }
}

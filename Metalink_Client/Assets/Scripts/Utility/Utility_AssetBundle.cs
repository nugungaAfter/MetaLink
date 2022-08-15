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

            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows); //���� ����
            AssetDatabase.Refresh(); //Asset ��������
        }

        /* ������Ʈ�� ����κ��� �����ϴ� �ڷ�ƾ �Լ� */
        public async Task<GameObject[]> InstantiateObject(string p_ServerPath, string p_LocalPath, string p_BundleName)
        {
            if (!File.Exists(p_LocalPath + p_BundleName)) //������ ���ÿ� �������� ������ => ���ÿ� ���� �ٿ�ε�
            {
                if (!Directory.Exists(p_LocalPath)) //������ �������� ������
                {
                    Directory.CreateDirectory(p_LocalPath); //���� ����
                }

                UnityWebRequest I_request = UnityWebRequest.Get(p_ServerPath + "/" + p_BundleName); //�����κ��� ���� ��û ����

                await Task.Run(() => I_request.SendWebRequest()); //��û�� �Ϸ�� ������ ���

                File.WriteAllBytes(p_LocalPath + "/" + p_BundleName, I_request.downloadHandler.data); //���� ��������� ������ ������ ���ÿ� ����
                AssetDatabase.Refresh(); //Asset ��������
            }

            var I_bundle = AssetBundle.LoadFromFile(p_LocalPath + "/" + p_BundleName); //���÷κ��� ���� �ε�

            GameObject[] I_assets = I_bundle.LoadAllAssets<GameObject>(); //���鿡�� ��� ���� �ε�

            I_bundle.Unload(false); //true�̸� ���¹��� ��ε�

            return I_assets;
        }
    }
}

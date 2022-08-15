using TriLibCore;
using UnityEngine;
using System.Threading.Tasks;
using System;

namespace Metalink.Utility
{
    public class FBXImporter : MonoBehaviour
    {
        public static void LoadFBX(GameObject p_WapperGameObject, string p_FilePath)
        {
            AssetLoaderContext l_context =
                    AssetLoader.LoadModelFromFile(
                    p_FilePath,
                    onLoad: OnLoad,
                    onMaterialsLoad: OnMaterialsLoad,
                    onProgress: OnProccess,
                    onError: OnError,
                    wrapperGameObject: p_WapperGameObject
                    );
        }

        public static void OnLoad(AssetLoaderContext p_Context)
        {
            print("¸ðµ¨ ·ÎµùµÊ");
        }

        public static void OnMaterialsLoad(AssetLoaderContext p_Context)
        {
            print("¸ÞÅÍ¸®¾ó ·ÎµùµÊ");
        }

        public static void OnProccess(AssetLoaderContext p_Context, float p_Progress)
        {
            print($"ÁøÇàµµ: {p_Progress}");
        }

        public static void OnError(IContextualizedError p_Error)
        {
            print(p_Error.GetInnerException().Message);
        }
    }
}

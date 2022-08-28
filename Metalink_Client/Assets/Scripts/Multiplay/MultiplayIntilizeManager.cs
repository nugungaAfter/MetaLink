using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

namespace Multiplay
{
    public class MultiplayIntilizeManager : MonoBehaviour
    {
        public void Awake()
        {
            Initlize();
        }

        public void Initlize()
        {
            var bro = Backend.Initialize(true);
            if (bro.IsSuccess()) {
                Debug.Log("서버 초기화 성공");
            }
            else {
                Debug.LogError("서버 초기화 실패");
            }
        }
    }
}

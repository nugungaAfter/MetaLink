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
                Debug.Log("���� �ʱ�ȭ ����");
            }
            else {
                Debug.LogError("���� �ʱ�ȭ ����");
            }
        }
    }
}

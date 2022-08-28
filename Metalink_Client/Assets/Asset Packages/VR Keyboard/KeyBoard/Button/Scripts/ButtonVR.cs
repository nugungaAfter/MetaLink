/**************************************************
Copyright : Copyright (c) RealaryVR. All rights reserved.
Description: Script for VR Button functionality.
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Matalink.Keybord
{

    public class ButtonVR : MonoBehaviour
    {
        public GameObject g_button;
        public UnityEvent g_onPress;
        public UnityEvent g_onRelease;
        GameObject g_presser;
        AudioSource g_sound;
        bool g_isPressed;

        void Start()
        {
            g_sound = GetComponent<AudioSource>();
            g_isPressed = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!g_isPressed)
            {
                g_button.transform.localPosition = new Vector3(0, 0.003f, 0);
                g_presser = other.gameObject;
                g_onPress.Invoke();
                g_sound.Play();
                g_isPressed = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == g_presser)
            {
                g_button.transform.localPosition = new Vector3(0, 0.015f, 0);
                g_onRelease.Invoke();
                g_isPressed = false;
            }
        }

        public void SpawnSphere()
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            sphere.transform.localPosition = new Vector3(0, 1, 2);
            sphere.AddComponent<Rigidbody>();
        }

    }
}

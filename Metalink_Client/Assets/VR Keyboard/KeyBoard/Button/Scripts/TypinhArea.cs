using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matalink.Keybord
{

    public class TypinhArea : MonoBehaviour
    {
        public GameObject leftHand;
        public GameObject RightHand;
        public GameObject leftTypingHand;
        public GameObject RightTypingHand;

        private void OnTriggerEnter(Collider other)
        {
            GameObject hand = other.GetComponentInParent<CharacterController>().gameObject;
            if (hand == null) return;
            if (hand == leftHand)
            {
                leftTypingHand.SetActive(true);
            }
            else if(hand == RightHand)
            {
                RightTypingHand.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            GameObject hand = other.GetComponentInParent<CharacterController>().gameObject;
            if (hand == null) return;
            if (hand == leftHand)
            {
                leftTypingHand.SetActive(false);
            }
            else if (hand == RightHand)
            {
                RightTypingHand.SetActive(false);
            }
        }
    }
}

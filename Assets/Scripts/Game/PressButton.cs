using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(AudioSource))]
    public class PressButton : MonoBehaviour
    {
        public static PressButton instance;

        private AudioSource _audioSource;
        [SerializeField]
        private AudioClip[] _audioClips;

        public Action buttonPressed;
        [SerializeField]
        private GameObject _pressedGO;
        [SerializeField]
        private GameObject _nonPressedGO;

        private void Awake() 
        {
            if (instance == null) instance = this;
            _audioSource = GetComponent<AudioSource>();
        }

        public void Press()
        {
            Debug.Log("pressed");
            _audioSource.clip = _audioClips[0];
            _audioSource.Play();
            _pressedGO.SetActive(true);
            _nonPressedGO.SetActive(false);
            buttonPressed?.Invoke();
        }

        public void UnPress()
        {
            Debug.Log("unpressed");
            _audioSource.clip = _audioClips[1];
            _audioSource.Play();
            _pressedGO.SetActive(false);
            _nonPressedGO.SetActive(true);
        }
    }
}
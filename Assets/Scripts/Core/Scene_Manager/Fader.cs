using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Island.Scene_Manager
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup _canvasGroup;
        [SerializeField]
        private float _fadeTime;
        private float _alphaChange;
        // Start is called before the first frame update
        void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                Debug.LogError("CanvasGroup is NULL.");
            }

            _alphaChange = Time.deltaTime / _fadeTime;
        }


        public IEnumerator FadeOut()
        {         
            while (_canvasGroup.alpha < 1.0f)
            {
                _canvasGroup.alpha += _alphaChange;
                yield return null;
            }
        }

        public IEnumerator FadeIn()
        {
            while (_canvasGroup.alpha > 0.0f)
            {
                _canvasGroup.alpha -= _alphaChange;
                yield return null;
            }
        }
    }
}

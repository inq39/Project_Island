using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Island.Cinematics
{  
    public class CinematicTrigger : MonoBehaviour
    {
        private PlayableDirector _playable;
        private bool _isTriggered = false;
        private void Awake()
        {
            _playable = GetComponent<PlayableDirector>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !_isTriggered)
            _playable.Play();
            _isTriggered = true;
        }
    }
}

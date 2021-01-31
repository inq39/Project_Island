using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Island.Core;
using UnityEngine.Playables;
using Island.Controller;

namespace Island.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private GameObject _player;
        private PlayableDirector _playableDirector;
        private void Start()
        {
            _playableDirector = GetComponent<PlayableDirector>();
            _player = GameObject.FindWithTag("Player");
            _playableDirector.played += DisableControl;
            _playableDirector.stopped += EnableControl;
        }

        void EnableControl(PlayableDirector pd)
        {
            _player.GetComponent<PlayerController>().enabled = true;
        }

        void DisableControl(PlayableDirector pd)
        {
            _player.GetComponent<PlayerController>().enabled = false;
            _player.GetComponent<ActionScheduler>().CancelAction();
        }
    }
}

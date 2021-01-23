using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Island.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform _player;


        void LateUpdate()
        {
            transform.position = _player.position;
        }
    }
}

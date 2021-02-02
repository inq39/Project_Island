using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace Island.Scene_Manager
{
    public class Portal : MonoBehaviour
    {
        [SerializeField]
        private int _sceneToLoad;
        public Transform _spawnPoint;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine("Transition");
            }
        }

        IEnumerator Transition()
        {
            DontDestroyOnLoad(this);
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }
            Debug.Log("Scene loaded!");
            UpdatePlayer(GetOtherPortal());
            Destroy(this);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player is NULL.");
            }
            player.GetComponent<NavMeshAgent>().Warp(otherPortal._spawnPoint.transform.position);
            player.transform.rotation = otherPortal._spawnPoint.transform.rotation;
        }

        private Portal GetOtherPortal()
        {
            var portals = FindObjectsOfType<Portal>();
            foreach (var portal in portals)
            {
                if (portal == this) continue;
                return portal;
            }
            return null;
        }
    }
}

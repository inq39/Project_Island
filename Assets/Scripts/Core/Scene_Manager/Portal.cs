using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace Island.Scene_Manager
{
    enum PortalIdentifier
    {
        A, B, C, D, E
    }
    public class Portal : MonoBehaviour
    {
        [SerializeField]
        private int _sceneToLoad;
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private PortalIdentifier _portal;
        [SerializeField]
        private float _fadeWaitTime;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            { 
                StartCoroutine("Transition");
            }
        }

        IEnumerator Transition()
        {
            Fader fader = GameObject.FindWithTag("Fader").GetComponent<Fader>();
            yield return fader.FadeOut();
            DontDestroyOnLoad(this);
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }
            Debug.Log("Scene loaded!");
            UpdatePlayer(GetOtherPortal());
            yield return new WaitForSeconds(_fadeWaitTime);
            yield return fader.FadeIn();
            Destroy(gameObject);
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
                else if (portal._portal == this._portal)
                {
                    return portal;
                }               
            }
            return null;
        }
    }
    
}

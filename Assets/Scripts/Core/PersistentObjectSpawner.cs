using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Island.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        GameObject _persistentObjectPrefab;
        static bool _hasSpawned = false;

        // Start is called before the first frame update
        private void Awake()
        {
            if (_hasSpawned) return;

            SpawnPersistentObject();

            _hasSpawned = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(_persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }

    }
}

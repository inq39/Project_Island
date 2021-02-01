using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Island.Scene_Manager
{
    public class Portal : MonoBehaviour
    {
       
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                SceneManager.LoadScene("Prototype2_Island");
            }
        }
    }
}

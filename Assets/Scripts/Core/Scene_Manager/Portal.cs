using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Island.Manager
{
    public class SceneController : MonoBehaviour
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

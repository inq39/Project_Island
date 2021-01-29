using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Island.Controller
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, 0.25f);              
               
                if (i+1 <  transform.childCount)
                { 
                    Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
                }
                else
                {
                    Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(0).position);
                }
            }
        }
    }
}

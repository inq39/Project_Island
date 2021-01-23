using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Island.Movement;

namespace Island.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        // Start is called before the first frame update
        void Start()
        {
            _mover = GetComponent<Mover>();
            if (_mover == null)
            {
                Debug.LogError("Mover is NULL.");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out hit, Mathf.Infinity);

            if (hasHit && hit.collider.name == "Terrain")
            {
                _mover.MoveTo(hit.point);
            }

        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Island.Movement;
using System;
using Island.Combat;

namespace Island.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Fighter _fighter;
        // Start is called before the first frame update
        void Start()
        {
            _mover = GetComponent<Mover>();
            if (_mover == null)
            {
                Debug.LogError("Mover is NULL.");
            }

            _fighter = GetComponent<Fighter>();
            if (_fighter == null)
            {
                Debug.LogError("Fighter is NULL.");
            }
        }

        // Update is called once per frame
        void Update()
        {
            InteractWithMovement();
            InteractWithCombat();
        }

        private void InteractWithCombat()
        {
            if (Input.GetMouseButtonDown(0))
            {
                AttackEnemy();
            }
        }

        private void AttackEnemy()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.name == "Enemy")
                {
                    CombatTarget target = hit.collider.GetComponent<CombatTarget>();
                    _fighter.Attack(target);
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit, Mathf.Infinity);

            if (hasHit && hit.collider.name == "Terrain")
            {
                _mover.MoveTo(hit.point);
            }    
        }
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
    
}

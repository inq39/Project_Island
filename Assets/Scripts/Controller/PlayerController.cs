using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Island.Movement;
using System;
using Island.Combat;
using Island.Core;

namespace Island.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Fighter _fighter;
        private Health _health;
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

            _health = GetComponent<Health>();
            if (_health == null)
            {
                Debug.Log("Health is NULL.");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_health.IsDead()) { return; }
            if (InteractWithCombat() == true) return;
            if (InteractWithMovement() == true) return;
        }

        private bool InteractWithCombat()
        {      
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.collider.GetComponent<CombatTarget>();
                if (target == null) { continue; }


                if (!_fighter.CanAttack(target.gameObject)) { continue; }           
                
                if (Input.GetMouseButton(0))
                {
                    _fighter.Attack(target.gameObject);
                }                                 
                return true;          
            }                      
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    _mover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
    
}

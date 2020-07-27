using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.Scripts.Interactables
{
    [RequireComponent(typeof(Collider))]
    public class Interactable3D : Interactable
    {
        Collider collider;

        protected void Start()
        {
            collider = GetComponent<Collider>();
            collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            StateController controller = other.GetComponentInParent<StateController>();
            string tag = other.tag;
            if (targetTags.Count == 0 || targetTags.Contains(tag))
            {
                Interact(other.tag, controller);
            }

        }

    }
}

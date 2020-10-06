using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.Scripts.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public class Interactable2D : Interactable
    {
        Collider2D collider;


        protected void Start()
        {
            collider = GetComponent<Collider2D>();
            collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
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

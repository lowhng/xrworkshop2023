﻿using System;
using UnityEngine;
using UnityEngine.Events;
using static InteractionManager;

namespace Assets.XRWorkshop_Test.Scripts
{
    /// <summary>
    /// The interactable interface is the object contract for assets placed in the game world that react to the player touching them with tools.
    /// </summary>
    public abstract class ForestInteractable : MonoBehaviour
    {
        public UnityEvent OnInteractStart;

        public UnityEvent OnInteractComplete;

        // A public method called getInteractionType that returns a Tool_interactions
        public abstract Tool_interactions getInteractionType();

        /// <summary>
        /// Flag to remember if this forest interactable has already been interacted with.
        /// Interactions are one time occurances.
        /// </summary>
        private bool done = false;

        /// <summary>
        /// This function is called when the interaction with this interactable is allowed to begin.
        /// The designer must queue up events to fire the interaction starts using OnInteractStart in the inspector.
        /// </summary>
        /// <param name="tool"></param>
        /// <param name="_lState"></param>
        /// <param name="_wState"></param>
        public void TryInteract(Tool_interactions tool, landState _lState, waterState _wState) {
            if (!done && tool == getInteractionType()) {
                if (_lState.Invoke(this) && _wState.Invoke(this)) {
                    OnInteractStart.Invoke();
                    done = true;
                }
            } 
        }
    }
}

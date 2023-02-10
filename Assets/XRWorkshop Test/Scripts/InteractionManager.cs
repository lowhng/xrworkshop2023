using Assets.XRWorkshop_Test.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{

    private int treesToPlant;

    private int watersToClean;

    private int pestsToRemove;

    private int trashToRemove;

    public List<ForestInteractable> _interactables { private set; get; }

    public landState _landState = DegradersRemaining; // Assumes at least one trash / pest to remove

    public waterState _waterState = NotClean;

    /// <summary>
    /// These Unity events will be triggered when the forest has been restored.
    /// </summary>
    public UnityEvent OnInteractionsComplete;

    // Start is called before the first frame update
    void Start()
    {
        _interactables = new List<ForestInteractable>(FindObjectsOfType<MonoBehaviour>().OfType<ForestInteractable>());
        foreach (ForestInteractable interactable in _interactables)
        {
            // This is poor coding practice, but we're working quick and dirty here.
            switch(interactable.getInteractionType())
            {
                case Tool_interactions.plant_tree:
                    treesToPlant++;
                    break;
                case Tool_interactions.clean_water:
                    watersToClean++;
                    break;
                case Tool_interactions.pest_removal:
                    pestsToRemove++;
                    break;
                case Tool_interactions.trash_removal:
                    trashToRemove++;
                    break;
                default:
                    break;
            }   
        }
        // Not sure how to remove hard coded dependency to events, or if that is considered a problem at this stage.
        // Certainly, adding more interactables in the future should require the modification of the InteractionManager class.
        Pest.PestRemoved += PestRemovedObserver;
        Trash.TrashRemoved += TrashRemovedObserver;
        Tree.TreeReplanted += TreeReplantedObserver;
        Water.WaterCleaned += WaterCleanedObserver;
        // log the number of trees to plant
        Debug.Log("Trees to plant: " + treesToPlant);
        // log the number of waters to clean
        Debug.Log("Waters to clean: " + watersToClean);
        // log the number of pests to remove
        Debug.Log("Pests to remove: " + pestsToRemove);
        // log the number of trash to remove
        Debug.Log("Trash to remove: " + trashToRemove);
    }

    private void TrashRemovedObserver()
    {
        trashToRemove--;
        if (trashToRemove <= 0 && pestsToRemove <= 0)
        {
            _landState = AllDegradersRemoved;
            Debug.Log("all degraders removed.");
        }
    }

    private void WaterCleanedObserver()
    {
        watersToClean--;
        if (watersToClean <= 0)
        {
            _waterState = Clean;
            Debug.Log("all water is clean.");
        }
    }

    private void TreeReplantedObserver()
    {
        treesToPlant--;
        if (treesToPlant <= 0)
        {
            _landState = AllTreesPlanted;
            Debug.Log("All interactions complete, invoking all interactions complete.");
            OnInteractionsComplete.Invoke();
        }
    }

    private void PestRemovedObserver()
    {
        pestsToRemove--;
        if (pestsToRemove <= 0 && trashToRemove <= 0)
        {
            Debug.Log("All degraders removed.");
            _landState = AllDegradersRemoved;
        }
    }

    private void OnDestroy()
    {
        // unsubscribe from pest removed event
        Pest.PestRemoved -= PestRemovedObserver;
        // unsubscribe from trash removed event
        Trash.TrashRemoved -= TrashRemovedObserver;
        // unsubscribe from tree replanted event
        Tree.TreeReplanted -= TreeReplantedObserver;
        // unsubscribe from water cleaned event
        Water.WaterCleaned -= WaterCleanedObserver;
    }

    private bool ShouldChangeScale() {
        return _landState == AllTreesPlanted && _waterState == Clean;
    }

    // A delegate called landState that takes a IIinteractable object and returns a boolean
    /// <summary>
    /// Determine if the interactable object can activate it's interaction.
    /// </summary>
    /// <param name="interactable"></param>
    /// <returns></returns>
    public delegate bool landState(ForestInteractable interactable);

    // A delegate called waterState that takes a IIinteractable object and returns a boolean
    /// <summary>
    /// Determine if the interactable object can activate it's interaction.
    /// </summary>
    /// <param name="interactable">The object checking if it is OK to be interacted with.</param>
    /// <returns>True if the object can be interacted with.</returns>
    public delegate bool waterState(ForestInteractable interactable);

    public static landState DegradersRemaining = (ForestInteractable interactable) => {
        if (interactable.getInteractionType() == Tool_interactions.plant_tree) {
            return false;
        }
        return true; 
    };

    public static landState AllDegradersRemoved = (ForestInteractable interactable) => { 
        return true; 
    };

    public static landState AllTreesPlanted = (ForestInteractable interactable) => { 
        return true; 
    };

    public static waterState NotClean = (ForestInteractable interactable) => { 
        return true; 
    };

    public waterState Clean = (ForestInteractable interactable) => { 
        return true; 
    };
    
}

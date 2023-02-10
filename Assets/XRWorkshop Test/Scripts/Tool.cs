using Assets.XRWorkshop_Test.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// The Tool script attatches to the VR hands.
/// </summary>
public class Tool : MonoBehaviour
{
    public static Tool_interactions _currentTool = Tool_interactions.none;

    public static readonly int tool_layer = 6;

    [SerializeField]
    private InteractionManager manager;

    private ToolSelector currentToolSelector;

    private void Start()
    {
        // Tell the physics system to make tool layer ignore tool layer.
        Physics.IgnoreLayerCollision(tool_layer, tool_layer);
    }

    private void OnTriggerEnter(Collider other)
    {
        // test if other has an ForestInteractionScript
        if (other.TryGetComponent<ForestInteractable>(out ForestInteractable sceneActor))
        {
            Assert.IsNotNull(sceneActor, "Scene actor is null");
            Assert.IsNotNull(manager._landState, "Land state is null");
            Assert.IsNotNull(manager._waterState, "Water state is null");
            sceneActor.TryInteract(_currentTool, manager._landState, manager._waterState);
            return;
        }
        if (other.TryGetComponent<ToolSelector>(out ToolSelector selector)) {
            if (_currentTool == selector.tool) {
                selector.DeselectTool();
                currentToolSelector = null;
                _currentTool = Tool_interactions.none;
            } else {
                selector.SelectTool();
                if (currentToolSelector != null) { 
                    currentToolSelector.DeselectTool();
                }
                currentToolSelector = selector;
                _currentTool = selector.tool;
                return;
            }
        }
    }
}

public enum Tool_interactions { 
    plant_tree,
    clean_water,
    pest_removal,
    trash_removal,
    none
}

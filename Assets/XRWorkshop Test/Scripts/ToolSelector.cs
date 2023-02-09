using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Tool Selector attatches to the Tool selection game objects that have the icons.
/// </summary>
public class ToolSelector : MonoBehaviour
{
    public UnityEvent OnToolSelected;

    public UnityEvent OnToolDeselected;
    
    public Tool_interactions tool = Tool_interactions.none;

    public void SelectTool()
    {
        OnToolSelected.Invoke();
    }

    public void DeselectTool()
    {
        OnToolDeselected.Invoke();
    }
}

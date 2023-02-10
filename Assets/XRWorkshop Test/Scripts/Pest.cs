using Assets.XRWorkshop_Test.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pest : ForestInteractable
{
    public static event Action PestRemoved;

    [SerializeField]
    Navigatable _navigatable;

    public void Start()
    {
        _navigatable.NavigationComplete += RemovePest;
    }

    public void OnDestroy()
    {
        _navigatable.NavigationComplete -= RemovePest;
    }

    public void RemovePest() {
        PestRemoved?.Invoke();
        OnInteractComplete.Invoke();
    }

    public override Tool_interactions getInteractionType()
    {
        return Tool_interactions.pest_removal;
    }
}

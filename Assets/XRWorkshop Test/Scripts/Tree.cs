using Assets.XRWorkshop_Test.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : ForestInteractable
{
    public static event Action TreeReplanted;

    [SerializeField, Tooltip("The growth script will invoke a function that calls the On Complete unity event.")]
    private Growth growth;

    void Start()
    {
        growth.GrowthComplete += OnGrowthComplete;
    }

    private void OnGrowthComplete()
    {
        OnInteractComplete.Invoke();
    }

    private void OnDestroy()
    {
        growth.GrowthComplete -= OnGrowthComplete;
    }

    public override Tool_interactions getInteractionType()
    {
        return Tool_interactions.plant_tree;
    }

    public void TreePlanted()
    {
        TreeReplanted?.Invoke();
    }

}

using Assets.XRWorkshop_Test.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : ForestInteractable
{
    public static event Action TreeReplanted;

    public override Tool_interactions getInteractionType()
    {
        return Tool_interactions.plant_tree;
    }
}

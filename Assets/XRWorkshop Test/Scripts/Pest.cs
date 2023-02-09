using Assets.XRWorkshop_Test.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pest : ForestInteractable
{
    public static event Action PestRemoved;
    
    public override Tool_interactions getInteractionType()
    {
        return Tool_interactions.pest_removal;
    }
}

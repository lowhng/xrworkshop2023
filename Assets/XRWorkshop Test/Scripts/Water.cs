using Assets.XRWorkshop_Test.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : ForestInteractable
{ 
    public static event Action WaterCleaned;

    public void CleanWater()
    {
        WaterCleaned?.Invoke(); // no time delay used.
        OnInteractComplete.Invoke();
    }

    public override Tool_interactions getInteractionType()
    {
        return Tool_interactions.clean_water;
    }

}

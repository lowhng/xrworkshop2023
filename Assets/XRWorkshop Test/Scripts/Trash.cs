using Assets.XRWorkshop_Test.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : ForestInteractable
{
    public static event Action TrashRemoved;

    public void RemoveTrash() {
        TrashRemoved?.Invoke();
        OnInteractComplete.Invoke();
    }

    public override Tool_interactions getInteractionType()
    {
        return Tool_interactions.trash_removal;
    }
}

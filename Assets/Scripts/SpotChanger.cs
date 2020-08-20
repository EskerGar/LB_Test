using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class SpotChanger: MonoBehaviour
{
    [SerializeField] private List<Spot> spotsList;
    [SerializeField] private List<Visitor> visitorsList;
    [SerializeField] private Inputs inputs;
    

    private void Start()
    {
        Assert.IsFalse(spotsList.Count <= 0 || visitorsList.Count <= 0, "spotsList.Count <= 0 || visitorsList.Count <= 0");
        inputs.OnStartChangeSpots += ChangeSpots;
    }

    private void ChangeSpots()
    {
        foreach (var visitor in visitorsList)
        {
            GiveVisitorPlace(visitor, spotsList);
        }
    }

    private Spot GetFreeSpot(IEnumerable<Spot> spotList)
    {
        var freeSpot = spotList.FirstOrDefault(spot => spot.IsFree);
        if (freeSpot == null) return null;
        freeSpot.IsFree = false;
        return freeSpot;
    }

    private void GiveVisitorPlace(Visitor visitor, IEnumerable<Spot> spotList)
    {
        var spot = GetFreeSpot(spotList);
        if (!visitor.CanGoToSpot(spot) && spot != null)
            spot.IsFree = true;
    }

}
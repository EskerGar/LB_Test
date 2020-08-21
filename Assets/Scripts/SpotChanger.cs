using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class SpotChanger: MonoBehaviour
{
    [SerializeField] private Spawn spawn;
    [SerializeField] private Inputs inputs;

    private List<Visitor> _visitorsList;
    private List<Spot> _spotsList;

    private void Start()
    {
        _visitorsList = spawn.GetVisitorList();
        _spotsList = spawn.GetSpotList();
        Assert.IsFalse(_spotsList.Count <= 0 || _visitorsList.Count <= 0, "spotsList.Count <= 0 || visitorsList.Count <= 0");
        inputs.OnStartChangeSpots += ChangeSpots;
    }

    private void OnDestroy()
    {
        inputs.OnStartChangeSpots -= ChangeSpots;
    }

    private void ChangeSpots()
    {
        foreach (var visitor in _visitorsList)
        {
            GiveVisitorPlace(visitor, GetRandomFreeSpot(_spotsList));
        }
    }

    private Spot GetRandomFreeSpot(IEnumerable<Spot> spotList)
    {
        var freeSpots = spotList.Where(spot => spot.IsFree).ToList();
        return freeSpots[Random.Range(1, freeSpots.Count)];
    }

    private void GiveVisitorPlace(Visitor visitor, Spot spot)
    {
        if (spot != null)
            visitor.CanGoToSpot(spot);
    }

}
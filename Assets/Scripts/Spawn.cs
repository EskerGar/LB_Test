using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject visitorPrefab;
    [SerializeField] private GameObject spotPrefab;
    [SerializeField] private int visitorsAmount;
    [SerializeField] private int spotsAmount;
    [SerializeField] private float visitorsRadius = 2f;
    [SerializeField] private float spotsRadius = 5f;

    public List<Spot> GetSpotList() => GenerateObjects<Spot>(spotPrefab, spotsAmount, spotsRadius, new Vector3(0f, 0f, 1f));

    public List<Visitor> GetVisitorList() => GenerateObjects<Visitor>(visitorPrefab, visitorsAmount, visitorsRadius, Vector3.zero);
    
    private void Start()
    {
        Assert.IsFalse(visitorsRadius >= spotsRadius, "VisitorsRadius >= SpotsRadius");
    }

    private List<T> GenerateObjects<T>(GameObject prefab, int amount, float radius, Vector3 startPosition)
    {
        var list = new List<T>();
        for (int i = 0; i < amount; i++)
        {
            var angle = i * Mathf.PI * 2f / amount;
            var newPos = startPosition + (new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f));
            var obj = Instantiate(prefab, newPos, Quaternion.identity);
            list.Add(obj.GetComponent<T>());
        }

        return list;
    }
}
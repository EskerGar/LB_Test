using System;
using UnityEngine;

public class Inputs: MonoBehaviour
{
    [SerializeField] private KeyCode startChangeSpots = KeyCode.Space;

    public event Action OnStartChangeSpots;

    private void Update()
    {
        if(Input.GetKeyDown(startChangeSpots))
            OnStartChangeSpots?.Invoke();
    }
}
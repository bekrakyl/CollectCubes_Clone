using System;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : MonoBehaviour
{
    [SerializeField] private List<Cube> activeCubes = new List<Cube>();

    private void Awake()
    {
        activeCubes.Clear();
        activeCubes = new List<Cube>();
    }

    public void AddCubes(Cube cube) 
    {
        if (!activeCubes.Contains(cube))
            activeCubes.Add(cube);
    }

    public void OnCubeCollected(Cube cube)
    {
        if(activeCubes.Contains(cube))
            activeCubes.Remove(cube);

        if (activeCubes.Count <= 0 && GameManager.Instance.ExecuteGame)
            GameManager.Instance.GameWin?.Invoke();
    }

    public Transform GetRandomCubeTr()
    {
        return activeCubes.RandomAt().transform;
    }
}

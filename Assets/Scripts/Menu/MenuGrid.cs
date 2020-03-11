using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGrid : MonoBehaviour
{
    private GameObject cellPrefab;

    public int Index;
    private void Awake()
    {
        this.cellPrefab = Resources.Load<GameObject>("Prefabs/UIPrefabs/Menu/MenuCell");
    }

    public void FullData(int index, MenuCell[] cells)
    {
        this.Index = index;
    }
}

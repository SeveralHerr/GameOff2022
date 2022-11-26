using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[CreateAssetMenu(fileName = "so_SelectableObject", menuName = "ScriptableObjects/SelectableObject")]
public class SelectableObject : ScriptableObject
{
    public GameObject Prefab; // turrent or UI
    public GameObject MouseOverPrefab;
    public List<Button> Buttons;
    public int Cost;
    public SelectableType Type;

}
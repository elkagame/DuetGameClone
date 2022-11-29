using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
#region Singleton class: GameManager

public static GameManager Instance;

void Awake()
{
    if(Instance == null)
       Instance = this;
}

#endregion



[HideInInspector] public bool isGameover = false;
}

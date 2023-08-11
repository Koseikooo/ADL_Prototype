using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager acc;

    public NavigationManager Navigation;

    private void Awake()
    {
        acc = this;
    }
}

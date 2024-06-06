using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarEnemies : MonoBehaviour
{
    private RectTransform rect;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        rect.rotation = Quaternion.Euler(0, 90, 0);
    }
}

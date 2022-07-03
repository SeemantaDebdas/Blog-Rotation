using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DebugTest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dotProductText;
    [SerializeField] Transform otherCharacter;

    private void Start()
    {
        DebugDotProduct();
    }

    private void DebugDotProduct()
    {
        Vector3 distance = (otherCharacter.position - transform.position).normalized;
        Vector3 crossProduct = Vector3.Cross(transform.forward, distance);
        dotProductText.text = $"<color=red>Cross Product:</color> {crossProduct:0.00}";
    }
}

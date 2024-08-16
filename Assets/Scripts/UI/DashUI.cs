using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class DashUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform dashLoadingBar;
    [SerializeField]
    private ShipTranslate shipTranslate;

    private float defaultWidth;

    private void Start()
    {
        defaultWidth=dashLoadingBar.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        float x = math.remap(shipTranslate.DashCooldown,0,0, defaultWidth,shipTranslate.TimeRemaining);
        dashLoadingBar.sizeDelta=new Vector2(x, dashLoadingBar.sizeDelta.y);
    }
}

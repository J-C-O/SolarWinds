using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ownable : MonoBehaviour
{
    public int owner;
    private int oldOwner = 0;

    public void Start() {
        MixColor();
        oldOwner = owner;
    }

    public void Update()
    {
        if (owner != oldOwner) {
            MixColor();
            oldOwner = owner;
        }
    }

    private void MixColor() {
        if (PlayerManager.PMInstance == null || PlayerManager.PMInstance.Players.Count == 0) {
            return;
        }
        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        float hue, saturation, val;
        Color.RGBToHSV(PlayerManager.PMInstance.Players[owner].PlayerColor, out hue, out saturation, out val);
        var mixColor = Color.HSVToRGB(hue, saturation / 2, 1);
        foreach (var renderer in renderers)
        {
            mixColor.a = renderer.material.color.a;
            renderer.material.color = mixColor;
        }
    }
}

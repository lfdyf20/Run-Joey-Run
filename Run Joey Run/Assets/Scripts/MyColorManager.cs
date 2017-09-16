using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyColorManager {

    private Color[] myColors = new Color[] { Color.red,
        Color.magenta, Color.black, Color.blue, Color.cyan,
        Color.gray, Color.green, Color.white, Color.yellow };

    public Color ChooseRandomColor() {
        return myColors[Random.Range(0, myColors.Length)];
    }

}

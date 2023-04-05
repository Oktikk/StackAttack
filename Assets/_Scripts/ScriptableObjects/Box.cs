using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Box")]
public class Box : ScriptableObject
{
    public enum Color
    {
        BLUE,
        CYAN,
        GREEN,
        PINK,
        PURPLE,
        RED,
        YELLOW
    }

    public Color color;

}

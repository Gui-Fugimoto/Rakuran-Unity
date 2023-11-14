using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character : IComparable
{
    public string id;
    public int speed;

    public int CompareTo(object obj)
    {
        var a = this;
        var b = obj as Character;

        if (a.speed < b.speed)
            return -1;

        if (a.speed > b.speed)
            return 1;

        return 0;
    }
}

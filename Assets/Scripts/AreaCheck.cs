using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AreaCheck
{
    public static bool IsWithinThisArea(Vector2 worldPosition, LayerMask layerMask)
    {
        if (Physics2D.OverlapPoint(worldPosition, layerMask))
            return true;
        else
            return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    public Material mat;

    float num = 0;

    public void MoveMapOffset(float offsetY)
    {
        num += offsetY;

        Vector2 offset = new Vector2(0, num);

        mat.SetTextureOffset("_BaseMap", offset);

        //Debug.Log(num);

    }




}

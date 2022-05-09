using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_WorldObject : MonoBehaviour
{
    public Sprite icon;
    public Color col = Color.white;
    public string text;
    public int textSize = 10;

    // Start is called before the first frame update
    void Start()
    {
        MM.instance.CreateMMWorldObject(this);
    }
}

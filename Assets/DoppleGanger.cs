using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoppleGanger : MonoBehaviour
{
    [SerializeField] private Material Purple;
    [SerializeField] private float value;
    [SerializeField] private float hue;
    [SerializeField] private float saturation;


    void Start()
    {
        value = 0f;
        Purple.SetColor("_Purple", Color.HSVToRGB(hue / 360, saturation / value, 0));
    }
    // Update is called once per frame
    void Update()
    {
        /*if( == true)
        {

        }*/
        value += Time.deltaTime * 50;
        Purple.SetColor("_Purple", Color.HSVToRGB(hue / 360, saturation / 100, value / 100));
    }
}

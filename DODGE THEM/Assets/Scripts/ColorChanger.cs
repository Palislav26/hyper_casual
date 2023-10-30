using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Camera cam;
    public Color[] colors;
    public float changeSpeed;
    public float time;
    private float currentTime;
    private int colorIndex;

    private void Awake()
    {
        cam = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ColorChange();
        ColorChangeTime();
    }

    //changes color of the background between set colors from inspector, they change over time
    void ColorChange()
    {
        cam.backgroundColor = Color.Lerp(cam.backgroundColor, colors[colorIndex], changeSpeed * Time.deltaTime);
    }

    void ColorChangeTime()
    {
        if(currentTime <= 0)
        {
            colorIndex++;
            CheckColorIndex();
            currentTime = time;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }
    //Once all colours are exchanged, process starts from beginning
    void CheckColorIndex()
    {
        if(colorIndex >= colors.Length)
        {
            colorIndex = 0;
        }
    }

    private void OnDestroy()
    {
        cam.backgroundColor = colors[0];
    }
}

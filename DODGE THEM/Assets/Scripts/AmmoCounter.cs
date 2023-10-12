using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    public TMP_Text bulletsTxt;
    public TMP_Text granadesTxt;
    public int numberOfBullets;
    public int numberOfGranades;

    // Start is called before the first frame update
    void Start()
    {
        numberOfBullets = 0;
        numberOfGranades = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBullets();
        UpdateGranades();
    }
    public void AddBullets(int newBullets)
    {
        numberOfBullets += newBullets;
    }

    public void DeductBullets(int newBullets)
    {
        numberOfBullets -= newBullets;
    }

    public void AddGranades(int newGranades)
    {
        numberOfGranades += newGranades;
    }

    public void DeductGranades(int newGranades)
    {
        numberOfGranades -= newGranades;
    }

    //method that reflects the currect score on the screen
    public void UpdateBullets()
    {
        bulletsTxt.text = "" + numberOfBullets;
    }
    public void UpdateGranades()
    {
        granadesTxt.text = "" + numberOfGranades;
    }
}

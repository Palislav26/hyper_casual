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
        //we tell UI how many ammunition player has at the start
        numberOfBullets = 50;
        numberOfGranades = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBullets();
        UpdateGranades();
    }

    //adds bullets 
    public void AddBullets(int newBullets)
    {
        numberOfBullets += newBullets;
    }

    //deduct bullets
    public void DeductBullets(int newBullets)
    {
        numberOfBullets -= newBullets;
    }

    //adds granades 
    public void AddGranades(int newGranades)
    {
        numberOfGranades += newGranades;
    }

    //adds granades 
    public void DeductGranades(int newGranades)
    {
        numberOfGranades -= newGranades;
    }

    //method that reflects the currect bullet count on the screen
    public void UpdateBullets()
    {
        bulletsTxt.text = "" + numberOfBullets;
    }

    //method that reflects the currect granade count on the screen
    public void UpdateGranades()
    {
        granadesTxt.text = "" + numberOfGranades;
    }
}

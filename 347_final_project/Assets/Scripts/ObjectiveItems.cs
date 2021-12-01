using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveItems : MonoBehaviour
{
    
    private static ArrayList RadioSpots = new ArrayList();
    private static ArrayList GunSpots = new ArrayList();
    private static ArrayList KeySpots = new ArrayList();


    private Vector3 newLoc;
    // Start is called before the first frame update
    void Start()
    {
        LoadLocations();


    }

    void LoadLocations()
    {

        if(this.gameObject.tag == "Radio")
        {
            int radioSpot = Random.Range(0,3);
            RadioSpots.Add(new Vector3(25.5f, 2.395f, 180.735f));
            RadioSpots.Add(new Vector3(17.19f, 1.63f, 179.43f));
            RadioSpots.Add(new Vector3(5.23f, 1.98f, 179.43f));
            newLoc = (Vector3)RadioSpots[radioSpot];
            this.gameObject.transform.position = newLoc;
        }
        else if(this.gameObject.tag == "Gun")
        {
            int gunSpot = Random.Range(0, 3);
            GunSpots.Add(new Vector3(24.705f, 0.25f, 207.9099f));
            GunSpots.Add(new Vector3(20f, 9.467f, 197.7f));
            GunSpots.Add(new Vector3(-9.543f, 9.968f, 197.7f));
            newLoc = (Vector3)GunSpots[gunSpot];
            this.gameObject.transform.position = newLoc;
        }
        else if (this.gameObject.tag == "Key")
        {
            int keySpot = Random.Range(0, 2);
            print(keySpot);
            KeySpots.Add(new Vector3(10.238f, 1.845f, 190.998f));
            KeySpots.Add(new Vector3(-18.886f, 2.385f, 190.998f));
            newLoc = (Vector3)KeySpots[keySpot];
            this.gameObject.transform.position = newLoc;
        }
        else if(this.gameObject.tag == "Blackmail")
        {

        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

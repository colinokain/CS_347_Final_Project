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
        int index;
        GameObject[] locations;

        if (this.gameObject.tag == "Radio")
        {
            locations = GameObject.FindGameObjectsWithTag("RadioLocation");
            index = Random.Range(0, locations.Length);
            this.gameObject.transform.position = locations[index].transform.position;
        }
        else if(this.gameObject.tag == "Gun")
        {
            locations = GameObject.FindGameObjectsWithTag("WeaponLocation");
            index = Random.Range(0, locations.Length);
            this.gameObject.transform.position = locations[index].transform.position;
        }
        else if (this.gameObject.tag == "Key")
        {
            locations = GameObject.FindGameObjectsWithTag("KeyLocation");
            index = Random.Range(0, locations.Length);
            this.gameObject.transform.position = locations[index].transform.position;
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

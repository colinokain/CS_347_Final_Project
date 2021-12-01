using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveItems : MonoBehaviour
{
    
    private static ArrayList RadioSpots = new ArrayList();

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
            print(radioSpot);
            RadioSpots.Add(new Vector3(25.5f, 2.395f, 180.735f));
            RadioSpots.Add(new Vector3(17.19f, 1.63f, 179.43f));
            RadioSpots.Add(new Vector3(5.23f, 1.98f, 179.43f));
            Vector3 newLoc = (Vector3)RadioSpots[radioSpot];
            this.gameObject.transform.position = newLoc;
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

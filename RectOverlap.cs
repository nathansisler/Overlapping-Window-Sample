using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is based on the two rectangle problem. We will generate two random rectangles 
//and give them a random color. We will then determine the size of the overlap between the two 
//rectangles and instantiate a rectangle of size at the location.

//To run place a cube prefab in the cube variable. Press any button once game is running to activate and reset.


public class RectOverlap : MonoBehaviour {

    public GameObject cube;
    public int numberOfRects = 2;

    List<MyXY> myCoords = new List<MyXY>(); //coordinates of the two random rectangles.

	void Update ()
    {
		if(Input.anyKeyDown)
        {
            //Clear the list for a new set
            myCoords.Clear();

            //Clear the rectangles from the scene
            foreach(GameObject i in GameObject.FindGameObjectsWithTag("MyCube"))
            {
                Destroy(i);
            }

            //Create the amount of rectangles were looking for
            for(int i =0; i< numberOfRects; i++)
            {
                Randomize();
            }

            CheckForOverlap();
        }
	}

    //Generate and instantiate a random rectangle then store the coordinants in a list
    void Randomize()
    {
       
        for (int i = 0; i < 2; i++)
        {
            MyXY temp = new MyXY();
            temp.x = Random.Range(2, 15);
            temp.y = Random.Range(2, 15);
            myCoords.Add(temp);
        }

        int thisSet = myCoords.Count -2;

        int max = Mathf.Max(myCoords[thisSet].x, myCoords[thisSet + 1].x);
        int min = Mathf.Min(myCoords[thisSet].x, myCoords[thisSet + 1].x);

        int yMin = Mathf.Min(myCoords[thisSet].y, myCoords[thisSet + 1].y);
        int yMax = Mathf.Max(myCoords[thisSet].y, myCoords[thisSet + 1].y);

        Color rectColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);// Generate a random color.
        rectColor.a = .5f; // Set color alpha to make the two rectangles easier to see

        for (int i = min; i<= max; i++)
        {
            GameObject myCube = Instantiate(cube);
            myCube.transform.position = new Vector3(i, yMin, 0);
            myCube.GetComponent<Renderer>().material.color = rectColor;

            for (int j = yMin+1; j<= yMax; j++)
            {
                GameObject myYCube = Instantiate(cube);
                myYCube.transform.position = new Vector3(i, j, 0);
                myYCube.GetComponent<Renderer>().material.color = rectColor;
            }
        }

    }

    //This aspect of the script will first need to locate the second x coordinate and the second to last x 
    //coordinate. Same with the y coordinates. If all coordinates are positive the function will 
    //instantiate a third rectangle in overlap space.
  void CheckForOverlap()
    {
        int farX = 0;
        int closeX = 0;
        int farY = 0;
        int closeY = 0;

        if(Mathf.Max(myCoords[0].x, myCoords[1].x) <= Mathf.Max(myCoords[2].x, myCoords[3].x))
        {
            farX = Mathf.Max(myCoords[0].x, myCoords[1].x);
        }
        else if(Mathf.Max(myCoords[0].x, myCoords[1].x) > Mathf.Max(myCoords[2].x, myCoords[3].x))
        {
            farX = Mathf.Max(myCoords[2].x, myCoords[3].x);
        }

        if (Mathf.Min(myCoords[0].x, myCoords[1].x) >= Mathf.Min(myCoords[2].x, myCoords[3].x))
        {
            closeX = Mathf.Min(myCoords[0].x, myCoords[1].x);
        }
        else if (Mathf.Min(myCoords[0].x, myCoords[1].x) < Mathf.Min(myCoords[2].x, myCoords[3].x))
        {
            closeX = Mathf.Min(myCoords[2].x, myCoords[3].x);
        }

        if (Mathf.Max(myCoords[0].y, myCoords[1].y) <= Mathf.Max(myCoords[2].y, myCoords[3].y))
        {
            farY = Mathf.Max(myCoords[0].y, myCoords[1].y);
        }
        else if (Mathf.Max(myCoords[0].y, myCoords[1].y) > Mathf.Max(myCoords[2].y, myCoords[3].y))
        {
            farY = Mathf.Max(myCoords[2].y, myCoords[3].y);
        }

        if (Mathf.Min(myCoords[0].y, myCoords[1].y) >= Mathf.Min(myCoords[2].y, myCoords[3].y))
        {
            closeY = Mathf.Min(myCoords[0].y, myCoords[1].y);
        }
        else if (Mathf.Min(myCoords[0].y, myCoords[1].y) < Mathf.Min(myCoords[2].y, myCoords[3].y))
        {
            closeY = Mathf.Min(myCoords[2].y, myCoords[3].y);
        }

        // instantite third rectangle
        if((farX - closeX) >= 0 && (farY - closeY) >= 0)
        {
            for (int i = closeX; i <= farX; i++)
            {
                GameObject myCube = Instantiate(cube);
                myCube.transform.position = new Vector3(i, closeY, -.1f);// .1 offset to see them better

                for (int j = closeY + 1; j <= farY; j++)
                {
                    GameObject myYCube = Instantiate(cube);
                    myYCube.transform.position = new Vector3(i, j, -.1f);
                }
            }
            print("Overlap ax" + closeX + " ay" + closeY + " bx" + farX + " by" + farY);
        }
        else
        {
            print("No Overlap");
        }

    }


}

[System.Serializable] // Serializable class to stucture list items
public class MyXY
{
    public int x;
    public int y;
}
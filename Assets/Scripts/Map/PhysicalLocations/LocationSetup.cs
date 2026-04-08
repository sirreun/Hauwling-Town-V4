using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSetup : DebugMonoBehaviour
{
    [Range(0f, 200f)]
    public float LocationWidth = 10f; //TODO: currently doenst match with the tilemap, will have overhangs
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private Vector3 centerPosition;

    [ContextMenu("Move Walls")]
    public void MoveWalls()
    {
        Vector3 newLeftPosition = new Vector3(centerPosition.x - (LocationWidth / 2), centerPosition.y, centerPosition.z);
        Vector3 newRightPosition = new Vector3(centerPosition.x + (LocationWidth / 2), centerPosition.y, centerPosition.z);

        leftWall.transform.position = newLeftPosition;
        rightWall.transform.position = newRightPosition;

        //TODO: This also needs to add markers for where the back background needs to go to
        //TODO: also match location position and player position for indoor and outdoor? different zooms?
        //TODO: connect wall/item foreground to wall grids
    }
}
using System.Collections.Generic;
using UnityEngine;

public class GameAction
{
    public int Id { get; set; }
    public string ActionName { get; set; }
    public string AnimationName { get; set; }
    public Dictionary<int, int> PersonalityModifiers { get; set; }
    public Vector3 Position { get; set; }


    public List<int> NeighbourIds = new List<int>();
    public Dictionary<int, Vector3> ClaimablePositions = new Dictionary<int, Vector3>();

    //TODO: CHANGE THIS AMOUNT AND/OR MAKE IT PUBLICLY AVAILABLE
    private int maxAmountOfReactors = 10;

    public bool IsPositionAvailable(int npcId)
    {
        if (ClaimablePositions.Count <= maxAmountOfReactors)
            return true;
        return false;
    }

    public Vector3 CreatePosition(int npcId)
    {
        while (true)
        {
            var grid = GetGrid();
            var randomX = Position.x + Random.Range(-1.5f, 1.5f);
            var randomZ = Position.z + Random.Range(-1.5f, 1.5f);
            var returnVector = new Vector3(randomX, 0.0f, randomZ);

            if (grid.IsWalkable(returnVector))
            {
                ClaimablePositions.Add(npcId, returnVector);
                return returnVector;
            }
        }
    }

    public override string ToString()
    {
        return "gameaction " + Id + " " + ActionName + " " + AnimationName + " " + Position;
    }

    private Grid GetGrid()
    {
        return GameObject.Find("AvatarGenerator").GetComponent<Grid>();
    }
}
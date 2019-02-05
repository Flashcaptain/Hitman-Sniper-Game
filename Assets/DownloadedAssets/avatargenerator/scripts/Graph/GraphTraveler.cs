﻿﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class GraphTraveler
{
    public Dictionary<int, GameAction> Actions = ActionsParser.NormalActions;
    private const int EvolutionAmount = 10;
    private readonly NpcObject _npc;

    public GraphTraveler(NpcObject npc)
    {
        _npc = npc;
    }

    public List<int> GetBestPath()
    {
        const int sampleSize = 6;
        const int headSize = sampleSize / 3;
        const int tailSize = sampleSize / 3;
        const int bodySize = sampleSize - headSize - tailSize;

        var possiblePaths = new List<List<int>>();
        sampleSize.Times(() => possiblePaths.Add(RandomPath()));

        for (var i = 0; i < EvolutionAmount; i++)
        {
            possiblePaths = possiblePaths.OrderByDescending(x => Weight(x)).ToList();

            var tempPossiblePaths = new List<List<int>>(possiblePaths);

            var bestPaths = tempPossiblePaths.Take(headSize);
            var acceptablePaths = tempPossiblePaths.Skip(headSize).Take(bodySize).ToList();
            var newPaths = new List<List<int>>();

            //TODO: Change mutate method because now it doesn't make a difference
            acceptablePaths.ToList().ForEach(x => Mutate(x));
            tailSize.Times(() => newPaths.Add(RandomPath()));

            possiblePaths.Clear();
            possiblePaths.AddRange(bestPaths);
            possiblePaths.AddRange(acceptablePaths);
            possiblePaths.AddRange(newPaths);
        }

        return possiblePaths.OrderByDescending(path => Weight(path)).First();
    }

    private float Weight(List<int> actionIds)
    {
        var tempAccumulatedValues = new GenericVector(_npc.AccumulatedValues.Size);
        tempAccumulatedValues.Sum(_npc.AccumulatedValues);

        for (var i = 0; i < actionIds.Count; i++)
        {
            var action = Actions[actionIds[i]];
            tempAccumulatedValues.Sum(action.PersonalityModifiers.ToGenericVector());
        }

        var angle = GenericVector.GetAngle(tempAccumulatedValues, _npc.PersonalityValuesGenericVector);
        var weigth = 10 / (angle + 1);

        return weigth;
    }

    private List<int> RandomPath(int maxLength = 10)
    {
        var randomPath = new HashSet<int>();
        var possibleFirstActions = Actions.Where(x => x.Value.NeighbourIds.Count > 0).Select(x => x.Value).ToList();

        if (!possibleFirstActions.Any())
        {
            var action = Actions.ElementAt(Random.Range(0, Actions.Count)).Value;
            randomPath.Add(action.Id);
            return randomPath.ToList();
        }
        var currentAction = possibleFirstActions[Random.Range(0, possibleFirstActions.Count)];

        while (currentAction.NeighbourIds.Count > 0)
        {
            randomPath.Add(currentAction.Id);

            var neighbours = new HashSet<int>(currentAction.NeighbourIds);
            neighbours.ExceptWith(randomPath);

            if (neighbours.Count <= 0)
            {
                break;
            }
            var randomNeighbour = neighbours.ElementAt(Random.Range(0, neighbours.Count));

            currentAction = Actions[randomNeighbour];
        }
        return randomPath.ToList();
    }

    private void Mutate(List<int> actionIds)
    {
        var mutationChance = 0.0f;
        var mutationFactor = 100 / actionIds.Count - 1;

        for (var i = 0; i < actionIds.Count - 1; i++)
        {
            mutationChance += mutationFactor;
            var random = Random.Range(0, 100);
            var neighboursCurrentNode = new HashSet<int>(Actions[actionIds[i]].NeighbourIds);
            var neighboursNextNode = new HashSet<int>(Actions[actionIds[i + 1]].NeighbourIds);

            if (random < mutationChance
                && i + 2 >= actionIds.Count
                && neighboursNextNode.Contains(actionIds[i]))
            {
                actionIds.Swap(i, i + 1);
                break;
            }

            if (random < mutationChance
                && neighboursNextNode.Contains(actionIds[i])
                && neighboursCurrentNode.Contains(actionIds[i + 2]))
            {
                actionIds.Swap(i, i + 1);
            }
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
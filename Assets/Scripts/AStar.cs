using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    public static PriorityQueue closedList, openList;
	private static float HeuristicEstimateCost(Node currentNode, Node goalNode)
	{
		Vector3 vectorCost = currentNode.position - goalNode.position;
        Debug.Log($"Vector cost: {vectorCost}");
		return vectorCost.magnitude;
	}

	/// <summary>
	/// CalculatePath calculates a path from a Node to the parent (previous node)
	/// It gets the parent of the current node and add to a list then reverse to 
	/// show the path from start to finish.
	/// </summary>
	/// <param name="node"></param>
	/// <returns></returns>
	private static ArrayList CalculatePath(Node node)
	{
		ArrayList list = new ArrayList();
		while (node != null)
		{
			list.Add(node);
			node = node.parent;
            Debug.Log($"Vector cost: {list}");
		}
		list.Reverse();
        Debug.Log($"Vector cost: {list}");
		return list;
	}

	public static ArrayList FindPath(Node startNode, Node endNode)
	{
		openList = new PriorityQueue();
		openList.Add(startNode);
		startNode.nodeTotalCost = 0f;
		startNode.estimateCost = HeuristicEstimateCost(startNode, endNode);

		closedList = new PriorityQueue();
		Node node = null;
		while(openList.Length != 0)
		{
			node = openList.GetFirst();
			if (node.position == endNode.position)
			{
				return CalculatePath(node);
			}

			ArrayList neighbours = new ArrayList();
			// TODO GetNeighbours from GridManager
			GridManager.Instance.GetNeighbours(node, neighbours);

			for (int index = 0; index < neighbours.Count; index++)
			{
				Node neighbourNode = (Node)neighbours[index];
				if (!closedList.Contains(neighbourNode))
				{
					float cost = HeuristicEstimateCost(node, neighbourNode);
					float totalCost = node.nodeTotalCost + cost;
					float neighbourNodeCost = HeuristicEstimateCost(neighbourNode, endNode);

					neighbourNode.nodeTotalCost = totalCost;
					neighbourNode.parent = node;
					neighbourNode.estimateCost = totalCost + neighbourNodeCost;

					if (!openList.Contains(neighbourNode))
					{
						openList.Add(neighbourNode);
					}
				}
			}
			closedList.Add(node);
			openList.Remove(node);
		}
		if (node.position != endNode.position)
		{
			Debug.Log("Goal node not found");
			return null;
		}
		return CalculatePath(node);		
	}
}

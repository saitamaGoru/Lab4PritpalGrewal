using System.Collections;

public class PriorityQueue 
{
   private ArrayList _nodeList = new ArrayList();

   public int Length => _nodeList.Count;

   public bool Contains(object node) => _nodeList.Contains(node);

    public Node GetFirst()
	{
		if (_nodeList.Count > 0)
		{
			return (Node)_nodeList[0];
		}
		return null;
	}

	public void Add(Node node)
	{
		_nodeList.Add(node);
		_nodeList.Sort(); // Use the IComparable interface to compare and sort it
	}

	public void Remove(Node node)
	{
		_nodeList.Remove(node);
		_nodeList.Sort();
	}

}

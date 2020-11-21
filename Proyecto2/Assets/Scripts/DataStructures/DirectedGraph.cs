using System;

namespace DataStructures {
    
public class Graph<T> where T : IComparable
{

    private LinkedList<GraphNode<T>> Nodes {get;} 
    private LinkedList<LinkedList<GraphNode<T>>> RelationsMatrix {get; set; }


    public Graph(){
            Nodes = new LinkedList<GraphNode<T>>();
            RelationsMatrix = new LinkedList<LinkedList<GraphNode<T>>>();
        }

    public void Add(T value,int weight){

            var newNode = new GraphNode<T>(value,weight);
            Nodes.Add(newNode);
            
        }

        
    public GraphNode<T> Get(T value) {
        
        int counter;
        for (counter = 0; counter < Nodes.Len - 1; counter++)
            if(Nodes.Get(counter).Data.CompareTo(value)==0)
                break;

        return Nodes.Get(counter);
            
    }


    public void AddEdge(T node1, T node2)
        {
            var nodeOne =Get(node1);
            var nodeTwo= Get(node2);
            nodeOne.AdjacentNodes.Add(nodeTwo);
            nodeTwo.AdjacentNodes.Add(nodeOne);
                           
        }



}


public class GraphNode<T> where T : IComparable
{

    public T Data    {get;set;}
    public int Weight{get;set;}
    public LinkedList<GraphNode<T>> AdjacentNodes{ get; set; }

    public GraphNode(T data, int weight)
    {
        Data = data;
        Weight = weight;
        AdjacentNodes= new LinkedList<GraphNode<T>>();
    }

 

}
}
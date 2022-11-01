using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    class Graph
    {
    }

    interface IMap
    { 
    }

    class TileMap : IMap
    {
    }

    class GraphMap : IMap
    {
        public Graph graph;
    }

    interface IAStarAdapter
    {
        Graph Transfer(IMap map);
    }

    class HexMap : IMap
    {
    }

    class HexAdapter : IAStarAdapter
    {
        public Graph Transfer(IMap map)
        {
            return null;
        }
    }

    class AStar
    {
        public void Find(int x, int y, Graph graph)
        {
        }
    }

    class GraphAdapter : IAStarAdapter
    {
        public Graph Transfer(IMap map)
        {
            return ((GraphMap)map).graph;
        }
    }

    class TileAdapter: IAStarAdapter
    {
        public Graph Transfer(IMap map)
        {
            // return map.graph;
            return null;
        }
    }

    static class AStarTransfer
    {
        public static Graph ToGraph(this TileMap map) => new TileAdapter().Transfer(map);
        public static Graph ToGraph(this GraphMap map) => new GraphAdapter().Transfer(map);
        public static Graph ToGraph(this HexMap map) => new HexAdapter().Transfer(map);
    }


    class Program
    {
        static void Main(string[] args)
        {
            AStar aStar = new AStar();

            TileMap map = new TileMap();
            aStar.Find(10, 10, map.ToGraph());

            GraphMap gp = new GraphMap();
            aStar.Find(10, 10, gp.ToGraph());

            HexMap hm = new HexMap();
            aStar.Find(10, 10, hm.ToGraph());
        }
    }
}

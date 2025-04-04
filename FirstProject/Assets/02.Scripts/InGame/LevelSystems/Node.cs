using UnityEngine;

namespace Match3.InGame.LevelSystems
{
    public struct Node
    {
        public Node(int x, int y, NodeTypes typeFlags)
        {
            X = x;
            Y = y;
            TypeFlags = typeFlags;
        }

        public int X;
        public int Y;
        public NodeTypes TypeFlags;


    }
}


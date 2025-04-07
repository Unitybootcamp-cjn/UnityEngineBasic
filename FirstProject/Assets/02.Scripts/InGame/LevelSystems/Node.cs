using UnityEngine;

namespace Match3.InGame.LevelSystems
{
    public struct Node
    {
        public Node(int x, int y, NodeTypes typeFlags, Transform block)
        {
            X = x;
            Y = y;
            TypeFlags = typeFlags;
            Block = block;
        }

        public int X;
        public int Y;
        public NodeTypes TypeFlags;
        public Transform Block; 

    }
}


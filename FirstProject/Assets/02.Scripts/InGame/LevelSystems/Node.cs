using UnityEngine;

namespace Match3.InGame.LevelSystems
{
    public struct Node
    {
        public Node(NodeTypes typeFlags, Transform block)
        {
            IsScheduledForDestroy = false;
            TypeFlags = typeFlags;
            Block = block;
        }

        public bool IsScheduledForDestroy;
        public NodeTypes TypeFlags;
        public Transform Block; 

    }
}


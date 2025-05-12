using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject blockPrefab;
    public int width = 10;
    public int height = 5;



    void Start()
    {
        for(int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Instantiate(blockPrefab, new Vector3(i, j, 0), Quaternion.identity, transform);
            }
        }
    }

}

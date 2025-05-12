using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject blockPrefab;
    //public int width = 10;
    //public int height = 5;

    public int crosswidth = 3;
    public int crossheight = 12;


    void Start()
    {
        // 벽 만들기
        //for(int i = 0; i < height; i++)
        //{
        //    for (int j = 0; j < width; j++)
        //    {
        //        Instantiate(blockPrefab, new Vector3(i, j, 0), Quaternion.identity, transform);
        //    }
        //}

        for (int i = 0; i < crossheight; i++)
        {
            if(i == crossheight / 2)
            {
                for (int k = 0; k < crosswidth; k++)
                {
                    for(int l = 0; l < crossheight; l++)
                    {
                        Instantiate(blockPrefab, new Vector3(l - crossheight/2 + crosswidth/2, k + i - crosswidth/2, 0), Quaternion.identity, transform);
                    }
                }
            }
            for (int j = 0; j < crosswidth; j++)
            {

                Instantiate(blockPrefab, new Vector3(j, i, 0), Quaternion.identity, transform);
                
            }
        }
    }

}

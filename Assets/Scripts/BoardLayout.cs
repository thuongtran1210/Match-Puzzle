using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLayout : MonoBehaviour
{
    public LayOutRow[] allRows;
    public Gem[,] GetLayout()
    {
        Gem[,] theLayout = new Gem[allRows[0].gemsInRow.Length, allRows.Length];

        for (int y = 0; y < allRows.Length; y++)
        {
            for (int x = 0; x < allRows[y].gemsInRow.Length; x++)
            {
                if (x < theLayout.GetLength(0))
                {
                    if (allRows[y].gemsInRow[x] != null)
                    {
                        theLayout[x, allRows.Length - 1 - y] = allRows[y].gemsInRow[x];
                    }
                }
            }
        }

        return theLayout;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class LayOutRow
{
    public Gem[] gemsInRow;
}
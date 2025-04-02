using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchFinder : MonoBehaviour
{
    private Board board;
    public List<Gem> currentMathches = new List<Gem>();
    
    private void Awake()
    {
        board = FindObjectOfType<Board>();
    }
    public void FindAllMatches()
    {
        //currentMathches.Clear();
        for (int x = 0; x < board.width; x++)
        {
            for (int y = 0; y < board.height; y++)
            {
                Gem currentGem = board.allGems[x, y];
                if (currentGem != null)
                {
                    if (x > 0 && x < board.width - 1)
                    {
                        Gem leftGem = board.allGems[x - 1, y];
                        Gem rightGem = board.allGems[x + 1,y];
                        if (leftGem != null && rightGem != null)
                        {
                            if (leftGem.type == currentGem.type && rightGem.type == currentGem.type)
                            {
                                currentGem.isMatched = true;
                                leftGem.isMatched = true;
                                rightGem.isMatched = true;

                                currentMathches.Add(currentGem);
                                currentMathches.Add(leftGem);
                                currentMathches.Add(rightGem);
                                
                            }
                        }
                    }
                    if (y > 0 && y < board.height - 1)
                    {
                        Gem aboveGem = board.allGems[x , y + 1];
                        Gem belowGem = board.allGems[x , y - 1];
                        if (aboveGem != null && belowGem != null)
                        {
                            if (aboveGem.type == currentGem.type && belowGem.type == currentGem.type)
                            {
                                currentGem.isMatched = true;
                                aboveGem.isMatched = true;
                                belowGem.isMatched = true;

                                currentMathches.Add(currentGem);
                                currentMathches.Add(aboveGem);
                                currentMathches.Add(belowGem);
                            }
                        }
                    }
                }
            }
        }
        if (currentMathches.Count() > 0)
        {
            currentMathches = currentMathches.Distinct().ToList();
        }
    }

}

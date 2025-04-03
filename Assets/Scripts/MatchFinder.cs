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
        currentMathches.Clear();
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
        CheckForBombs();
    }
    public void CheckForBombs()
    {
        for (int i = 0; i < currentMathches.Count; i++)
        {
            Gem gem = currentMathches[i];

            int x = gem.posIndex.x;
            int y = gem.posIndex.y;
            // Check left pos x the Bomb
            if (gem.posIndex.x > 0)
            {
                if (board.allGems[x - 1,y] !=null )
                {
                    if (board.allGems[x - 1, y].type == Gem.GemType.bomb)
                    {
                        MarkBombArena(new Vector2Int(x-1,y), board.allGems[x-1,y]);
                    }
                }
            }
            // Check right pos x the bomb
            if (gem.posIndex.x < board.width - 1)
            {
                if (board.allGems[x + 1, y] != null)
                {
                    if (board.allGems[x + 1, y].type == Gem.GemType.bomb)
                    {
                        MarkBombArena(new Vector2Int(x + 1, y), board.allGems[x + 1, y]);
                    }
                }
            }
            // Check left pos y the Bomb
            if (gem.posIndex.y > 0)
            {
                if (board.allGems[x , y - 1] != null)
                {
                    if (board.allGems[x, y - 1].type == Gem.GemType.bomb)
                    {
                        MarkBombArena(new Vector2Int(x, y - 1), board.allGems[x, y - 1]);
                    }
                }
            }
            // Check right pos y the bomb
            if (gem.posIndex.y <  board.height -1 )
            {
                if (board.allGems[x, y + 1] != null)
                {
                    if (board.allGems[x, y + 1].type == Gem.GemType.bomb)
                    {
                        MarkBombArena(new Vector2Int(x, y + 1), board.allGems[x, y + 1]);
                    }
                }
            }
        }
    }
    public void MarkBombArena(Vector2Int bombPos, Gem theBomb)
    {
        for (int x = bombPos.x - theBomb.blastSize; x <=bombPos.x + theBomb.blastSize; x++)
        {
            for (int y = bombPos.y - theBomb.blastSize; y <= bombPos.y + theBomb.blastSize; y++)
            {
                if (x >= 0 && x < board.width && y>= 0 && y < board.height)
                {
                    if (board.allGems[x, y] != null)
                    {
                        board.allGems[x, y].isMatched = true;
                        currentMathches.Add(board.allGems[x, y]);
                    }
                }
            }
        }
        currentMathches = currentMathches.Distinct().ToList();
    }

}
 
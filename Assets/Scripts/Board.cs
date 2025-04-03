using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject bgTilePrefab;

    public Gem[] gems;
    public Gem[,] allGems;

    public float gemSpeed;

    [HideInInspector]
    public MatchFinder matchFind;
    public enum BoardState {wait, move }
    public BoardState currentState = BoardState.move;
    private void Awake()
    {
        matchFind = FindObjectOfType<MatchFinder>();

    }
    // Start is called before the first frame update
    void Start()
    {
        allGems = new Gem[width, height];

        Setup();

    }
    private void Update()
    {
        //matchFind.FindAllMatches();
    }
    private void Setup()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);
                GameObject bgTile = Instantiate(bgTilePrefab, pos, Quaternion.identity);
                bgTile.transform.parent = this.transform;
                bgTile.name = $"BG Tile - {x} , {y}";
                //Gem
                int gemToUse = Random.Range(0, gems.Length);
                int interactions = 0;
                while (MatchesAt(new Vector2Int(x, y), gems[gemToUse]) && interactions < 100)
                {
                    gemToUse = Random.Range(0, gems.Length);
                    interactions++;
                }

                SpawnGem(new Vector2Int(x, y), gems[gemToUse]);
            }
        }
    }
    private void SpawnGem(Vector2Int pos, Gem gemToSpawn)
    {
        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, pos.y + height, 0f), Quaternion.identity);
        gem.transform.parent = this.transform;
        gem.name = $"Gem {pos.x} , {pos.y}";

        allGems[pos.x, pos.y] = gem;

        gem.SetupGem(pos, this);
    }
    bool MatchesAt(Vector2Int posToCheck, Gem gemToCheck)
    {
        if (posToCheck.x > 1)
        {
            if (allGems[posToCheck.x - 1, posToCheck.y].type == gemToCheck.type &&
                allGems[posToCheck.x - 2, posToCheck.y].type == gemToCheck.type)
            {
                return true;
            }
        }
        if (posToCheck.y > 1)
        {
            if (allGems[posToCheck.x, posToCheck.y - 1].type == gemToCheck.type &&
                allGems[posToCheck.x, posToCheck.y - 2].type == gemToCheck.type)
            {
                return true;
            }
        }
        return false;
    }
    private void DestroyMatchedGemAt(Vector2Int position)
    {
        if (allGems[position.x, position.y] != null)
        {
            if (allGems[position.x, position.y].isMatched)
            {
                Destroy(allGems[position.x, position.y].gameObject);
                allGems[position.x, position.y] = null;
            }
        }
    }
    public void DestroyMatches()
    {
        for (int i = 0; i < matchFind.currentMathches.Count; i++)
        {
            if (matchFind.currentMathches[i] != null)
            {
                DestroyMatchedGemAt(matchFind.currentMathches[i].posIndex);
            }
        }
        StartCoroutine(DecreaseRowCor());
    }
    private IEnumerator DecreaseRowCor()
    {
        yield return new WaitForSeconds(.2f);
        int nullCounter = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                if (allGems[x, y] == null)
                {
                    nullCounter++;
                }
                else if(nullCounter > 0)
                {
                    allGems[x,y].posIndex.y -= nullCounter;
                    allGems[x, y - nullCounter] = allGems[x,y];
                    allGems[x,y] = null;
                }

            }
            nullCounter = 0;
        }
        StartCoroutine(FillBoardCor());
    }
    private IEnumerator FillBoardCor()
    {
        yield return new WaitForSeconds(.5f);
        ReFillBoard();

        yield return new WaitForSeconds(.5f);

        matchFind.FindAllMatches();
        if (matchFind.currentMathches.Count > 0)
        {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            currentState = BoardState.move;
        }

    }
    private void ReFillBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allGems[x, y] == null)
                {
                    int gemToUse = Random.Range(0, gems.Length);

                    SpawnGem(new Vector2Int(x, y), gems[gemToUse]);
                }
            }
        }
        CheckMisplacedGems();
    }
    private void CheckMisplacedGems()
    {
        List<Gem> foundGems = new List<Gem>();
        foundGems.AddRange(FindObjectsOfType<Gem>());
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (foundGems.Contains(allGems[x,y]))
                {
                     foundGems.Remove(allGems[x,y]);
                }
            }
        }
        foreach (Gem g in foundGems)
        {
            Destroy(g.gameObject);
        }
    }
}

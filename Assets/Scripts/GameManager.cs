using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform emptySpace;
    [SerializeField] private Tile[] tiles;
    [SerializeField] private int emptySpaceIndex;

    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit)
            {
                if(Vector2.Distance(emptySpace.position, hit.transform.position) < 1.3)
                {
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    emptySpace.position = hit.transform.position;
                    hit.transform.GetComponent<Tile>().MoveTile(lastEmptySpacePosition);
                    int tileIndex = FindIndex(hit.transform.GetComponent<Tile>());
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }
        }
        
    }

    private void Shuffle()
    {
        int inversion;

        if(emptySpaceIndex != tiles.Length - 1)
        {
            Vector2 tileOnEmptySpacePosition = tiles[tiles.Length - 1].GetTargetPosition();
            tiles[tiles.Length - 1].SetTargetPosition(emptySpace.position);
            emptySpace.position = tileOnEmptySpacePosition;

            tiles[emptySpaceIndex] = tiles[tiles.Length - 1];
            tiles[tiles.Length - 1] = null;
            emptySpaceIndex = tiles.Length - 1;
        }

        do
        {
            for (int i = 0; i <= tiles.Length - 2; i++)
            {
                Vector2 lastPosition = tiles[i].GetTargetPosition();
                int randomIndex = Random.Range(0, tiles.Length - 2);

                tiles[i].SetTargetPosition(tiles[randomIndex].GetTargetPosition());
                tiles[randomIndex].SetTargetPosition(lastPosition);

                Tile tile = tiles[i];
                tiles[i] = tiles[randomIndex];
                tiles[randomIndex] = tile;
            }

            inversion = GetInversions();
            Debug.Log("Puzzle shuffled");
        } while (inversion % 2 != 0);
       
    }

    private int FindIndex(Tile tile)
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i] != null)
            {
                if(tiles[i] == tile)
                {
                    return i;
                }
            }
        }

        return -1;
    }

    private int GetInversions()
    {
        int inversionsSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            int thisTileInvertion = 0;
            for (int j = i; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].GetNumber() > tiles[j].GetNumber())
                    {
                        thisTileInvertion++;
                    }
                }
            }
            inversionsSum += thisTileInvertion;
        }
        return inversionsSum;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _debugSquarePrefab;
    private List<GameObject> _debugSquares = new List<GameObject>();

    [SerializeField] private float _spawnSpeed;
    [SerializeField] private GameObject[] _boxPrefabs;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _boxSpeed;

    [SerializeField] private bool _drawDebug;


    public GameObject[,] Board;
    bool _gameRunning = false;

    private void Start()
    {
        Board = new GameObject[12, 6];
    }

    private void Update()
    {
        if (_drawDebug)
        {
            DrawBoard();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _gameRunning = true;
            StartCoroutine(SpawnBox());
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            _gameRunning = false;
            StopAllCoroutines();
        }
    }

    IEnumerator SpawnBox()
    {
        while (_gameRunning)
        {
            GameObject box = Instantiate(_boxPrefabs[Random.Range(0, 7)], _spawnPoints[Random.Range(0, 2)].position, Quaternion.identity);
            box.transform.SetParent(GameObject.Find("Boxes").transform, true);
            box.GetComponent<BoxController>().speed = _boxSpeed;
            yield return new WaitForSeconds(_spawnSpeed);
        }
    }

    public bool IsFieldEmpty(int x, int y)
    {
        return Board[x, y] == null;
    }

    public void AddBoxToBoard(GameObject box, int x, int y)
    {
        Board[x, y] = box;
        CheckForMatch(x, y);
        CheckForLine();
    }

    public void RemoveFromBoard(int x, int y)
    {
        if (x > 11 || y > 5) return;
        Board[x,y] = null;
    }

    private void DrawBoard()
    {
        foreach(GameObject square in _debugSquares)
        {
            Destroy(square);
        }
        for(int i=0; i<12; i++)
        {
            for(int j=0; j<6; j++)
            {
                if(Board[i,j] != null)
                {
                    _debugSquares.Add(Instantiate(_debugSquarePrefab, new Vector3(i, j), Quaternion.identity));
                }
            }
        }
    }

    public void CheckForMatch(int x, int y)
    {
        int counter = 1;
        Box.Color color = Board[x,y].GetComponent<BoxController>().box.color;
        List<GameObject> toDestroy = new List<GameObject>();
        toDestroy.Add(Board[x,y]);
        for(int i = x-1; i >= 0; i--)
        {
            if (Board[i, y] == null) break;
            if(Board[i,y].GetComponent<BoxController>().box.color == color)
            {
                counter++;
                toDestroy.Add(Board[i,y]);
            }
            else
            {
                break;
            }
        }
        for (int i = x+1; i < 12; i++)
        {
            if (Board[i, y] == null) break;
            if (Board[i, y].GetComponent<BoxController>().box.color == color)
            {
                counter++;
                toDestroy.Add(Board[i, y]);
            }
            else
            {
                break;
            }
        }
        for (int i = y-1; i >= 0; i--)
        {
            if (Board[x, i] == null) break;
            if (Board[x, i].GetComponent<BoxController>().box.color == color)
            {
                counter++;
                toDestroy.Add(Board[x, i]);
            }
            else
            {
                break;
            }
        }
        for (int i = y+1; i < 6; i++)
        {
            if (Board[x, i] == null) break;
            if (Board[x, i].GetComponent<BoxController>().box.color == color)
            {
                counter++;
                toDestroy.Add(Board[x, i]);
            }
            else
            {
                break;
            }
        }
        if(counter >= 3)
        {
            foreach(GameObject box in toDestroy)
            {
                Destroy(box);
            }
        }
        else
        {
            toDestroy.Clear();
        }
    }

    private void CheckForLine()
    {
        List<GameObject> toDestroy = new List<GameObject>();
        for (int i=0; i<12; i++)
        {
            if(Board[i,0] != null)
            {
                toDestroy.Add(Board[i, 0]);
            }
        }
        if(toDestroy.Count == 12)
        {
            foreach(GameObject box in toDestroy)
            {
                Destroy(box);
            }
            for(int i=0; i<12; i++)
            {
                Board[i,0] = null;
            }
        }
        toDestroy.Clear();
    }
}

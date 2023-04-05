using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private GameObject _gameManager;

    public Box box;

    private Vector3 _dropPosition;

    public bool isMoving;

    public float speed;

    public float testx, testy;

    public string canMove;

    private void Start()
    {
        _dropPosition = GetRandomDropPosition();

        _gameManager = GameObject.Find("GameManager");

        StartCoroutine(MoveToDropPosition());
    }

    private void Update()
    {
        if(!isMoving && CanMove(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y-1)))
        {
            StartCoroutine(MoveBox("Vertical", -1));
        }
    }

    private Vector3 GetRandomDropPosition()
    {
        return new Vector3(Random.Range(0, 12), 6);
    }

    private void PlaceBox(float x, float y)
    {
        _gameManager.GetComponent<GameManager>().AddBoxToBoard(gameObject, Mathf.RoundToInt(x), Mathf.RoundToInt(y));
    }

    private void DeleteBox(float x, float y)
    {
        _gameManager.GetComponent<GameManager>().RemoveFromBoard(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
    }

    IEnumerator MoveToDropPosition()
    {
        isMoving = true;
        while(transform.position != _dropPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _dropPosition, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        isMoving = false;
        if (!CanMove(_dropPosition.x, _dropPosition.y - 1))
        {
            Destroy(gameObject);
        }
    }


    public IEnumerator MoveBox(string direction, int value)
    {
        isMoving = true;
        Vector3 newPosition;
        Vector3 oldPosition = transform.position;

        switch (direction)
        {
            case "Horizontal":
                newPosition = transform.position + new Vector3(value, 0);
                break;
            case "Vertical":
                newPosition = transform.position + new Vector3(0, value);
                break;
            default:
                newPosition = transform.position;
                break;
        }

        if (!CanMove(newPosition.x, newPosition.y))
        {
            isMoving = false;
            yield break;
        }

        DeleteBox(oldPosition.x, oldPosition.y);
        PlaceBox(newPosition.x, newPosition.y);

        while(transform.position != newPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        isMoving = false;
    }

    private bool CanMove(float x, float y)
    {
        x = Mathf.Round(x);
        y = Mathf.Round(y);
        if (y < 0) return false;
        if (x < 0 || x > 11) return false;
        if (!_gameManager.GetComponent<GameManager>().IsFieldEmpty(Mathf.RoundToInt(x), Mathf.RoundToInt(y))) return false;

        return true;
    }
}

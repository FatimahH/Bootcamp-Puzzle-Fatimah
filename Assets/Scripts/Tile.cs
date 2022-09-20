using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private int number;

    private Vector2 targetPosition;
    private Vector2 correctPosition;
    private SpriteRenderer sprite;

    private void Awake()
    {
        targetPosition = transform.position;
        correctPosition = transform.position;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, targetPosition, 0.05f);

        if(targetPosition == correctPosition)
        {
            sprite.color = Color.green;
        }
        else
        {
            sprite.color = Color.white;
        }
    }
    public void MoveTile(Vector2 emptySpace)
    {
        targetPosition = emptySpace;
    }

    public Vector2 GetTargetPosition()
    {
        return targetPosition;
    }

    public void SetTargetPosition(Vector2 position)
    {
        targetPosition = position;
    }

    public int GetNumber()
    {
        return number;
    }
}

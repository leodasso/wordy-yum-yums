using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

	public enum Direction {
		None,
		Up,
		Right,
		Down,
		Left,
	}
	
	public float snakeSpeed = 5;
	public float speedPerBodySegment = .5f;

	public Direction direction;

	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.A)) TryGoLeft();
		if (Input.GetKey(KeyCode.D)) TryGoRight();
		if (Input.GetKey(KeyCode.W)) TryGoUp();
		if (Input.GetKey(KeyCode.S)) TryGoDown();

		transform.Translate(DirectionToVector(direction) * Time.deltaTime * snakeSpeed);
    }

	bool Horizontal => direction == Direction.Right || direction == Direction.Left;
	bool Vertical => direction == Direction.Up || direction == Direction.Down;

	void TryGoLeft() {
		if (Horizontal) return;
		direction = Direction.Left;
	}

	void TryGoRight() {
		if (Horizontal) return;
		direction = Direction.Right;
	}

	void TryGoUp() {
		if (Vertical) return;
		direction = Direction.Up;
	}

	void TryGoDown() {
		if (Vertical) return;
		direction = Direction.Down;
	}

	static Vector2 DirectionToVector (Direction input) {
		switch (input) {
			case Direction.Up: return Vector2.up;
			case Direction.Down: return Vector2.down;
			case Direction.Left: return Vector2.left;
			case Direction.Right: return Vector2.right;
			case Direction.None: return Vector2.zero;
			default: return Vector2.right;
		}
	}
}

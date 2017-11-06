using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpNeat;

public enum PlayersMarks
{
	empty,
	x,
	o
}

public class Board
{
	public PlayersMarks[,] squares = new PlayersMarks[3, 3];

	public void MarkSquare(int x, int y, PlayersMarks mark)
	{
		squares[x, y] = mark;
	}
}

public interface IPlayer
{
	PlayersMarks assignedMark { get; set; }
	System.Tuple<int,int> GetMove (Board currentBoard);
}

public class RandomPlayer : IPlayer
{
	public PlayersMarks assignedMark { get; set; }

	public System.Tuple<int,int> GetMove (Board currentBoard)
	{
		int numberOfRows = currentBoard.squares.GetLength(0);
		int numberOfCols = currentBoard.squares.GetLength(1);

		int xPos = Random.Range (0, numberOfRows - 1);
		int yPos = Random.Range (0, numberOfCols - 1);
		System.Tuple<int,int> position = new System.Tuple<int,int>(xPos, yPos);

		return position;
	}
}

public class TicTacToe : MonoBehaviour
{
	Board gameBoard = new Board();

	RandomPlayer rando = new RandomPlayer();

	public void Start()
	{
		System.Tuple<int, int> randosMove = rando.GetMove(gameBoard);
	}
}

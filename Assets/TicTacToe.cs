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
	public PlayersMarks[,] squares = new PlayersMarks[3, 3]; // instantiating a multidimensional array that accepts PlayersMarks as the values

	public bool MarkSquare(int x, int y, PlayersMarks mark)
	{
		if(squares [x, y] == PlayersMarks.empty)
		{
			squares[x, y] = mark;
			return true;
		}
		return false;
	}
}

public interface IPlayer
{
	PlayersMarks assignedMark { get; set; } // you can't have variables in an interface but you can have properties; the get; set; syntax makes assignedMark an assignable property
	System.Tuple<int,int> GetMove(Board currentBoard);
}

public class RandomPlayer : IPlayer
{
	public PlayersMarks assignedMark { get; set; }

	public System.Tuple<int,int> GetMove(Board currentBoard)
	{
		int numberOfRows = currentBoard.squares.GetLength(0);
		int numberOfCols = currentBoard.squares.GetLength(1);

		int xPos = Random.Range(0, numberOfRows);
		int yPos = Random.Range(0, numberOfCols);
		System.Tuple<int,int> position = new System.Tuple<int,int>(xPos, yPos);

		return position;
	}
}

public class TicTacToe : MonoBehaviour
{
	Board gameBoard = new Board();

	RandomPlayer rando0 = new RandomPlayer();
	RandomPlayer rando1 = new RandomPlayer();

	public void Start()
	{
		rando0.assignedMark = PlayersMarks.x;
		rando1.assignedMark = PlayersMarks.o;

		bool winner = false;

		while(winner == false)
		{
			winner = takeTurn(rando0);
			Debug.Log("winner: " + winner);
		}

	}

	public bool winningMove(int xPos, int yPos)
	{
		int yLength = gameBoard.squares.GetLength(0);
		int xLength = gameBoard.squares.GetLength(1);
		bool xAxisWins = true;
		bool yAxisWins = true;

		Debug.Log("randosMove: " + gameBoard.squares[xPos, yPos]);
		for(int square = 0; square < yLength; square++)
		{
			Debug.Log("gameBoard.squares[xPos, square]: " + gameBoard.squares[xPos, square]);
			if(gameBoard.squares[xPos, square] != gameBoard.squares[xPos, yPos])
			{
				yAxisWins = false;
			}
		}

		if(yAxisWins == true)
		{
			return true;
		}

		else
		{
			for(int square = 0; square < xLength; square++)
			{
				if(gameBoard.squares[square, yPos] != gameBoard.squares[xPos, yPos])
				{
					xAxisWins = false;
				}
			}
		}

		if(xAxisWins == true)
		{
			return true;
		}

//		else // Todo: diagonals
//		{
//		}	

		return false;
	}

	public bool takeTurn(RandomPlayer rando)
	{
		bool iWon = false;
		bool turnComplete = false;

		while(turnComplete == false)
		{
			System.Tuple<int, int> randosMove = rando.GetMove(gameBoard);
			Debug.Log("randosMove: " + randosMove);

			turnComplete = gameBoard.MarkSquare(randosMove.Item1, randosMove.Item2, rando.assignedMark);

			Debug.Log(gameBoard.squares[0,0] + ", " + gameBoard.squares[1,0] + ", " + gameBoard.squares[2,0]);
			Debug.Log(gameBoard.squares[0,1] + ", " + gameBoard.squares[1,1] + ", " + gameBoard.squares[2,1]);
			Debug.Log(gameBoard.squares[0,2] + ", " + gameBoard.squares[1,2] + ", " + gameBoard.squares[2,2]);

			if(turnComplete == true)
			{
				iWon = winningMove(randosMove.Item1, randosMove.Item2);
			}
		}

		return iWon;
	}
}

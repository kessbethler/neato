using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpNeat;

public class TicTacToe : MonoBehaviour
{
	public enum SquareTypes
	{
		empty,
		x,
		o
	}

	public class Move
	{
		int x;
		int y;
	}

	public SquareTypes[,] Board { get; set; }

	void Start()
	{
		Board = new SquareTypes[3, 3];
	}

	public interface IPlayer
	{
		Move GetMove(SquareTypes[,] board);
	}
}

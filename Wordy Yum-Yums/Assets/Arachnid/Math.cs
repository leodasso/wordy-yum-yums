﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arachnid
{
	public static class Math
	{
		/// <summary>
		/// Returns the positive position of the y coordinate of a circle given the circle's radius and x position.
		/// </summary>
		/// <param name="radius">Circle's radius in real units</param>
		/// <param name="x">The x position on the circle, where 0 is the origin, and 1 is the farthest right on the circle</param>
		public static float PositionYOfCircle(float radius, float x)
		{
			return Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(radius * x, 2));
		}

		/// <summary>
		/// Returns the euler angle of the given vector.
		/// </summary>
		/// <param name="degreesOffset">Degrees to offset the result by</param>
		public static float AngleFromVector2(Vector2 vector, float degreesOffset)
		{
			Vector2 rotatedVector = Quaternion.Euler(0, 0, degreesOffset) * vector;
			float rad = Mathf.Atan2(rotatedVector.y, rotatedVector.x);
			return Mathf.Rad2Deg * rad;
		}
		
		public static Vector2 RadiansToVector2(float radian)
		{
			return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
		}
  
		public static Vector2 DegreeToVector2(float degree)
		{
			return RadiansToVector2(degree * Mathf.Deg2Rad);
		}

		/// <summary>
		/// Given the input, returns a number that's rounded to the nearest increment. For example, given an input
		/// of 7.55 and an increment of 5, will round to 10. An input of 6 with increment 5 will round to 5. Only accepts
		/// positive rounding increments.
		/// </summary>
		public static float RoundToNearest(float input, float roundingIncrement)
		{
			roundingIncrement = Mathf.Abs(roundingIncrement);
			
			// failsafe for rounding increments near 0
			if (roundingIncrement <= Mathf.Epsilon) return input;
			
			float remainder = Mathf.Abs(input) % roundingIncrement;
			float half = roundingIncrement / 2;

			if (input >= 0)
			{
				if (remainder < half)
					return input - remainder;
				else
					return input + roundingIncrement - remainder;
			}

			// negative numbers are fun
			if (remainder < half)
				return input + remainder;
			else
				return input - roundingIncrement + remainder;
			
		}

		/// <summary>
		/// Given a vector of a direction in 2D space (x, y) returns a vector that's the 2D
		/// vector projected on to the ground. Useful for taking a Vector2 input (like gamepad joystick)
		/// and returning a vector that will move an object along the ground.
		/// </summary>
		public static Vector3 Project2Dto3D(Vector2 flatVector)
		{
			return new Vector3(flatVector.x, 0, flatVector.y);
		}

		/// <summary>
		/// Returns a random element of the given list.
		/// </summary>
		public static T RandomElementOfList<T>(List<T> list)
		{
			int selectedIndex = Random.Range(0, list.Count);
			return list[selectedIndex];
		}

		public static bool LayerMaskContainsLayer(LayerMask layerMask, int layer)
		{
			return layerMask == (layerMask | (1 << layer));
		}

		/// <summary>
		/// Given any arbitrary angle, returns that same angle but expressed as a number between 0 and 360
		/// </summary>
		public static float Angle0to360(float inputAngle)
		{
			if (inputAngle >= 0) return inputAngle % 360;

			inputAngle = Mathf.Abs(inputAngle);
			float remainder = inputAngle % 360;
			return 360 - remainder;
		}
	}
}
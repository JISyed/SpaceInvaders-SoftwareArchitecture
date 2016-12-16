using Debug = System.Diagnostics.Debug;
using Random = System.Random;
using Console = System.Console;

namespace SpaceInvaders
{
	static class Randomizer
	{
		private static Random generator;

		/// <summary>
		///		Initialize the random number generator
		/// </summary>
		public static void Initialize()
		{
			Randomizer.generator = new Random();
		}

		/// <summary>
		///		Returns a float between 0.0 (inclusive) and 1.0 (exclusive)
		/// </summary>
		/// <returns></returns>
		public static float RandomNormalizedRange()
		{
			double generatedNumber = generator.NextDouble();
			return (float) generatedNumber;
		}

		/// <summary>
		///		Returns an int between 0 and maxNumber-1
		/// </summary>
		/// <param name="maxNumber"></param>
		/// <returns></returns>
		public static int RandomInt(int maxNumber)
		{
			if(maxNumber == 0)
			{
				return 0;
			}
			else if(maxNumber < 0)
			{
				Debug.Assert(false, "Randomizer.RandomInt() cannot take negative parameter!");
				return 0;
			}

			return generator.Next(maxNumber);
		}

		/// <summary>
		///		Returns a uint between 0 and maxNumber-1
		/// </summary>
		/// <param name="maxNumber"></param>
		/// <returns></returns>
		public static uint RandomUnsignedInt(uint maxNumber)
		{
			if (maxNumber == 0u)
			{
				return 0u;
			}

			int generatedNumber = generator.Next((int)maxNumber);

			return (uint) generatedNumber;
		}

		/// <summary>
		///		Returns a float between 0 (inclusive) and maxNumber (exclusive)
		/// </summary>
		/// <param name="maxNumber"></param>
		/// <returns></returns>
		public static float RandomFloat(float maxNumber)
		{
			if (maxNumber == 0.0f)
			{
				return 0.0f;
			}
			else if (maxNumber < 0.0f)
			{
				Debug.Assert(false, "Randomizer.RandomFloat() cannot take negative parameter!");
				return 0;
			}
			
			// Get a number between 0.0f and 1.0f
			float factor = Randomizer.RandomNormalizedRange();

			// Multiply the factor with the max
			return maxNumber * factor;
		}

		/// <summary>
		///		Returns an int between min (inclusive) and max (exclusive)
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int RandomRange(int min, int max)
		{
			Debug.Assert(max >= min, "Randomizer.RandomRange(int) has _min_ bigger than _max_ !");

			if(min == max)
			{
				return min;
			}

			// Find a number between 0 and [max - min]
			int baseNumber = Randomizer.RandomInt(max - min);

			// Add min to the random number
			return baseNumber + min;
		}

		/// <summary>
		///		Returns a uint between min (inclusive) and max (exclusive)
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static uint RandomRange(uint min, uint max)
		{
			Debug.Assert(max >= min, "Randomizer.RandomRange(uint) has _min_ bigger than _max_ !");

			if (min == max)
			{
				return min;
			}

			// Find a number between 0 and [max - min]
			uint baseNumber = Randomizer.RandomUnsignedInt(max - min);

			// Add min to the random number
			return baseNumber + min;
		}

		/// <summary>
		///		Returns a float between min (inclusive) and max (exclusive)
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static float RandomRange(float min, float max)
		{
			Debug.Assert(max >= min, "Randomizer.RandomRange(float) has _min_ bigger than _max_ !");

			if (min == max)
			{
				return min;
			}

			// Find a number between 0 and [max - min]
			float baseNumber = Randomizer.RandomFloat(max - min);

			// Add min to the random number
			return baseNumber + min;
		}
	}
}

using Debug = System.Diagnostics.Debug;

namespace SpaceInvaders
{
	/// <summary>
	///		Utility static class for floating point math
	/// </summary>
	static class Mathf
	{
		/// <summary>
		///		Returns the absolute value of the given float
		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>
		static public float Abs(float n)
		{
			// If negative, make positive
			if(n < 0.0f)
			{
				return -n;
			}

			// Just return it if positive
			return n;
		}

		/// <summary>
		///		Returns number^power
		/// </summary>
		/// <param name="number"></param>
		/// <param name="power"></param>
		/// <returns></returns>
		static public float Pow(float number, uint power)
		{
			float total = 1.0f;

			// n^0 == 1
			if(power == 0)
			{
				return total;
			}


			for(uint i = 1; i <= power; i++)
			{
				total *= number;
			}

			return total;
		}
	}
}

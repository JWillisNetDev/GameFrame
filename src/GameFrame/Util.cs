namespace GameFrame;

public static class Util
{
	public static int ClampInt32(int value, int min, int max)
	{
		if (value < min)
		{
			return min;
		}
		else if (value > max)
		{
			return max;
		}
		return value;
	}
}

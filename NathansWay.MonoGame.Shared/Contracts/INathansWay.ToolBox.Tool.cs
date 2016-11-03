#region Using Statements

using Microsoft.Xna.Framework;

#endregion

namespace NathansWay.MonoGame.Shared
{
	public interface ITool
	{
		E__ToolBoxToolz ToolType { get; }
		Game MainGame { get; }
	}
}


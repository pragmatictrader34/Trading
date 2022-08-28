// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript.DrawingTools
{
    /// <summary>
    /// Indicates the current state of the drawing tool to perform various actions, such as building, editing, or moving.
    /// </summary>
    public enum DrawingState
    {
        /// <summary>
        /// The initial state when a drawing tool is first being drawn, allowing for the anchors to be set for the drawing.
        /// </summary>
        Building,
        /// <summary>
        /// Allows for changing the values of any of the drawing tools anchors
        /// </summary>
        Editing,
        /// <summary>
        /// The drawing tool is normal on the chart and is not in a state to allow for changes.
        /// </summary>
        Normal,
        /// <summary>The entire drawing tool to be moved by a user.</summary>
        Moving,
    }
}

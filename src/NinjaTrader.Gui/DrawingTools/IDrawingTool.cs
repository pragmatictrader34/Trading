using NinjaTrader.Gui.Chart;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript.DrawingTools
{
    /// <summary>
    /// Represents an interface that exposes information regarding a drawn chart object.
    /// </summary>
    [CLSCompliant(false)]
    public interface IDrawingTool
    {
        /// <summary>
        /// A read-only collection of all of the IDrawingTool's ChartAnchors
        /// </summary>
        IEnumerable<ChartAnchor> Anchors { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        /// <summary>The current DrawingState of the drawing tool</summary>
        DrawingState DrawingState { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        /// <summary>
        /// An object value indicating which type of NinjaScript the drawing tool originated (null if user drawn)
        /// </summary>
        NinjaScriptBase DrawnBy { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        string Id { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        /// <summary>
        /// A read-only bool determining if the drawing tool can be interacted with by the user.
        /// </summary>
        bool IgnoresUserInput { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        /// <summary>
        /// A bool determining if the drawing tool displays on all charts of the instrument
        /// </summary>
        bool IsGlobalDrawingTool { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        /// <summary>A bool determining if the drawing tool can be moved</summary>
        bool IsLocked { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        /// <summary>
        /// A read-only bool indicating if the drawing tool is attached to an indicator or strategy
        /// </summary>
        bool IsAttachedToNinjaScript { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        bool IsAttachedToVisible { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        /// <summary>
        /// A read-only bool indicating if drawing tool was manually drawn by a user
        /// </summary>
        bool IsUserDrawn { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        /// <summary>
        /// A bool determining if the drawing tool will reside on a different ZOrder from the NinjaScript object it was drawn
        /// </summary>
        bool IsSeparateZOrder { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        void OnBarsChanged();

        /// <summary>
        /// An int value representing the panel the drawing tool resides
        /// </summary>
        int PanelIndex { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        State State { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        void SetState(State state);

        ScaleJustification ScaleJustification { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        /// <summary>
        /// A string value representing the unique ID of the draw object. (Global draw objects will have an "@" added as a prefix to the string)
        /// </summary>
        string Tag { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

        string Template { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        /// <summary>
        /// A read-only enum indicating the order in which the drawing tool will be drawn.
        /// </summary>
        DrawingToolZOrder ZOrderType { [MethodImpl(MethodImplOptions.NoInlining)] get; }

        /// <summary>
        /// A read-only bool indicating if the drawing tool can be used for creating an alert
        /// </summary>
        bool SupportsAlerts { [MethodImpl(MethodImplOptions.NoInlining)] get; }
    }
}

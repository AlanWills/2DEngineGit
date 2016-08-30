namespace CelesteEngine
{
    public delegate void OnPressed(BaseObject baseObject);

    /// <summary>
    /// An interface to describe objects which respond to being pressed either by the mouse or the touchpad.
    /// </summary>
    public interface IPressable
    {
        /// <summary>
        /// These represent a function which we want to execute when this object is pressed by the appropriate mouse button/touchpad.
        /// To do this, construct a function of the form:
        /// public void Function(Button button) { ... }
        /// 
        /// Then do OnClicked += Function;
        /// 
        /// In fact, more than one function can be set up in this way.
        /// </summary>
        event OnPressed OnLeftPressed;
        event OnPressed OnMiddlePressed;
        event OnPressed OnRightPressed;
        event OnPressed OnReleased;
    }
}

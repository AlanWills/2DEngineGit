using CelesteEngine;
using Microsoft.Xna.Framework;

namespace CelesteEngineCelesteEngineUnitTestGameProject
{
    /// <summary>
    /// A files which holds mock classes used to test abstract classes or provide a public interface to protected functions for testing
    /// </summary>

    /// <summary>
    /// We cannot instantiate component because it is private, so we create this test wrapper so that we can test the Component class.
    /// The constructor calls the LoadContent and Initiliase functions as they are empty in the Component class.
    /// </summary>
    public class MockComponent : Component
    {
        public MockComponent()
        {
            LoadContent();
            Initialise();
        }

        #region Wrappers for Extension Functions

        public void CheckAlive()
        {
            TestExtensionFunctions.CheckAlive(this);
        }

        public void CheckDead()
        {
            TestExtensionFunctions.CheckDead(this);
        }

        public void CheckHidden()
        {
            TestExtensionFunctions.CheckHidden(this);
        }

        #endregion
    }

    /// <summary>
    /// A class used to test templated functions which allow returning of an inherited classes derived from Component
    /// </summary>
    public class MockInheritedComponent : MockComponent { }

    /// <summary>
    /// A class used to test the abstract base screen class
    /// </summary>
    public class MockBaseScreen : BaseScreen { public MockBaseScreen(string screenDataAsset = "Test\\TestBaseScreenData") : base(screenDataAsset) { } }

    /// <summary>
    /// A class used to test the abstract base object class.
    /// LoadContent and Initialise are called in the constructor.
    /// </summary>
    public class MockBaseObject : BaseObject
    {
        public MockBaseObject() :
            this(Vector2.Zero)
        {

        }

        public MockBaseObject(Vector2 localPosition) :
            this(localPosition, AssetManager.DefaultEmptyPanelTextureAsset)
        {

        }

        public MockBaseObject(Vector2 localPosition, string textureAsset) : 
            this(Vector2.Zero, localPosition, textureAsset)
        {

        }

        public MockBaseObject(Vector2 size, Vector2 localPosition, string textureAsset) :
            base(size, localPosition, textureAsset)
        {
            LoadContent();
            Initialise();
        }
    }

    /// <summary>
    /// A class used to test the instance screenmanager class which will be running whilst we run the tests so unavailable itself for testing.
    /// </summary>
    public class MockScreenManager : ScreenManager
    {
        public MockScreenManager() { }

        public override void LoadContent()
        {
            
        }

        public override void Initialise()
        {

        }

        public override void Begin()
        {
            
        }
    }
}

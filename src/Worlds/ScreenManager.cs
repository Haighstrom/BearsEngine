using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaighFramework;

namespace BearsEngine.Worlds
{
    public class ScreenManager : IScene
    {
        private Screen _currentWorld;

        public ScreenManager(Screen initialWorld)
        {
            _currentWorld = initialWorld;
        }

        #region Properties
        public bool Active { get; set; } = true;

        public Screen ActiveWorld { get { return _currentWorld; } }

        public bool Visible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        #region Methods

        #region Start
        public void Start()
        {
            _currentWorld.Start();
        }
        #endregion

        #region OnResize
        public void OnResize(object sender, SizeEventArgs e)
        {
            _currentWorld.OnResize(sender, e);
        }
        #endregion

        #region Update
        public void Update(double elapsed)
        {
            _currentWorld.Update(elapsed);
        }
        #endregion

        #region Render
        public void Render(ref Matrix4 projection, ref Matrix4 modelView)
        {
            _currentWorld.Render(ref projection, ref modelView);
        }
        #endregion

        #region End
        public void End()
        {
            _currentWorld.End();
        }
        #endregion

        #region ChangeWorld
        public void ChangeWorld(Screen newWorld)
        {
            //if (newWorld == null)
            //    throw new Exception("Called ChangeWorld with no world to change to");

            //_currentWorld.End();
            //_currentWorld.UpdateLists();
            //if (_currentWorld.Autoclear)
            //    _currentWorld.ClearTweens();

            //_currentWorld = newWorld;

            //_currentWorld.Active = true;
            //_currentWorld.UpdateLists();

            //_currentWorld.Start();
            //_currentWorld.UpdateLists();
        }
        #endregion

        #endregion
    }
}
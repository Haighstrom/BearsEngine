using HaighFramework;

namespace BearsEngine.Worlds
{
    public class TabbedPanel : Entity
    {
        #region GetPanelRect
        private static IRect<float> GetPanelRect(IRect<float> fullPosition, Direction orientation, int edgeToPanelGap, int tabPanelOverlap)
        {
            Rect panelPos;
            switch (orientation)
            {
                case Direction.Up:
                    panelPos = new Rect(0, edgeToPanelGap, fullPosition.W, fullPosition.H - edgeToPanelGap);
                    break;
                case Direction.Right:
                    throw new NotImplementedException();
                //break;
                case Direction.Down:
                    throw new NotImplementedException();
                //break;
                case Direction.Left:
                    throw new NotImplementedException();
                //break;
                case Direction.None:
                    throw new ArgumentException("Direction cannot be None for TabbedPanel");
                default:
                    throw new ArgumentException();
            }

            return panelPos;
        }
        #endregion

        #region Fields
        private readonly Direction _orientation;
        private readonly int _edgeToPanelGap;
        private readonly int _tabPanelOverlap;
        private readonly int _firstTabShift;
        private readonly int _tabSpacing;
        private readonly int _lastTabToNewTabButtonSpace;
        private readonly int _spaceBetweenArrows;

        private Button _tabsBack, _tabsForward;
        private Button _createNewTabButton;
        private List<Tab> _tabs = new List<Tab>();
        private int _firstTabDisplayed = 0;
        #endregion

        #region Constructors
        #region With AddNewTab Button
        public TabbedPanel(int layer, Direction orientation, IRect<float> fullPosition, int edgeToPanelGap, int tabPanelOverlap, int firstTabShift, int tabSpacing, int lastTabToNewTabButtonSpace, int spaceBetweenArrows, IPoint<float> tabsBackAndForwardSize, Point tabAddButtonSize, UITheme theme, string panelGFX, string tabsBackArrow, string tabsForwardArrow, string addNewTabGFX, Func<Tab> addNewTabFn)
            : this(layer, orientation, fullPosition, edgeToPanelGap, tabPanelOverlap, firstTabShift, tabSpacing, lastTabToNewTabButtonSpace, spaceBetweenArrows, theme, new Panel(panelGFX, GetPanelRect(fullPosition, orientation, edgeToPanelGap, tabPanelOverlap).Size), new Image(tabsBackArrow, tabsBackAndForwardSize), new Image(tabsForwardArrow, tabsBackAndForwardSize), new Image(addNewTabGFX, tabAddButtonSize), addNewTabFn)
        {
        }
        #endregion

        #region With just tabs (no add, no arrows)
        public TabbedPanel(int layer, Direction orientation, IRect<float> fullPosition, int edgeToPanelGap, int tabPanelOverlap, int firstTabShift, int tabSpacing, UITheme theme, string panelGFX)
            : this(layer, orientation, fullPosition, edgeToPanelGap, tabPanelOverlap, firstTabShift, tabSpacing, theme, new Panel(panelGFX, GetPanelRect(fullPosition, orientation, edgeToPanelGap, tabPanelOverlap).Size))
        {
        }
        #endregion

        private TabbedPanel(int layer, Direction orientation, IRect<float> fullPosition, int edgeToPanelGap, int tabPanelOverlap, int firstTabShift, int tabSpacing, int lastTabToNewTabButtonSpace, int spaceBetweenArrows, UITheme theme, IGraphic panelGFX, IRectGraphic tabsBackArrow, IRectGraphic tabsForwardArrow, IRectGraphic tabAddButtonGraphic, Func<Tab> createNewTabFn)
            : this(layer, orientation, fullPosition, edgeToPanelGap, tabPanelOverlap, firstTabShift, tabSpacing, spaceBetweenArrows, theme, panelGFX, tabsBackArrow, tabsForwardArrow)
        {
            if (tabAddButtonGraphic == null || createNewTabFn == null)
                throw new HException("TabbedPanel.cs ctr: either tabAddButtonGraphic or createNewTabFn is null.");

            _lastTabToNewTabButtonSpace = lastTabToNewTabButtonSpace;

            Add(_createNewTabButton = new Button(1, tabAddButtonGraphic.Shift(_lastTabToNewTabButtonSpace, (edgeToPanelGap - tabAddButtonGraphic.H) / 2), tabAddButtonGraphic, theme, () => AddTab(createNewTabFn(), true)));
        }

        private TabbedPanel(int layer, Direction orientation, IRect<float> fullPosition, int edgeToPanelGap, int tabPanelOverlap, int firstTabShift, int tabSpacing, int spaceBetweenArrows, UITheme theme, IGraphic panelGFX, IRectGraphic tabsBackArrow, IRectGraphic tabsForwardArrow)
            : this(layer, orientation, fullPosition, edgeToPanelGap, tabPanelOverlap, firstTabShift, tabSpacing, theme, panelGFX)
        {
            if (tabsForwardArrow == null || tabsBackArrow == null)
                throw new HException("TabbedPanel.cs ctr: either tabsForwardArrow or tabsBackArrow is null.");

            _spaceBetweenArrows = spaceBetweenArrows;

            Add(_tabsForward = new Button(1, tabsForwardArrow, theme, TabForward)
            {
                Visible = false,
                Active = false,
                X = Panel.W - tabsForwardArrow.W - _spaceBetweenArrows,
                Y = (_edgeToPanelGap - tabsForwardArrow.H) / 2,
            });

            Add(_tabsBack = new Button(1, tabsBackArrow, theme, TabBack)
            {
                Visible = false,
                Active = false,
                X = _tabsForward.X - tabsBackArrow.W - _spaceBetweenArrows,
                Y = (_edgeToPanelGap - tabsBackArrow.H) / 2,
            });
        }

        private TabbedPanel(int layer, Direction orientation, IRect<float> fullPosition, int edgeToPanelGap, int tabPanelOverlap, int firstTabShift, int tabSpacing, UITheme theme, IGraphic panelGFX)
            : base(layer, fullPosition)
        {
            _orientation = orientation;
            _edgeToPanelGap = edgeToPanelGap;
            _tabPanelOverlap = tabPanelOverlap;
            _firstTabShift = firstTabShift;
            _tabSpacing = tabSpacing;

            Add(Panel = new Entity(1, GetPanelRect(this, _orientation, _edgeToPanelGap, _tabPanelOverlap), panelGFX));
        }
        #endregion

        #region Properties
        public Entity Panel { get; private set; }

        public IReadOnlyList<Tab> Tabs => _tabs;

        public int TabsCount => _tabs.Count;

        public Tab CurrentTab { get; private set; }

        private float MaximumDistanceTabsCanBe => _tabsBack != null ? _tabsBack.Left : Panel.W - _spaceBetweenArrows;
        #endregion

        #region Methods
        #region AddTabs
        public void AddTabs(Tab t1, params Tab[] moreTabs)
        {
            AddTab(t1, true);

            foreach (var t in moreTabs)
                AddTab(t, false);
        }
        #endregion

        #region AddTab
        public void AddTab(Tab t, bool activate)
        {
            _tabs.Add(t);
            Add(t);

            t.TPParent = this;

            foreach (var a in t.Items)
                Panel.Add(a);

            t.LeftClicked += (s, e) => SwitchTo((Tab)s);

            if (activate)
            {
                CurrentTab?.Deactivate();
                CurrentTab = t;
                t.Activate();
            }
            else
                t.Deactivate();

            RepositionTabs();

            //if we activated the tab but it's created off-screen, press the "right" button until you can see it
            if (activate) 
                while (!t.Visible)
                {
                    _firstTabDisplayed++;
                    RepositionTabs();
                }
        }
        #endregion

        #region RemoveTab
        public void RemoveTab(Tab tab)
        {
            tab.Deactivate();
            tab.Remove();

            int index = _tabs.IndexOf(tab);
            
            _tabs.Remove(tab);

            RepositionTabs();

            //if we deleted the active tab, activate either the next tab along or the previous, if there is one
            if (tab == CurrentTab)
            {
                if (_tabs.Count != 0)
                {
                    CurrentTab = _tabs[index < _tabs.Count ? index : index - 1];
                    CurrentTab.Activate();
                }
                else
                    CurrentTab = null;
            }
        }
        #endregion

        #region Resize
        public void Resize(Point size) => Resize(size.X, size.Y);

        public void Resize(float newW, float newH)
        {
            W = newW;
            H = newH;
            Panel.R = GetPanelRect(this, _orientation, _edgeToPanelGap, _tabPanelOverlap);

            if (_tabsForward != null)
            {
                _tabsForward.X = Panel.W - _tabsForward.W - _spaceBetweenArrows;
                _tabsBack.X = _tabsForward.X - _tabsBack.W - _spaceBetweenArrows;
            }

            RepositionTabs();
        }
        #endregion

        #region SwitchTo
        public void SwitchTo(Tab t)
        {
            if (!_tabs.Contains(t))
                throw new HException("Tabbed panel {0} does not contain Tab {1}", this, t);

            if (t == CurrentTab)
                return;

            CurrentTab?.Deactivate();
            CurrentTab = t;
            CurrentTab.Activate();
        }
        #endregion

        #region RepositionTabs
        private void RepositionTabs()
        {
            //todo: different orientations
            if (_orientation != Direction.Up)
                throw new NotImplementedException();

            if (_createNewTabButton != null)
                _createNewTabButton.X = _lastTabToNewTabButtonSpace;

            float x = _firstTabShift;

            for (int i = 0; i < Tabs.Count; ++i)
            {
                if (i < _firstTabDisplayed)
                {
                    Tabs[i].Visible = false;
                    Tabs[i].Active = false;
                }
                else
                {
                    Tabs[i].X = x;
                    x += Tabs[i].W + _tabSpacing;
                    if (Tabs[i].Right + _lastTabToNewTabButtonSpace + _createNewTabButton?.W >= MaximumDistanceTabsCanBe)
                    {
                        Tabs[i].Visible = false;
                        Tabs[i].Active = false;
                    }
                    else
                    {
                        Tabs[i].Visible = true;
                        Tabs[i].Active = true;
                        if (_createNewTabButton != null)
                            _createNewTabButton.X = Tabs[i].Right + _lastTabToNewTabButtonSpace;
                    }
                }
            }

            if (_tabsForward != null)
            {
                if (_firstTabDisplayed == 0)
                {
                    _tabsBack.Visible = false;
                    _tabsBack.Active = false;
                }
                else
                {
                    _tabsBack.Visible = true;
                    _tabsBack.Active = true;
                }

                if (Tabs.Count == 0 || _tabs.Last().Right + _lastTabToNewTabButtonSpace + _createNewTabButton?.W < MaximumDistanceTabsCanBe)
                {
                    _tabsForward.Visible = false;
                    _tabsForward.Active = false;
                }
                else
                {
                    _tabsForward.Visible = true;
                    _tabsForward.Active = true;
                }
            }

            //if we resized or deleted a tab and now there's space to fit another tab in, move left one
            if (_firstTabDisplayed > 0 && _tabs.Last().Right + _tabSpacing + _tabs[_firstTabDisplayed - 1].W + _lastTabToNewTabButtonSpace + _createNewTabButton?.W < MaximumDistanceTabsCanBe)
            {
                _firstTabDisplayed--;
                RepositionTabs();
            }
        }
        #endregion

        #region TabForward
        private void TabForward()
        {
            if (_tabs.Last().Right + _lastTabToNewTabButtonSpace + _createNewTabButton?.W < MaximumDistanceTabsCanBe)
                return;

            _firstTabDisplayed++;
            RepositionTabs();
        }
        #endregion

        #region TabBack
        private void TabBack()
        {
            if (_firstTabDisplayed == 0)
                return;

            _firstTabDisplayed--;
            RepositionTabs();
        }
        #endregion
        #endregion
    }
}
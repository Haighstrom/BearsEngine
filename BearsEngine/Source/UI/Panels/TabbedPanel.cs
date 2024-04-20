namespace BearsEngine.UI;

public class TabbedPanel : Entity
{
    private static Rect GetPanelRect(Rect fullPosition, Direction orientation, int edgeToPanelGap, int tabPanelOverlap)
    {
        var panelPos = orientation switch
        {
            Direction.Up => new Rect(0, edgeToPanelGap, fullPosition.W, fullPosition.H - edgeToPanelGap),
            Direction.Right => throw new NotImplementedException(),
            //break;
            Direction.Down => throw new NotImplementedException(),
            //break;
            Direction.Left => throw new NotImplementedException(),
            //break;
            _ => throw new ArgumentException(),
        };
        return panelPos;
    }
    

    private readonly Direction _orientation;
    private readonly int _edgeToPanelGap;
    private readonly int _tabPanelOverlap;
    private readonly int _firstTabShift;
    private readonly int _tabSpacing;
    private readonly int _lastTabToNewTabButtonSpace;
    private readonly int _spaceBetweenArrows;

    private readonly Button _tabsBack, _tabsForward;
    private readonly Button _createNewTabButton;
    private readonly List<Tab> _tabs = new();
    private int _firstTabDisplayed = 0;
    

    public TabbedPanel(float layer, Direction orientation, Rect fullPosition, int edgeToPanelGap, int tabPanelOverlap, int firstTabShift, int tabSpacing, int lastTabToNewTabButtonSpace, int spaceBetweenArrows, Point tabsBackAndForwardSize, Point tabAddButtonSize, UITheme theme, string panelGFX, string tabsBackArrow, string tabsForwardArrow, string addNewTabGFX, Func<Tab> addNewTabFn)
        : this(layer, orientation, fullPosition, edgeToPanelGap, tabPanelOverlap, firstTabShift, tabSpacing, lastTabToNewTabButtonSpace, spaceBetweenArrows, theme, new Panel(panelGFX, GetPanelRect(fullPosition, orientation, edgeToPanelGap, tabPanelOverlap).Size), new Image(tabsBackArrow, tabsBackAndForwardSize), new Image(tabsForwardArrow, tabsBackAndForwardSize), new Image(addNewTabGFX, tabAddButtonSize), addNewTabFn)
    {
    }


    public TabbedPanel(float layer, Direction orientation, Rect fullPosition, int edgeToPanelGap, int tabPanelOverlap, int firstTabShift, int tabSpacing, UITheme theme)
        : this(layer, orientation, fullPosition, edgeToPanelGap, tabPanelOverlap, firstTabShift, tabSpacing, theme, new Image(theme.TabbedPanel.Panel.BackgroundColour, GetPanelRect(fullPosition, orientation, edgeToPanelGap, tabPanelOverlap).Size))
    {
    }

    public TabbedPanel(float layer, Direction orientation, Rect fullPosition, int edgeToPanelGap, int tabPanelOverlap, int firstTabShift, int tabSpacing, UITheme theme, string panelGFX)
        : this(layer, orientation, fullPosition, edgeToPanelGap, tabPanelOverlap, firstTabShift, tabSpacing, theme, new Panel(panelGFX, GetPanelRect(fullPosition, orientation, edgeToPanelGap, tabPanelOverlap).Size))
    {
    }
    

    private TabbedPanel(float layer, Direction orientation, Rect fullPosition, int edgeToPanelGap, int tabPanelOverlap, int firstTabShift, int tabSpacing, int lastTabToNewTabButtonSpace, int spaceBetweenArrows, UITheme theme, IGraphic panelGFX, IRectGraphic tabsBackArrow, IRectGraphic tabsForwardArrow, IRectGraphic tabAddButtonGraphic, Func<Tab> createNewTabFn)
        : this(layer, orientation, fullPosition, edgeToPanelGap, tabPanelOverlap, firstTabShift, tabSpacing, spaceBetweenArrows, theme, panelGFX, tabsBackArrow, tabsForwardArrow)
    {
        if (tabAddButtonGraphic == null || createNewTabFn == null)
            throw new Exception("TabbedPanel.cs ctr: either tabAddButtonGraphic or createNewTabFn is null.");

        _lastTabToNewTabButtonSpace = lastTabToNewTabButtonSpace;

        Add(_createNewTabButton = new Button(1, tabAddButtonGraphic.R.Shift(_lastTabToNewTabButtonSpace, (edgeToPanelGap - tabAddButtonGraphic.H) / 2), tabAddButtonGraphic, theme, () => AddTab(createNewTabFn(), true)));
    }

    private TabbedPanel(float layer, Direction orientation, Rect fullPosition, int edgeToPanelGap, int tabPanelOverlap, int firstTabShift, int tabSpacing, int spaceBetweenArrows, UITheme theme, IGraphic panelGFX, IRectGraphic tabsBackArrow, IRectGraphic tabsForwardArrow)
        : this(layer, orientation, fullPosition, edgeToPanelGap, tabPanelOverlap, firstTabShift, tabSpacing, theme, panelGFX)
    {
        if (tabsForwardArrow == null || tabsBackArrow == null)
            throw new Exception("TabbedPanel.cs ctr: either tabsForwardArrow or tabsBackArrow is null.");

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

    private TabbedPanel(float layer, Direction orientation, Rect fullPosition, int edgeToPanelGap, int tabPanelOverlap, int firstTabShift, int tabSpacing, UITheme theme, IGraphic panelGFX)
        : base(layer, fullPosition)
    {
        _orientation = orientation;
        _edgeToPanelGap = edgeToPanelGap;
        _tabPanelOverlap = tabPanelOverlap;
        _firstTabShift = firstTabShift;
        _tabSpacing = tabSpacing;

        Add(Panel = new Entity(1, GetPanelRect(R, _orientation, _edgeToPanelGap, _tabPanelOverlap), panelGFX));
    }
    
    public Entity Panel { get; private set; }

    public IReadOnlyList<Tab> Tabs => _tabs;

    public int TabsCount => _tabs.Count;

    public Tab CurrentTab { get; private set; }

    private float MaximumDistanceTabsCanBe => _tabsBack != null ? _tabsBack.R.Left : Panel.W - _spaceBetweenArrows;
    

    public void AddTabs(Tab t1, params Tab[] moreTabs)
    {
        AddTab(t1, true);

        foreach (var t in moreTabs)
            AddTab(t, false);
    }
    
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
    
    public void Resize(Point size) => Resize(size.X, size.Y);

    public void Resize(float newW, float newH)
    {
        W = newW;
        H = newH;
        var rect = GetPanelRect(R, _orientation, _edgeToPanelGap, _tabPanelOverlap);
        Panel.X = rect.X;
        Panel.Y = rect.Y;
        Panel.W = rect.W;
        Panel.H = rect.H;

        if (_tabsForward != null)
        {
            _tabsForward.X = Panel.W - _tabsForward.W - _spaceBetweenArrows;
            _tabsBack.X = _tabsForward.X - _tabsBack.W - _spaceBetweenArrows;
        }

        RepositionTabs();
    }
    
    public void SwitchTo(Tab t)
    {
        if (!_tabs.Contains(t))
            throw new Exception($"Tabbed panel {this} does not contain Tab {t}");

        if (t == CurrentTab)
            return;

        CurrentTab?.Deactivate();
        CurrentTab = t;
        CurrentTab.Activate();
    }
    
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
                if (Tabs[i].R.Right + _lastTabToNewTabButtonSpace + _createNewTabButton?.W >= MaximumDistanceTabsCanBe)
                {
                    Tabs[i].Visible = false;
                    Tabs[i].Active = false;
                }
                else
                {
                    Tabs[i].Visible = true;
                    Tabs[i].Active = true;
                    if (_createNewTabButton != null)
                        _createNewTabButton.X = Tabs[i].R.Right + _lastTabToNewTabButtonSpace;
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

            if (Tabs.Count == 0 || _tabs.Last().R.Right + _lastTabToNewTabButtonSpace + _createNewTabButton?.W < MaximumDistanceTabsCanBe)
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
        if (_firstTabDisplayed > 0 && _tabs.Last().R.Right + _tabSpacing + _tabs[_firstTabDisplayed - 1].W + _lastTabToNewTabButtonSpace + _createNewTabButton?.W < MaximumDistanceTabsCanBe)
        {
            _firstTabDisplayed--;
            RepositionTabs();
        }
    }
    
    private void TabForward()
    {
        if (_tabs.Last().R.Right + _lastTabToNewTabButtonSpace + _createNewTabButton?.W < MaximumDistanceTabsCanBe)
            return;

        _firstTabDisplayed++;
        RepositionTabs();
    }
    
    private void TabBack()
    {
        if (_firstTabDisplayed == 0)
            return;

        _firstTabDisplayed--;
        RepositionTabs();
    }
    
    
}
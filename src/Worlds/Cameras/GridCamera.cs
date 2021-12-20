using System;
using System.Collections.Generic;
using System.Linq;
using BearsEngine.Pathfinding;
using HaighFramework;

namespace BearsEngine.Worlds
{
    //public class GridCamera<N> : TerrainCamera
    //    where N : INode
    //{
    //    private P[] _terrainToPMap;

    //    #region Constructors
    //    #region Variable Tile Size Cameras
    //    public GridCamera(int mapW, int mapH, int defaultTerrain, string terrainSpriteSheetPath, int spriteSheetW, int spriteSheetH, int layer, IRect<float> position, float tileSizeW, float tileSizeH, Func<int,int,N> createNodeFunc, P[] terrainToPMap)
    //        : this(HF.Arrays.FillArray(mapW, mapH, defaultTerrain), defaultTerrain, terrainSpriteSheetPath, spriteSheetW, spriteSheetH, layer, position, tileSizeW, tileSizeH, createNodeFunc, terrainToPMap)
    //    {
    //    }
    //    public GridCamera(int[,] map, int defaultTerrain, string terrainSpriteSheetPath, int spriteSheetW, int spriteSheetH, int layer, IRect<float> position, float tileSizeW, float tileSizeH, Func<int, int, N> createNodeFunc, P[] terrainToPMap)
    //        : base(map, defaultTerrain, terrainSpriteSheetPath, spriteSheetW, spriteSheetH, layer, position, tileSizeW, tileSizeH)
    //    {
    //        SetUpNodeGrid(map.GetLength(0), map.GetLength(1), createNodeFunc, terrainToPMap);
    //    }
    //    #endregion

    //    #region Fixed Tile Size Cameras
    //    public GridCamera(int mapW, int mapH, int defaultTerrain, string terrainSpriteSheetPath, int spriteSheetW, int spriteSheetH, int layer, IRect<float> position, IRect<float> viewport, Func<int, int, N> createNodeFunc, P[] terrainToPMap)
    //        : this(HF.Arrays.FillArray(mapW, mapH, defaultTerrain), defaultTerrain, terrainSpriteSheetPath, spriteSheetW, spriteSheetH, layer, position, viewport, createNodeFunc, terrainToPMap)
    //    {
    //    }

    //    public GridCamera(int[,] map, int defaultTerrain, string terrainSpriteSheetPath, int spriteSheetW, int spriteSheetH, int layer, IRect<float> position, IRect<float> viewport, Func<int, int, N> createNodeFunc, P[] terrainToPMap)
    //        : base(map, defaultTerrain, terrainSpriteSheetPath, spriteSheetW, spriteSheetH, layer, position, viewport)
    //    {
    //        SetUpNodeGrid(map.GetLength(0), map.GetLength(1), createNodeFunc, terrainToPMap);
    //    }
    //    #endregion
    //    #endregion

    //    #region Indexers
    //    public override int this[int x, int y]
    //    {
    //        get => base[x, y];
    //        set
    //        {
    //            base[x, y] = value;
    //            Grid[x,y].PassType = _terrainToPMap[value];
    //        }
    //    }

    //    public override int this[Point p]
    //    {
    //        get => base[p];
    //        set
    //        {
    //            base[p.X, p.Y] = value;
    //            Grid[p.X, p.Y].PassType = _terrainToPMap[value];
    //        }
    //    }
    //    #endregion

    //    #region Properties
    //    public NodeGrid<N, P> Grid { get; private set; }
    //    #endregion

    //    #region Methods
    //    #region SetUpNodeGrid
    //    private void SetUpNodeGrid(int width, int height, Func<int, int, N> createNodeFunc, P[] terrainToPMap)
    //    {
    //        Grid = new NodeGrid<N, P>(width, height);
    //        _terrainToPMap = terrainToPMap;

    //        //create nodes
    //        for (int i = 0; i < width; ++i)
    //            for (int j = 0; j < height; ++j)
    //            {
    //                Grid[i, j] = createNodeFunc(i, j);
    //                Grid[i, j].PassType = terrainToPMap[SpriteMap[i, j]];
    //            }

    //        //connect nodes
    //        for (int i = 0; i < width; ++i)
    //            for (int j = 0; j < height; ++j)
    //            {
    //                if (Grid.IsInBounds(i - 1, j))
    //                    Grid[i, j].ConnectedNodes.Add(Grid[i - 1, j]);
    //                if (Grid.IsInBounds(i + 1, j))
    //                    Grid[i, j].ConnectedNodes.Add(Grid[i + 1, j]);
    //                if (Grid.IsInBounds(i, j - 1))
    //                    Grid[i, j].ConnectedNodes.Add(Grid[i, j - 1]);
    //                if (Grid.IsInBounds(i, j + 1))
    //                    Grid[i, j].ConnectedNodes.Add(Grid[i, j + 1]);
    //            }
    //    }
    //    #endregion

    //    #region Resize
    //    public void ResizeMap(int newW, int newH)
    //    {
    //        base.ResizeMap(newW, newH);
    //        throw new NotImplementedException();
    //    }

    //    public void ResizeMap(int newW, int newH, int newIndex)
    //    {
    //        base.ResizeMap(newW, newH, newIndex);
    //        throw new NotImplementedException();
    //    }
    //    #endregion

    //    #endregion
    //}
}
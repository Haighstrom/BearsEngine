using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearsEngine
{
    [Flags]
    public enum Direction
    {
        None =  0,
        Up =    1 << 0,
        Right = 1 << 1,
        Down =  1 << 2, 
        Left =  1 << 3,
        All =   Up | Right | Down | Left,
    }

    [Flags]
    public enum EightWayDirection
    {
        None =      0,
        Up =        1 << 0,
        UpRight =   1 << 1,
        Right =     1 << 2,
        DownRight = 1 << 3,
        Down =      1 << 4,
        DownLeft =  1 << 5,
        Left =      1 << 6,
        UpLeft =    1 << 7,
        All =       1 << 8 - 1,
    }

    public enum RotateDirection
    {
        Clockwise = 1,
        C = Clockwise,
        CounterClockwise = -1,
        CC = CounterClockwise,
    }
}
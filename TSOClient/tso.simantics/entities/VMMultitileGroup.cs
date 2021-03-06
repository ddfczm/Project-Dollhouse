﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tso.world.model;
using Microsoft.Xna.Framework;
using tso.world.components;

namespace TSO.Simantics.entities
{
    /// <summary>
    /// Ties multiple entities together with a common name and set of repositioning functions.
    /// </summary>
    public class VMMultitileGroup
    {
        public List<VMEntity> Objects = new List<VMEntity>();

        public void ChangePosition(short x, short y, sbyte level, Direction direction, VMContext context)
        {
            int Dir = 0;
            switch (direction)
            {
                case Direction.NORTH:
                    Dir = 0; break;
                case Direction.EAST:
                    Dir = 2; break;
                case Direction.SOUTH:
                    Dir = 4; break;
                case Direction.WEST:
                    Dir = 6; break;
            }

            for (int i = 0; i < Objects.Count(); i++)
            {
                var sub = Objects[i];
                var off = new Vector3((sbyte)(((ushort)sub.Object.OBJ.SubIndex) >> 8), (sbyte)(((ushort)sub.Object.OBJ.SubIndex) & 0xFF), 0);
                off = Vector3.Transform(off, Matrix.CreateRotationZ((float)(Dir * Math.PI / 4.0)));

                sub.Direction = direction;
                context.Blueprint.ChangeObjectLocation((ObjectComponent)sub.WorldUI, (short)Math.Round(x + off.X), (short)Math.Round(y + off.Y), (sbyte)level);
            }
            for (int i = 0; i < Objects.Count(); i++) Objects[i].PositionChange(context);
        }
    }
}

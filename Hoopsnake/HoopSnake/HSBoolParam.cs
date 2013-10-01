/**
 * 
 * Hoopsnake, version 0.6.6, for Grasshopper
 * Copyright (c) 2011-2013 Ioannis (Yannis) Chatzikonstantinou
 * http://yconst.com/software/hoopsnake/
 * 
 * This software is released under a 
 * Creative Commons Attribution-ShareAlike 3.0 Unported License
 * http://creativecommons.org/licenses/by-sa/3.0/
 * 
 */

using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Display;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;

namespace HoopSnake
{
    public class HSBoolParam : GH_Param<IGH_Goo>
    {
        private bool cond = true;
        HSComponent m_owner;
        public HSBoolParam(HSComponent nOwner)
            : base(new GH_InstanceDescription("Termination Condition", "B*", "Accepts either a boolean or number. \n If the boolean is false, or the current\n iterations count is equal or more than\n the input, the loop will stop.", "Params", "Primitive"))
        {
            this.m_owner = nOwner;
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("0fd89270-63ca-11e0-ae3e-0800200c9a66"); }
        }
        internal void collectVolatileData_Silent()
        {
            cond = true;
            foreach (IGH_Param par in this.Sources)
            {
                if (!cond) break;
                cond = isInLoop(par.VolatileData);
            }
            CollectVolatileData_FromSources();
        }

        internal bool isInLoop(IGH_Structure s)
        {
            foreach (GH_Path p in s.Paths)
            {
                foreach (IGH_Goo item in s.get_Branch(p))
                {
                    if (item is GH_Boolean && !((GH_Boolean)item).Value)
                    {
                        return false;
                    }
                    else if (item is GH_Number && m_owner.CStep >= ((GH_Number)item).Value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected override void CollectVolatileData_FromSources()
        {
            this.m_data.Clear();
            this.m_data.Append(new GH_Boolean(cond));
        }

        protected override void CollectVolatileData_Custom()
        {
            this.m_data.Clear();
            this.m_data.Append(new GH_Boolean(cond));
        }

        protected override IGH_Goo InstantiateT()
        {
            return new GH_ObjectWrapper();
        }

        public bool Cond
        {
            get
            {
                return cond;
            }
        }
    }
}

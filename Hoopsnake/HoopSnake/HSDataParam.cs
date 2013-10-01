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
    public class HSDataParam : GH_Param<IGH_Goo>
    {
        private GH_Structure<IGH_Goo> localCopy = new GH_Structure<IGH_Goo>();
        private GH_Structure<IGH_Goo> history = new GH_Structure<IGH_Goo>();
        private HSComponent m_owner;
        private GH_Param<IGH_Goo> m_param;

        public HSDataParam(HSComponent nOwner, GH_Param<IGH_Goo> nParam)
            : base(new GH_InstanceDescription("Data", "D*", "Represents a collection of generic data", "Params", "Primitive"))
        {
            this.m_owner = nOwner;
            this.m_param = nParam;
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("238dd090-62a4-11e0-ae3e-0800200c9a66"); }
        }

        //
        // Silently collect data from the input sources.
        //
        internal void collectVolatileData_Silent() {
            try
            {
                // Choose input.
                IGH_Structure orig = new GH_Structure<IGH_Goo>();
                if (this.Sources.Count > 0 && !isNullOrEmpty(this.Sources[0].VolatileData))
                {
                    orig = this.Sources[0].VolatileData;
                }
                else if (m_param.Sources.Count > 0 && !isNullOrEmpty(m_param.Sources[0].VolatileData))
                {
                    orig = m_param.Sources[0].VolatileData;
                }
                GH_Structure<IGH_Goo> TempCopy = DuplicateStructure(orig);

                localCopy = TempCopy.Duplicate();

                int minh = PathIndex(history, "min");
                int maxnew = PathIndex(TempCopy, "max");
                history = shiftPaths(history, Math.Max(0, maxnew - minh + 1), true);
                TempCopy.MergeStructure(history);
                history = TempCopy;

                this.m_data = localCopy.Duplicate();
            }
            catch(Exception e) {
                m_owner.attr.Panel.Message("err", m_owner.NickName, "Error while collecting data: " + e.Message);
            }
        }

        //
        // Duplicates a data structure.
        //
        internal GH_Structure<IGH_Goo> DuplicateStructure(IGH_Structure source)
        {
            GH_Structure<IGH_Goo> copy = new GH_Structure<IGH_Goo>();
            foreach (GH_Path p in source.Paths)
            {
                List<IGH_Goo> l = new List<IGH_Goo>();
                foreach (IGH_Goo item in source.get_Branch(p))
                {
                    if (item == null)
                    {
                        l.Add(null);
                    }
                    else
                    {
                        l.Add(item.Duplicate());
                    }
                }
                // New path
                copy.AppendRange(l, new GH_Path(p));
            }            
            return copy;
        }

        //
        // Shifts the input data structure's zero-index paths
        // by the specified amount of positions.
        // Warning: This may produce invalid structures!
        //
        internal GH_Structure<IGH_Goo> shiftPaths(GH_Structure<IGH_Goo> source, int positions, bool shiftZeroLength)
        {
            foreach (GH_Path p in source.Paths)
            {
                int[] tp = p.InternalPath;
                if (tp.Length > 0)
                {
                    tp[0] += positions;
                    p.InternalPath = tp;
                }
                else if (shiftZeroLength)
                {
                    p.InternalPath = new int[1] { positions };
                }
            }
            return source;
        }

        //
        // Gets the maximum or minimum zero-position path
        // index within a data structure.
        //
        internal int PathIndex(GH_Structure<IGH_Goo> source, string mode)
        {
            if (mode == "max")
            {
                int index = -1;
                foreach (GH_Path p in source.Paths)
                {
                    int[] tp = p.InternalPath;
                    if (tp.Length > 0 && tp[0] > index) index = tp[0];
                }
                return index;
            }
            else
            {
                int index = Int32.MaxValue;
                foreach (GH_Path p in source.Paths)
                {
                    int[] tp = p.InternalPath;
                    if (tp.Length > 0 && tp[0] < index) index = tp[0];
                }
                if (index == Int32.MaxValue) index = 0;
                return index;
            }
        }

        //
        // Tests whether a data structure is null / empty.
        // Returns false if not empty and has at least one
        // non-null data item.
        //
        internal bool isNullOrEmpty(IGH_Structure s)
        {
            if (!s.IsEmpty)
            {
                foreach (GH_Path p in s.Paths)
                {
                    foreach (IGH_Goo item in s.get_Branch(p))
                    {
                        if (item != null)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        protected override void CollectVolatileData_FromSources()
        {
            this.m_data = localCopy.Duplicate();
        }

        protected override void CollectVolatileData_Custom()
        {
            collectVolatileData_Silent();
        }

        internal void clearAllData()
        {
            localCopy = new GH_Structure<IGH_Goo>();
            history = new GH_Structure<IGH_Goo>();
            this.m_data = localCopy.Duplicate();
        }

        protected override IGH_Goo InstantiateT()
        {
            return new GH_ObjectWrapper();
        }

        public GH_Structure<IGH_Goo> Copy
        {
            get
            {
                return localCopy;
            }
        }
        public GH_Structure<IGH_Goo> History
        {
            get
            {
                return history;
            }
        }
    }
}

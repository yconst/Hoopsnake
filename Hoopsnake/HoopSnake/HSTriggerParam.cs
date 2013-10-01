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
using System.Collections;
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
    public class HSTriggerParam : GH_Param<IGH_Goo>
    {
        private HSComponent m_owner;
        private GH_Structure<IGH_Goo> copy = new GH_Structure<IGH_Goo>();
        private bool isTheSame = true;

        public HSTriggerParam(HSComponent nOwner)
            : base(new GH_InstanceDescription("Trigger", "T*", "Is input unchanged?", "Params", "Primitive"))
        {
            this.m_owner = nOwner;
            this.WireDisplay = GH_ParamWireDisplay.faint;
        }
        //
        // Check if structure has changed.
        //
        internal bool same(bool update)
        {
            if (this.Sources.Count == 0)
            {
                //m_owner.attr.Panel.Message(m_owner.NickName + ": One of the sources is empty");
                isTheSame = true;
                return true;
            }
            else
            {
                isTheSame = isStructureTheSame(this.Sources[0].VolatileData, copy);
                if (update && !isTheSame)
                {
                    copy = duplicateStructure(this.Sources[0].VolatileData);
                }
                return isTheSame;
            }
        }
        //
        // Compare two structures for equality.
        //
        internal bool isStructureTheSame(IGH_Structure struct1, IGH_Structure struct2)
        {
           // m_owner.attr.Panel.Message(m_owner.NickName + ": Compare:");
            if (struct1 == null ^ struct2 == null)
            {
                //m_owner.attr.Panel.Message("    One of the structures is null");
                return false;
            }
            if (struct1.IsEmpty ^ struct2.IsEmpty)
            {
                //m_owner.attr.Panel.Message("    One of the structures is empty");
                return false;
            }
            if (struct1 != null && struct2 != null)
            {
                int pc = Math.Min(struct1.Paths.Count, struct2.Paths.Count);
                for (int i = 0; i < pc; i++)
                {
                    IList b1 = struct1.get_Branch(struct1.Paths[i]);
                    IList b2 = struct2.get_Branch(struct2.Paths[i]);
                    int bc = Math.Min(b1.Count, b2.Count);
                    for (int j = 0; j < bc; j++)
                    {
                        if (b1[j] is GH_Integer && b2[j] is GH_Integer)
                        {
                            GH_Integer n1 = (GH_Integer)b1[j];
                            GH_Integer n2 = (GH_Integer)b2[j];
                            if (n1.Value != n2.Value) return false;
                        }
                        else if (b1[j] is GH_Number && b2[j] is GH_Number)
                        {
                            GH_Number n1 = (GH_Number)b1[j];
                            GH_Number n2 = (GH_Number)b2[j];
                            if (Math.Round(n1.Value,8) != Math.Round(n2.Value,8)) return false;
                        }
                    }
                }
            }
            return true;
        }
        //
        // Duplicate a structure.
        //
        internal GH_Structure<IGH_Goo> duplicateStructure(IGH_Structure source)
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
                copy.AppendRange(l, p);
            }
            return copy;
        }
        //
        // Check if a structure is null.
        //
        internal bool isNull(IGH_Structure s)
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
            this.m_data.Clear();
            this.m_data.Append(new GH_Boolean(isTheSame));
            if (m_owner.AutoUpdate)
            {
                // Remove and re-attach handler
                m_owner.Document.SolutionEnd -= m_owner.StartSolutionHandler;
                m_owner.Document.SolutionEnd += m_owner.StartSolutionHandler;
            }
        }
        protected override void CollectVolatileData_Custom() 
        {
            this.m_data.Clear();
            this.m_data.Append(new GH_Boolean(isTheSame));
        }
        internal void clearAllData()
        {
            copy.Clear();
            isTheSame = true;
            this.m_data.Clear();
            this.m_data.Append(new GH_Boolean(isTheSame));
        }
        protected override IGH_Goo InstantiateT()
        {
            return new GH_ObjectWrapper();
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("fa836630-c345-11e0-962b-0800200c9a66"); }
        }
    }
}

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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using System.Windows.Forms;
using GH_IO;

namespace HoopSnake
{
    class HSAttributes : Grasshopper.Kernel.Attributes.GH_ComponentAttributes
    {
        HSComponent m_owner;
        private HSCPanel panel;

        public HSAttributes(IGH_Component nComponent)
            : base(nComponent)
        {
            m_owner = (HSComponent)nComponent; 
        }

        public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            SetupPanel();
            if (!panel.Visible)
            {
                panel.Show(GH_InstanceServer.DocumentEditor);
            }
            panel.UpdateComboBox();
            panel.HSCombo.SelectedItem = m_owner;
            return GH_ObjectResponse.Handled;
        }

        // Check the state of the panel variable.
        // If it is not set, try to get the panel from
        // the first hoopsnake in the owner.HoopSnakes
        // list that has a non-null panel. If this 
        // is not set either, generate a new panel
        // and initialize it.
        private void SetupPanel() {
            if (panel == null)
            {
                foreach (HSComponent h in m_owner.HoopSnakes)
                {
                    if (h.attr.panel != null)
                    {
                        panel = h.attr.panel;
                        return;
                    }
                }
                panel = new HSCPanel(m_owner);
            }
        }
        public HSCPanel Panel
        {
            get
            {
                if (panel == null) SetupPanel();
                return panel;
            }
        }
    }
}

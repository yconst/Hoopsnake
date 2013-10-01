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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using GH_IO;
using GH_IO.Serialization;
using Rhino;

namespace HoopSnake
{
    public class HSComponent : GH_Component 
    {
        // References to components
        internal HSDataParam P;
        internal HSBoolParam B;
        internal HSTriggerParam T;
        internal HSComponent OldComponent = null;

        // Reference to attributes object. An explicit one is required to avoid 
        // casting of m_attributes to HSAttributes everytime.
        internal HSAttributes attr;

        // Static (global) references
        static internal HSComponent ActiveComponent;
        static private bool stop = false;
        static private bool updateView = true;
        static internal int delay = 0;

        private bool autoUpdate = false;

        // 
        private int dataLimit = Int32.MaxValue;
        private bool resetCounterOnLoop = true;

        int cStep = 0, lStep = 0;

        public HSComponent()
            : base("HoopSnake", "HS", "HoopSnake Feedback Component", "Extra", "Volatile Prototypes")
        {}
        public override void CreateAttributes()
        {
            base.m_attributes = attr = new HSAttributes(this);
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.Register_GenericParam("Starting Data", "S", "Starting Data. This will be output just once before any looping is performed.");
            P = new HSDataParam(this, (GH_Param<IGH_Goo>)Params.Input[0]);
            B = new HSBoolParam(this);
            T = new HSTriggerParam(this);
            pManager.RegisterParam(P);
            pManager.RegisterParam(B);
            pManager.RegisterParam(T);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("Feedback", "F", "Feedback Output. Here you get a copy of the data at the \"*D\" input, or the \"*S\" input if the first is empty.");
            pManager.Register_GenericParam("Feedback History", "H", "History Output. Here you get the history of all Hoopsnake iterations.");
            pManager.Register_IntegerParam("Loops Counter", "L", "Loops Counter.");
            pManager.Register_IntegerParam("Iterations Counter", "I", "Iterations Counter.");
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            DA.DisableGapLogic();
            if (DA.Iteration <= 0)
            {
                // Solveinstance is being called as many times as the length
                // of the input data. Why? Dunno. But it messes things up, outputting
                // n*n data. 
                DA.SetDataTree(0, P.VolatileData);
                DA.SetDataTree(1, P.History);
                DA.SetData(2, lStep);
                DA.SetData(3, cStep);
            }
        }

        // Start.
        public void Start()
        {
            // Prepare this component for looping.
            if (resetCounterOnLoop) ResetStepCounter();
            ActiveComponent = this;
            OldComponent = null;
            // Start looping.
            Loop();
        }

        // Auto loop all. 
        public void autoLoop()
        {
            attr.Panel.Message("Checking all Components.");
            ResetAllData();
            // Case 1:  There is only on HS component in the document.
            //          Use that as a starting point.
            if (HoopSnakes.Count == 1)
            {
                ActiveComponent = HoopSnakes[0];
            }
            // Case 2:  There are more components.
            //          Attempt to find a "starting point" for the
            //          loop structure by checking if there is a component
            //          with no inputs in trigger.
            else
            {
                foreach (HSComponent h in HoopSnakes)
                {
                    if (h.Params.Input[3].SourceCount == 0)
                    {
                        h.OldComponent = ActiveComponent;
                        ActiveComponent = h;
                    }
                }
            }
            Loop();
        }

        //
        // Performs a complete loop of ALL the components,
        // in their correct sequence, depending on their
        // connectivity.
        //
        public void Loop()
        {
            attr.Panel.loopMode();
            if (!updateView) Rhino.Display.RhinoView.EnableDrawing = false;
            attr.Panel.Message("Starting Loop.");
            int steps = 0;
            // Reset all broken components
            foreach (HSComponent h in HoopSnakes)
            {
                if (h.Params.Output[0].VolatileData.IsEmpty) h.ResetData();
            }
            while (ActiveComponent != null && !stop)
            {
                // 1.
                // If an active HoopSnake exists:
                //    If it is still within the loop step it.
                //    Otherwise for the active HoopSnake:
                //       Am I done?
                //       If yes:
                //       1. give control back to the old guy (or null).
                //       2. Reset OldComponent to null.
                if (ActiveComponent != null)
                {
                    if (ActiveComponent.ShouldLoopContinue)
                    {
                        ActiveComponent.Step();
                        steps++;
                    }
                    else
                    {
                        ActiveComponent.lStep++;
                        ActiveComponent.ExpireSolution(true);
                        if (ActiveComponent.OldComponent == null)
                        {
                            attr.Panel.Message("dot", ActiveComponent.NickName, "End of Local Loop.");
                            ActiveComponent = null;
                        }
                        else
                        {
                            HSComponent c = ActiveComponent.OldComponent;
                            attr.Panel.Message("up", ActiveComponent.NickName, "End of Local Loop. Switching to \"" + c.NickName + "\"");
                            ActiveComponent.OldComponent = null;
                            ActiveComponent = c;
                            
                        }
                    }
                }
          
                // 2.
                // Foreach HoopSnake do:
                // IF my T* input has changed I am in charge!
                // If somebody else was in charge, store him
                // in OldComponent.
                foreach (HSComponent h in HoopSnakes)
                {
                    if (!h.T.same(true) && ActiveComponent != h)
                    {
                        // Messages
                        if (ActiveComponent != null) attr.Panel.Message("down", ActiveComponent.NickName, "Switching to \"" + h.NickName + "\"");
                        else attr.Panel.Message("forward", "", "Switching to \"" + h.NickName + "\"");

                        h.OldComponent = ActiveComponent;
                        ActiveComponent = h;
                        h.ResetData();
                        if (h.resetCounterOnLoop) h.ResetStepCounter();

                        //Message
                        attr.Panel.Message("dot", ActiveComponent.NickName, "Starting Local Loop.");
                    }
                }
                if (delay > 0)
                {
                    Thread.Sleep(delay);
                }
            }
            attr.Panel.Message("End of Loop.");
            attr.Panel.Message("Total solutions: " + steps.ToString());

            attr.Panel.defaultMode();
            stop = false;
            Rhino.Display.RhinoView.EnableDrawing = true;
            Rhino.RhinoDoc.ActiveDoc.Views.Redraw();
        }
        //
        // Performs a single step of the active component
        // 1. Collect Data
        // 2. Recalculate Solution
        // 3. Update Boolean State
        //
        // After this function finishes, the solution must
        // be in a complete new state.
        //
        public void Step()
        {
            P.collectVolatileData_Silent();
            cStep++;
            ExpireSolution(true);
            B.collectVolatileData_Silent();
            RhinoApp.Wait();
        }
        
        //
        // Sets the Stop flag so that
        // the looping will stop on the
        // next iteration.
        //
        public void Stop()
        {
            stop = true;
        }

        //
        // Clear all data of the D* input.
        // 1. Clear Data.
        // 2. Calculate solution with
        //    blank data.
        // 3. Collect data again (from S
        //    if D* is empty).
        // 4. Recalculate solution.
        // 5. Update boolean state.
        // 
        public void ResetData()
        {
            attr.Panel.Message("reset", NickName, "Resetting.");
            P.clearAllData();
            ResetStepCounter();
            ExpireSolution(true);
            P.collectVolatileData_Silent();
            ExpireSolution(true);
            B.collectVolatileData_Silent();
            T.same(true);
        }

        //
        // Careful! Resetting all components
        // is not the same as resetting each
        // one serially.
        //
        public void ResetAllData()
        {
            foreach (HSComponent h in HoopSnakes)
            {
                h.ResetData();
                h.ResetLoopCounter();
            }
            foreach (HSComponent h in HoopSnakes)
            {
                T.same(true);
            }
        }

        //
        // Resets the Step Counter
        //
        public void ResetStepCounter()
        {
            cStep = 0;
        }

        //
        // Resets the Loop Counter
        //
        public void ResetLoopCounter()
        {
            lStep = 0;
        }

        //
        // Writing and Reading.
        // Any methods to save custom properties apart from those of the original
        // component should be defined here.
        // ______________________________________________

        public override bool Read(GH_IReader reader)
        {
            try
            {
                GH_IReader reader2 = reader.FindChunk("Hoopsnake_Data");
                if (reader2 != null)
                {
                    dataLimit = Int32.MaxValue;// FIX THAT!!! //int.Parse(reader2.GetInt32("dataLimit").ToString());
                    resetCounterOnLoop = reader2.GetBoolean("resetCounter");
                    updateView = reader2.GetBoolean("updateView");
                    autoUpdate = reader2.GetBoolean("autoUpdate");
                    int newDelay = 0;
                    reader2.TryGetInt32("delay", ref newDelay);
                }
            }
            catch {}
            // In the end, call the reader of the base component to load inherited fields.
            return base.Read(reader);
        }
        public override bool Write(GH_IWriter writer)
        {
            try
            {
                GH_IWriter writer2 = writer.CreateChunk("Hoopsnake_Data");
                writer2.SetInt32("dataLimit", dataLimit);
                writer2.SetBoolean("resetCounter", resetCounterOnLoop);
                writer2.SetBoolean("updateView", updateView);
                writer2.SetBoolean("autoUpdate", autoUpdate);
                writer2.SetInt32("delay", delay);
            }
            catch {}
            return base.Write(writer);
        }

        //
        // Extra Context Menu Items and corresponding
        // event handlers.
        // ______________________________________________

        public override bool AppendMenuItems(ToolStripDropDown iMenu)
        {
            bool r = base.AppendMenuItems(iMenu);
            GH_DocumentObject.Menu_AppendSeparator(iMenu);
            GH_DocumentObject.Menu_AppendGenericMenuItem(iMenu, "Step", new EventHandler(this.menu_StepButton), HoopSnake.Properties.Resources.PLAY, null, true);
            GH_DocumentObject.Menu_AppendGenericMenuItem(iMenu, "Loop", new EventHandler(this.menu_LoopButton), null, null, true);
            GH_DocumentObject.Menu_AppendGenericMenuItem(iMenu, "Auto Loop All", new EventHandler(this.menu_AutoLoopButton), HoopSnake.Properties.Resources.LOOP, null, true);
            GH_DocumentObject.Menu_AppendSeparator(iMenu);
            GH_DocumentObject.Menu_AppendGenericMenuItem(iMenu, "Stop", new EventHandler(this.menu_StopButton), HoopSnake.Properties.Resources.STOP, null, true);
            GH_DocumentObject.Menu_AppendSeparator(iMenu);
            GH_DocumentObject.Menu_AppendGenericMenuItem(iMenu, "Reset", new EventHandler(this.menu_ResetButton), HoopSnake.Properties.Resources.RESET, null, true);
            GH_DocumentObject.Menu_AppendGenericMenuItem(iMenu, "Reset All", new EventHandler(this.menu_ResetAllButton), HoopSnake.Properties.Resources.RESET, null, true);
            GH_DocumentObject.Menu_AppendSeparator(iMenu);
            GH_DocumentObject.Menu_AppendGenericMenuItem(iMenu, "Monitor T* Input", new EventHandler(this.menu_TimerButton), null, null, true, autoUpdate);
            GH_DocumentObject.Menu_AppendSeparator(iMenu);
            GH_DocumentObject.Menu_AppendGenericMenuItem(iMenu, "Reset Counter", new EventHandler(this.menu_ResetCounterButton), null, null, true, resetCounterOnLoop);
            return r;
        }
        private void menu_StepButton(object sender, EventArgs e) 
        {
            Step();
        }
        private void menu_LoopButton(object sender, EventArgs e)
        {
            Start();
        }
        private void menu_AutoLoopButton(object sender, EventArgs e)
        {
            attr.Panel.ClearConsole();
            autoLoop();
        }
        private void menu_ResetButton(object sender, EventArgs e)
        {
            ResetData();
        }
        private void menu_ResetAllButton(object sender, EventArgs e)
        {
            ResetAllData();
            attr.Panel.ClearConsole();
        }
        private void menu_StopButton(object sender, EventArgs e)
        {
            Stop();
        }
        private void menu_ResetCounterButton(object sender, EventArgs e)
        {
            ToolStripMenuItem iItem = (ToolStripMenuItem)sender;
            iItem.Checked = !iItem.Checked;
            resetCounterOnLoop = iItem.Checked;
        }
        private void menu_TimerButton(object sender, EventArgs e)
        {
            ToolStripMenuItem iItem = (ToolStripMenuItem)sender;
            iItem.Checked = !iItem.Checked;
            autoUpdate = iItem.Checked;
        }

        //
        // Properties
        // ______________________________________________

        //
        // Check whether looping is still valid for
        // this particular component. Checks:
        // 1. Whether the component's condition is true.
        // 2. Whether the datalimit has been reached.
        //
        // Note: User interruption ("stop" variable)
        // is handled in the "while" evaluation within 
        // the loop.
        //
        public bool ShouldLoopContinue
        {
            get
            {
                return B.Cond && (dataLimit < 1 || P.VolatileData.DataCount < dataLimit);
            }
        }
        public bool InLoop
        {
            get
            {
                return ActiveComponent != null;
            }
        }
        public int CStep
        {
            get
            {
                return cStep;
            }
        }
        public bool AutoUpdate
        {
            get
            {
                return autoUpdate;
            }
        }
        static public bool UpdateView
        {
            get
            {
                return updateView;
            }
            set
            {
                updateView = value;
            }
        }
        public List<HSComponent> HoopSnakes
        {
            get
            {
                List<HSComponent> r = new List<HSComponent>();
                if (Document != null)
                {
                    foreach (GH_DocumentObject o in Document.Objects)
                    {
                        if (o.ComponentGuid == this.ComponentGuid)
                        {
                            r.Add((HSComponent)o);
                        }
                    }
                }
                return r;
            }
        }
        public bool ResetCounterOnLoop
        {
            get
            {
                return resetCounterOnLoop;
            }
            set
            {
                resetCounterOnLoop = value;
            }
        }
        public void StartSolutionHandler(object o, GH_SolutionEventArgs e)
        {
            if (!InLoop)
            {
                Document.SolutionEnd -= StartSolutionHandler;
                Start();
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("0f243fa0-62a3-11e0-ae3e-0800200c9a66"); }
        }
        public GH_Document Document
        {
            get { return this.OnPingDocument(); }
        }
        protected override System.Drawing.Bitmap Internal_Icon_24x24
        {
            get
            {
                return HoopSnake.Properties.Resources.ICON;
            }
        }
    }
}

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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HoopSnake
{
    public partial class HSCPanel : Form
    {
        private HSComponent owner;
        public HSCPanel(HSComponent nOwner)
        {
            owner = nOwner;
            InitializeComponent();
            UpdateViewButton.Checked = HSComponent.UpdateView;
            delayBox.Text = HSComponent.delay.ToString();
        }
        internal void loopMode()
        {
            Looper.Visible = true;
            statusLabel.Text = "Looping.";
            ResetAllButton.Enabled = false;
            LoopAllButton.Enabled = false;
            StepButton.Enabled = false;
            UpdateViewButton.Enabled = false;
            HSCombo.Enabled = false;
        }
        internal void defaultMode()
        {
            Looper.Visible = false;
            statusLabel.Text = "Ready.";
            ResetAllButton.Enabled = true;
            LoopAllButton.Enabled = true;
            StepButton.Enabled = true;
            UpdateViewButton.Enabled = true;
            HSCombo.Enabled = true;
        }
        private void UpdateComboBox(object sender, EventArgs e)
        {
            UpdateComboBox();
        }
        internal void UpdateComboBox()
        {
            HSCombo.ComboBox.DataSource = owner.HoopSnakes;
            HSCombo.ComboBox.DisplayMember = "NickName";
            HSCombo.ComboBox.ValueMember = "NickName";
        }
        private void LoopAllButton_Click(object sender, EventArgs e)
        {
            owner.autoLoop();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            owner.Stop();
        }

        public void Message(String s)
        {
            console.Rows.Insert(0, 1);
            console.Rows[0].HeaderCell.Value = console.Rows.Count;
            console.Rows[0].Cells[2].Value = s;
            console.Rows[0].Cells[0].ValueType = typeof(System.Drawing.Bitmap);
            console.Rows[0].Cells[0].Value = HoopSnake.Properties.Resources.DOT;
        }

        public void Message(String t, String f, String s)
        {
            console.Rows.Insert(0, 1);
            console.Rows[0].HeaderCell.Value = console.Rows.Count;
            console.Rows[0].Cells[1].Value = f;
            console.Rows[0].Cells[2].Value = s;
            console.Rows[0].Cells[0].ValueType = typeof(System.Drawing.Bitmap);
            if (t.Equals("reset"))
            {
                console.Rows[0].Cells[0].Value = HoopSnake.Properties.Resources.ARESET;
            }
            else if (t.Equals("up"))
            {
                console.Rows[0].Cells[0].Value = HoopSnake.Properties.Resources.AUP;
            }
            else if (t.Equals("down"))
            {
                console.Rows[0].Cells[0].Value = HoopSnake.Properties.Resources.ADOWN;
            }
            else if (t.Equals("forward"))
            {
                console.Rows[0].Cells[0].Value = HoopSnake.Properties.Resources.ARIGHT;
            }
            else
            {
                console.Rows[0].Cells[0].Value = HoopSnake.Properties.Resources.DOT;
            }
        }


        public void ClearConsole()
        {
            console.Rows.Clear();
        }

        private void Panel_Closing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void resetCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            owner.ResetStepCounter();
            owner.ExpireSolution(true);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            owner.ResetAllData();
            ClearConsole();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripEx1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void UpdateViewButton_Click(object sender, EventArgs e)
        {
            HSComponent.UpdateView = !HSComponent.UpdateView;
            UpdateViewButton.Checked = HSComponent.UpdateView;
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            try
            {
                HSComponent c = (HSComponent)HSCombo.SelectedItem;
                c.Step();
            }
            catch
            {
                Message("Error while stepping.");
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            int val = 0;
            bool success = Int32.TryParse(delayBox.Text, out val);
            if (success)
            {
                HSComponent.delay = val;
            }
            else
            {
                delayBox.Text = HSComponent.delay.ToString();
            }
        }
    }
}

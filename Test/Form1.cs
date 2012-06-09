using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            filterTreeView1.BeginUpdate();
            filterTreeView1.Nodes.Add("Parent");
            filterTreeView1.Nodes[0].Nodes.Add("Child 1");
            filterTreeView1.Nodes[0].Nodes.Add("Child 2");
            filterTreeView1.Nodes[0].Nodes[1].Nodes.Add("Grandchild");
            filterTreeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Great Grandchild");
            filterTreeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Great");
            filterTreeView1.Nodes[0].Nodes.Add("Child X");
            filterTreeView1.Nodes[0].Nodes[1].Nodes.Add("XXX");
            filterTreeView1.EndUpdate();
            filterTreeView1.ExpandAll();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            filterTreeView1.FilterString = textBox1.Text;
            filterTreeView1.Filter();
            filterTreeView1.ExpandAll();
        }
    }
}

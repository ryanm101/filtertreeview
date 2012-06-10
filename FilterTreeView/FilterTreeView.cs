using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

internal class resfinder {}

namespace FilterTreeView
{

    [ToolboxBitmap(typeof(resfinder), "FilterTreeView.Resources.FilterTreeView.bmp")]
    public partial class FilterTreeView : TreeView
    {
        private TreeNode[] NodesBackup;
        private String _FilterString;

        public FilterTreeView()
        {
            InitializeComponent();
            NodesBackup = null;
        }

        public String FilterString {
            get { return _FilterString; }
            set { _FilterString = value; }
        }
        
        public void Filter() {
            this.BeginUpdate();
            if (NodesBackup == null) {
                NodesBackup = new TreeNode[this.Nodes.Count];
                int i = 0;
                foreach (TreeNode nb in this.Nodes) {
                    NodesBackup[i] = (TreeNode)nb.Clone();
                    i++;
                }
            }
            if ((_FilterString == String.Empty) || (_FilterString == "")) {
                this.Nodes.Clear();
                foreach (TreeNode tn in NodesBackup)
                    this.Nodes.Add((TreeNode)tn.Clone());
                NodesBackup = null;
            } else {
                List<int> lstIndexes = new List<int>();
                this.Nodes.Clear();
                foreach (TreeNode tn in NodesBackup)
                    this.Nodes.Add((TreeNode)tn.Clone());
                foreach (TreeNode _node in this.Nodes) {
                    RecurseSearch(_node);
                    if (_node.GetNodeCount(false) == 0)
                        if (_node.Text.IndexOf(_FilterString,StringComparison.CurrentCultureIgnoreCase) == -1)
                            lstIndexes.Add(_node.Index);
                }
                for (int i = lstIndexes.Count -1; i >= 0; i--)
                    this.Nodes.RemoveAt(lstIndexes[i]);
            }
            this.EndUpdate();
            this.Refresh();
        }

        private void RecurseSearch(TreeNode tn) {
            List<int> lstIndexes = new List<int>();
            foreach (TreeNode _node in tn.Nodes) {
                if (_node.GetNodeCount(false) == 0) {
                    if (_node.Text.IndexOf(_FilterString, StringComparison.CurrentCultureIgnoreCase) == -1)
                        lstIndexes.Add(_node.Index);
                } else {
                    RecurseSearch(_node);
                    if (_node.GetNodeCount(false) == 0) {
                        if (_node.Text.IndexOf(_FilterString, StringComparison.CurrentCultureIgnoreCase) == -1)
                            lstIndexes.Add(_node.Index);
                    }
                }
            }
            for (int i = lstIndexes.Count -1; i >=0; i--)
                tn.Nodes.RemoveAt(lstIndexes[i]);
        }
    }
}

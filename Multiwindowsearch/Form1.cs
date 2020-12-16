using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiwindowsearch
{
    public partial class Form1 : Form
    {
        public object TreeViewItem { get; private set; }

        public Form1()
        {
            InitializeComponent();
            showTreeView(0);
            showTreeView(1);

        }

        public void LoadTreeControl()
        {
                        

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nSelectNo = tabControl1.SelectedIndex;
        }


        public void showTreeView(int nChannel)
        {
            TreeNode root;
            if (nChannel == 0)
            {
                treeView1.Nodes.Clear();
                root = treeView1.Nodes.Add("Root");
            }
            else
            {
                treeView2.Nodes.Clear();
                root = treeView2.Nodes.Add("Root");
            }
            foreach (string drive in Directory.GetLogicalDrives())
            {
                DriveInfo di = new DriveInfo(drive);

                if (di.IsReady)
                {
                    TreeNode node = root.Nodes.Add(drive);
                    node.Nodes.Add("\\");
                }

            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text.Equals("\\"))
                {
                    e.Node.Nodes.Clear();
                    string path = e.Node.FullPath.Substring(e.Node.FullPath.IndexOf("\\") + 1);
                    string[] directories = Directory.GetDirectories(path);

                    foreach (string str in directories)
                    {
                        TreeNode newNode = e.Node.Nodes.Add(str.Substring(str.LastIndexOf("\\") + 1));
                        newNode.Nodes.Add("\\");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Treeview1" + ex.Message);
            }
        }

        private void treeView2_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text.Equals("\\"))
                {
                    e.Node.Nodes.Clear();
                    string path = e.Node.FullPath.Substring(e.Node.FullPath.IndexOf("\\") + 1);
                    string[] directories = Directory.GetDirectories(path);


                    foreach (string str in directories)
                    {
                        TreeNode  newNode = e.Node.Nodes.Add(str.Substring(str.LastIndexOf("\\") + 1));
                        newNode.Nodes.Add("\\");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Treeview2" + ex.Message);
            }
        }
    }


}

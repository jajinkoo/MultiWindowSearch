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

        // 트리뷰 +버튼 누른 후 동작 
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectTreeView(e.Node);
        }

        void SelectTreeView(TreeNode node)
        {
            if (node.FullPath == null)
            {
                return;
            }
            string path = node.FullPath;
            ViewDirectory(path);
        }

        void ViewDirectory(string path)
        {
            string curPath = path;

            if (path.IndexOf("Root\\") == 0)
            {
                curPath = path.Substring(path.IndexOf("\\") + 1);

                string strTemp = (curPath.Length > 4) ? curPath.Remove(curPath.IndexOf("\\") + 1, 1) : curPath;
                curPath = strTemp;
            }

            try
            {
                listView1.Items.Clear();

                string[] directories = Directory.GetDirectories(curPath);

                foreach (string directory in directories)
                {
                    DirectoryInfo info = new DirectoryInfo(directory);
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        info.Name,info.LastWriteTime.ToString(), "파일폴더", ""
                    });
                    listView1.Items.Add(item);
                }
                
                string[] files = Directory.GetFiles(curPath);

                foreach (string file in files)
                {
                    FileInfo info = new FileInfo(file);
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        info.Name, info.LastWriteTime.ToString(), info.Extension, ((info.Length/1000)+1).ToString() +"KB"
                    });
                    listView1.Items.Add(item);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("ViewDirectoryList" + ex.Message);
            }

        }
    }


}

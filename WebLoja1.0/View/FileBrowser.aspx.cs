
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.SessionState;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.IO;

    public class FileBrowser : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.ListBox lstDirs;
        protected System.Web.UI.WebControls.Button cmdParent;
        protected System.Web.UI.WebControls.Button cmdShowInfo;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.ListBox lstFiles;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.Label Label3;
        protected System.Web.UI.WebControls.Button cmdBrowse;
        protected System.Web.UI.WebControls.Label lblFileInfo;
        protected System.Web.UI.WebControls.Label lblCurrentDir;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string startingDir = System.Web.HttpContext.Current.Server.MapPath("yourfolderHere");//@"c:\Temp";
                lblCurrentDir.Text = startingDir;
                ShowFilesIn(startingDir);
                ShowDirectoriesIn(startingDir);
            }

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdParent.Click += new System.EventHandler(this.cmdParent_Click);
            this.cmdShowInfo.Click += new System.EventHandler(this.cmdShowInfo_Click);
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void ShowFilesIn(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);

            lstFiles.Items.Clear();
            foreach (FileInfo fileItem in dirInfo.GetFiles())
            {
                lstFiles.Items.Add(fileItem.Name);
            }
        }

        private void ShowDirectoriesIn(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);

            lstDirs.Items.Clear();
            foreach (DirectoryInfo dirItem in dirInfo.GetDirectories())
            {
                lstDirs.Items.Add(dirItem.Name);
            }
        }

        private void cmdShowInfo_Click(object sender, System.EventArgs e)
        {
            if (lstFiles.SelectedIndex != -1)
            {
                string fileName = Path.Combine(lblCurrentDir.Text, lstFiles.SelectedItem.Text);
                FileInfo selFile = new FileInfo(fileName);
                lblFileInfo.Text = "<b>" + selFile.Name + "</b><br>";
                lblFileInfo.Text += "Size: " + selFile.Length + "<br>";
                lblFileInfo.Text += "Created: ";
                lblFileInfo.Text += selFile.CreationTime.ToString();
                lblFileInfo.Text += "<br>Last Accessed: ";
                lblFileInfo.Text += selFile.LastAccessTime.ToString();
            }

        }

        private void cmdParent_Click(object sender, System.EventArgs e)
        {
            if (Directory.GetParent(lblCurrentDir.Text) != null)
            {
                string newDir = Directory.GetParent(lblCurrentDir.Text).FullName;
                lblCurrentDir.Text = newDir;
                ShowFilesIn(newDir);
                ShowDirectoriesIn(newDir);
            }

        }

        private void cmdBrowse_Click(object sender, System.EventArgs e)
        {
            if (lstDirs.SelectedIndex != -1)
            {
                string newDir = Path.Combine(lblCurrentDir.Text, lstDirs.SelectedItem.Text);
                lblCurrentDir.Text = newDir;
                ShowFilesIn(newDir);
                ShowDirectoriesIn(newDir);
            }

        }

    }

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace ExpressPOS
{
    public partial class frmReportView : Form
    {
        clsConnectionNode clsCN = new clsConnectionNode();
        public frmReportView()
        {
            InitializeComponent();
            clsCN.DBConnectionInitializing();
        }

        private void frmReportView_Load(object sender, EventArgs e)
        {

        }



    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkerPositions
{
    public partial class Form1 : Form
    {
        private MarkerMediator mediator = new MarkerMediator();
        private Button addButton;

        public Form1()
        {
            InitializeComponent();
            this.addButton = new Button();
            this.addButton.Click += OnAddClick;
            this.addButton.Text = "Add Marker";
            this.addButton.Dock = DockStyle.Bottom;
            this.Controls.Add(this.addButton);
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            var m = this.mediator.CreateMarker();
            this.Controls.Add(m);
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void integrantesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modificarArtículoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void añadirCategoríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AñadirCategoria ventanaAñadirCategoria = new AñadirCategoria();
            ventanaAñadirCategoria.Show();
        }
    }
}

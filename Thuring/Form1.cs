using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thuring
{
    public partial class Form1 : Form
    {
        const string pattern = @"(?<symbol>[A-Z0-1])(?<direction>L|R|S)q(?<stateNumber>\d+)";
        public Form1()
        {
            InitializeComponent();

            grid.RowCount = 5;
            SetTitles();
        }

        private void SetTitles()
        {
            for (int i = 1; i <= grid.Columns.Count; i++)
            {
                grid.Columns[i - 1].HeaderText = $"q{i}";
            }

            char[] letters = (txtАлфавит.Text + "e").ToCharArray();

            grid.RowCount = letters.Length;

            for (int i = 0; i < grid.RowCount; i++)
            {
                grid.Rows[i].HeaderCell.Value = letters[i].ToString();
            }


        }
        private void выполнитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int state = 1;
            char[] letters = (txtАлфавит.Text + "e").ToCharArray();

            char symbol = GetSymbol()[0];

            int rowIndex = Array.IndexOf(letters, symbol);

            string command = grid[state - 1, rowIndex].Value.ToString();

            Match m = Regex.Match(command, pattern);

            var nextSymbol = m.Groups["symbol"].Value;
            var direction = m.Groups["direction"].Value;
            var nextState = m.Groups["stateNumber"].Value;


            if (txtTape.SelectedText != ".")
            {
                int selectIndex = txtTape.SelectionStart;

                switch (direction)
                {
                    case "L":
                        txtTape.SelectedText = nextSymbol;
                        txtTape.Select(selectIndex - 1, 1);
                        break;
                    case "R":
                        txtTape.SelectedText = nextSymbol;
                        txtTape.Select(selectIndex + 1, 1);
                        break;
                }


            }

        }
        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private string GetSymbol()
        {
            return txtTape.SelectedText;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CoinCompare
{
    public partial class Form1 : Form
    {
        DataTable CoinsDatabase = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void button_openCsv_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Open coins .CSV database";
            openFileDialog1.DefaultExt = "csv";
            openFileDialog1.Filter = "CSV files|*.csv|All files|*.*";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            CoinsDatabase = new DataTable();
            ReadCsv(openFileDialog1.FileName, CoinsDatabase);
            CoinsDatabase.Columns.Add("Collisions");
            dataGridView1.DataSource = CoinsDatabase;

        }

        public void ReadCsv(string fileName, DataTable table)
        {
            table.Clear();
            table.Columns.Clear();
            FileStream inputFile;
            try
            {
                inputFile = File.OpenRead(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening file:" + fileName + " : " + ex.Message);
                return;
            }

            //read headers
            StringBuilder inputStr = new StringBuilder();
            int c = inputFile.ReadByte();
            while (c != '\r' && c != '\n' && c != -1)
            {
                byte[] b = new byte[1];
                b[0] = (byte)c;
                inputStr.Append(Encoding.GetEncoding(Properties.Settings.Default.CodePage).GetString(b));
                c = inputFile.ReadByte();
            }

            //create and count columns and read headers
            int colNum = 0;
            if (inputStr.Length != 0)
            {
                string[] cells = inputStr.ToString().Split(Properties.Settings.Default.CSVdelimiter);
                colNum = cells.Length - 1;
                for (int i = 0; i < colNum; i++)
                {
                    table.Columns.Add(cells[i]);
                }
            }

            //read CSV content string by string
            while (c != -1)
            {
                int i = 0;
                c = 0;
                inputStr.Length = 0;
                while (i < colNum && c != -1 /*&& c != '\r' && c != '\n'*/)
                {
                    c = inputFile.ReadByte();
                    byte[] b = new byte[1];
                    b[0] = (byte)c;
                    if (c == Properties.Settings.Default.CSVdelimiter) i++;
                    if (c != -1) inputStr.Append(Encoding.GetEncoding(Properties.Settings.Default.CodePage).GetString(b));
                }
                while (c != '\r' && c != '\n' && c != -1) c = inputFile.ReadByte();
                if (inputStr.ToString().Replace(Properties.Settings.Default.CSVdelimiter, ' ').Trim().TrimStart('\r').TrimStart('\n').TrimStart('\r').TrimStart('\n').TrimEnd('\n').TrimEnd('\r').TrimEnd('\r').TrimEnd('\n') != "")
                {
                    string[] cells = inputStr.ToString().Split(Properties.Settings.Default.CSVdelimiter);

                    DataRow row = table.NewRow();
                    for (i = 0; i < cells.Length - 1; i++)
                    {
                        row[i] = cells[i].TrimStart('\r').TrimStart('\n').TrimEnd('\n').TrimEnd('\r').Trim();
                    }
                    table.Rows.Add(row);
                }
            }
            inputFile.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Collisions"].Value = "";
            List<int> result = compareCoin(dataGridView1.CurrentCell.RowIndex);
            for (int j = 0; j < result.Count; j++)
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Collisions"].Value += dataGridView1.Rows[result[j]].Cells[0].Value.ToString() + ", ";
        }

        private void button_compare_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                List<int> result = compareCoin(i);
                for (int j = 0; j < result.Count; j++)
                    dataGridView1.Rows[i].Cells["Collisions"].Value += dataGridView1.Rows[result[j]].Cells[0].Value.ToString() + ", ";
            }
        }

        List<int> compareCoin(int strNum)
        {
            List<int> result = new List<int>();
            for (int n = 0; n < dataGridView1.RowCount; n++) result.Add(n);

            for (int i = 1; i < dataGridView1.ColumnCount - 1; i++)
            {
                int minVal1 = 0;
                int maxVal1 = 0;
                string str1 = "";
                if (dataGridView1.Rows[strNum].Cells[i].Value != null)
                {
                    str1 = dataGridView1.Rows[strNum].Cells[i].Value.ToString();
                    int.TryParse(str1.Substring(0, str1.IndexOf('-')).Trim(), out minVal1);
                    int.TryParse(str1.Substring(str1.IndexOf('-') + 1).Trim(), out maxVal1);
                    for (int j = 0; j < result.Count; j++)
                    {
                        if (result[j] != strNum)
                        {
                            int minVal2 = 0;
                            int maxVal2 = 0;
                            string str2 = "";
                            if (dataGridView1.Rows[result[j]].Cells[i].Value != null)
                            {
                                str2 = dataGridView1.Rows[result[j]].Cells[i].Value.ToString();
                                int.TryParse(str2.Substring(0, str2.IndexOf('-')).Trim(), out minVal2);
                                int.TryParse(str2.Substring(str2.IndexOf('-') + 1).Trim(), out maxVal2);
                                if (!(
                                    (minVal1 >= minVal2 && minVal1 <= maxVal2) || (maxVal1 >= minVal2 && maxVal1 <= maxVal2) ||
                                    (minVal2 >= minVal1 && minVal2 <= maxVal1) || (maxVal2 >= minVal1 && maxVal2 <= maxVal1)
                                    ))
                                {
                                    result.RemoveAt(j);
                                    j--;
                                }
                            }
                            else
                            {
                                result.RemoveAt(j);
                                j--;
                            }
                        }
                        else
                        {
                            result.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++) dataGridView1.Rows[i].Cells["Collisions"].Value = "";
        }
    }
}

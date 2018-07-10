using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CoinCompare
{
    public partial class Form1 : Form
    {
        DataTable CoinsDatabase = new DataTable();
        const int resultColumn = 1;
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
            CoinsDatabase.Columns.Add("Collisions").SetOrdinal(resultColumn);
            refreshGrids();
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

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*dataGridView_table.Rows[dataGridView_table.CurrentCell.RowIndex].Cells[resultColumn].Value = "";
            dataGridView_graph.Rows[dataGridView_table.CurrentCell.RowIndex].Cells[resultColumn].Value = "";
            List<int> result = compareCoin(dataGridView_table.CurrentCell.RowIndex);
            for (int j = 0; j < result.Count; j++)
            {
                dataGridView_table.Rows[dataGridView_table.CurrentCell.RowIndex].Cells[resultColumn].Value += dataGridView_table.Rows[result[j]].Cells[0].Value.ToString() + ", ";
                dataGridView_graph.Rows[dataGridView_table.CurrentCell.RowIndex].Cells[resultColumn].Value += dataGridView_table.Rows[result[j]].Cells[0].Value.ToString() + ", ";
            }*/
        }

        private void button_compare_Click(object sender, EventArgs e)
        {
            button_clear_Click(this, EventArgs.Empty);
            for (int i = 0; i < dataGridView_table.RowCount; i++)
            {
                List<int> result = compareCoin(i);
                for (int j = 0; j < result.Count; j++)
                {
                    dataGridView_table.Rows[i].Cells[resultColumn].Value += dataGridView_table.Rows[result[j]].Cells[0].Value.ToString() + ", ";
                    dataGridView_graph.Rows[i].Cells[resultColumn].Value += dataGridView_table.Rows[result[j]].Cells[0].Value.ToString() + ", ";
                }
            }
        }

        List<int> compareCoin(int strNum)
        {
            List<int> result = new List<int>();
            for (int n = 0; n < dataGridView_table.RowCount; n++) result.Add(n);

            for (int i = 1; i < dataGridView_table.ColumnCount; i++)
            {
                if (i != resultColumn)
                {
                    int minVal1 = 0;
                    int maxVal1 = 0;
                    string str1 = "";
                    if (dataGridView_table.Rows[strNum].Cells[i].Value.ToString().Contains("-"))
                    {
                        str1 = dataGridView_table.Rows[strNum].Cells[i].Value.ToString();
                        int.TryParse(str1.Substring(0, str1.IndexOf('-')).Trim(), out minVal1);
                        int.TryParse(str1.Substring(str1.IndexOf('-') + 1).Trim(), out maxVal1);
                        for (int j = 0; j < result.Count; j++)
                        {
                            if (result[j] != strNum)
                            {
                                int minVal2 = 0;
                                int maxVal2 = 0;
                                string str2 = "";
                                if (dataGridView_table.Rows[result[j]].Cells[i].Value.ToString().Contains("-"))
                                {
                                    str2 = dataGridView_table.Rows[result[j]].Cells[i].Value.ToString();
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
            }
            return result;
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView_table.RowCount; i++)
            {
                dataGridView_table.Rows[i].Cells[resultColumn].Value = "";
                dataGridView_graph.Rows[i].Cells[resultColumn].Value = "";
            }
        }

        void drawGraph(int column, int row)
        {
            int maxLength = 256;
            int minVal1 = 0;
            int maxVal1 = 0;
            string str1 = "";
            if (dataGridView_table.Rows[row].Cells[column].Value.ToString() != "")
            {
                str1 = dataGridView_table.Rows[row].Cells[column].Value.ToString();
                int.TryParse(str1.Substring(0, str1.IndexOf('-')).Trim(), out minVal1);
                int.TryParse(str1.Substring(str1.IndexOf('-') + 1).Trim(), out maxVal1);
            }

            Image img;
            int fontSize = 8;
            img = new Bitmap(maxLength, dataGridView_graph.Rows[row].Height, PixelFormat.Format24bppRgb);
            var graphics = Graphics.FromImage(img);
            graphics.FillRectangle(Brushes.LightGray, 0, 0, maxLength, dataGridView_graph.Rows[row].Height);
            graphics.FillRectangle(Brushes.Green, minVal1, 0, maxVal1 - minVal1, dataGridView_graph.Rows[row].Height);
            int start_pos = minVal1 - fontSize * 3;
            if (start_pos < 0) start_pos = 0;
            int end_pos = maxVal1;
            if (end_pos + fontSize * 3 > maxLength) end_pos = maxLength - fontSize * 3;
            graphics.DrawString(minVal1.ToString(), new Font("Tahoma", fontSize), Brushes.Black, start_pos, 0);
            graphics.DrawString(maxVal1.ToString(), new Font("Tahoma", fontSize), Brushes.Black, end_pos, 0);
            if (dataGridView_graph.Columns[column] is DataGridViewImageColumn)
            {
                dataGridView_graph.Rows[row].Cells[column].Value = img;
            }
        }

        void refreshGrids()
        {
            dataGridView_table.Rows.Clear();
            dataGridView_table.Columns.Clear();
            for (int i = 0; i < CoinsDatabase.Columns.Count; i++)
            {
                dataGridView_table.Columns.Add(CoinsDatabase.Columns[i].ToString(), CoinsDatabase.Columns[i].ToString());
                dataGridView_table.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < CoinsDatabase.Rows.Count; i++)
            {
                dataGridView_table.Rows.Add(CoinsDatabase.NewRow());
                for (int j = 0; j < CoinsDatabase.Columns.Count; j++) dataGridView_table.Rows[i].Cells[j].Value = CoinsDatabase.Rows[i].ItemArray[j];
            }

            //copy table to graph
            dataGridView_graph.Rows.Clear();
            dataGridView_graph.Columns.Clear();
            dataGridView_graph.Columns.Add(dataGridView_table.Columns[0].Name, dataGridView_table.Columns[0].Name);
            for (int i = 1; i < dataGridView_table.ColumnCount; i++)
            {
                if (i != resultColumn)
                {
                    DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                    imageCol.Name = dataGridView_table.Columns[i].Name;
                    dataGridView_graph.Columns.Add(imageCol);
                }
                else dataGridView_graph.Columns.Add(dataGridView_table.Columns[i].Name.ToString(), dataGridView_table.Columns[i].Name.ToString());
            }
            for (int i = 0; i < dataGridView_table.ColumnCount; i++)
            {
                dataGridView_graph.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int i = 0; i < dataGridView_table.RowCount; i++)
            {
                dataGridView_graph.Rows.Add();
                dataGridView_graph.Rows[i].Cells[0].Value = dataGridView_table.Rows[i].Cells[0].Value;
                dataGridView_graph.Rows[i].Cells[resultColumn].Value = dataGridView_table.Rows[i].Cells[resultColumn].Value;
                for (int j = 1; j < dataGridView_graph.ColumnCount; j++)
                    if (dataGridView_graph.Columns[j] is DataGridViewImageColumn) drawGraph(j, i);
            }
            button_compare_Click(this, EventArgs.Empty);
        }

        private Rectangle dragBoxFromMouseDown_graph;
        private int rowIndexFromMouseDown_graph;
        private int rowIndexOfItemUnderMouseToDrop_graph;

        private void dataGridView_graph_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown_graph != Rectangle.Empty &&
                !dragBoxFromMouseDown_graph.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = dataGridView_graph.DoDragDrop(
                          dataGridView_graph.Rows[rowIndexFromMouseDown_graph],
                          DragDropEffects.Move);
                }
            }
        }

        private void dataGridView_graph_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown_graph = dataGridView_graph.HitTest(e.X, e.Y).RowIndex;

            if (rowIndexFromMouseDown_graph != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown_graph = new Rectangle(
                          new Point(
                            e.X - (dragSize.Width / 2),
                            e.Y - (dragSize.Height / 2)),
                      dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown_graph = Rectangle.Empty;
        }

        private void dataGridView_graph_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView_graph_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dataGridView_graph.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop_graph = dataGridView_graph.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataRow row = CoinsDatabase.NewRow();
                for (int i = 0; i < CoinsDatabase.Columns.Count; i++) row[i] = CoinsDatabase.Rows[rowIndexFromMouseDown_graph].ItemArray[i];
                CoinsDatabase.Rows[rowIndexFromMouseDown_graph].Delete();
                CoinsDatabase.Rows.InsertAt(row, rowIndexOfItemUnderMouseToDrop_graph);
                refreshGrids();
            }
        }

        private Rectangle dragBoxFromMouseDown_table;
        private int rowIndexFromMouseDown_table;
        private int rowIndexOfItemUnderMouseToDrop_table;

        private void dataGridView_table_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown_table != Rectangle.Empty &&
                !dragBoxFromMouseDown_table.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = dataGridView_table.DoDragDrop(
                          dataGridView_table.Rows[rowIndexFromMouseDown_table],
                          DragDropEffects.Move);
                }
            }
        }

        private void dataGridView_table_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown_table = dataGridView_table.HitTest(e.X, e.Y).RowIndex;

            if (rowIndexFromMouseDown_table != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown_table = new Rectangle(
                          new Point(
                            e.X - (dragSize.Width / 2),
                            e.Y - (dragSize.Height / 2)),
                      dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown_table = Rectangle.Empty;
        }

        private void dataGridView_table_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView_table_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dataGridView_table.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop_table = dataGridView_table.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataRow row = CoinsDatabase.NewRow();
                for (int i = 0; i < CoinsDatabase.Columns.Count; i++) row[i] = CoinsDatabase.Rows[rowIndexFromMouseDown_table].ItemArray[i];
                CoinsDatabase.Rows[rowIndexFromMouseDown_table].Delete();
                CoinsDatabase.Rows.InsertAt(row, rowIndexOfItemUnderMouseToDrop_table);
                refreshGrids();
            }
        }

    }
}

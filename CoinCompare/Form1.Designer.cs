namespace CoinCompare
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_openCsv = new System.Windows.Forms.Button();
            this.dataGridView_table = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_compare = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView_graph = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_table)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_graph)).BeginInit();
            this.SuspendLayout();
            // 
            // button_openCsv
            // 
            this.button_openCsv.Location = new System.Drawing.Point(12, 12);
            this.button_openCsv.Name = "button_openCsv";
            this.button_openCsv.Size = new System.Drawing.Size(75, 23);
            this.button_openCsv.TabIndex = 0;
            this.button_openCsv.Text = "Open CSV";
            this.button_openCsv.UseVisualStyleBackColor = true;
            this.button_openCsv.Click += new System.EventHandler(this.button_openCsv_Click);
            // 
            // dataGridView_table
            // 
            this.dataGridView_table.AllowDrop = true;
            this.dataGridView_table.AllowUserToAddRows = false;
            this.dataGridView_table.AllowUserToDeleteRows = false;
            this.dataGridView_table.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_table.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_table.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_table.Name = "dataGridView_table";
            this.dataGridView_table.RowHeadersVisible = false;
            this.dataGridView_table.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_table.Size = new System.Drawing.Size(746, 377);
            this.dataGridView_table.TabIndex = 1;
            this.dataGridView_table.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button_compare
            // 
            this.button_compare.Enabled = false;
            this.button_compare.Location = new System.Drawing.Point(93, 12);
            this.button_compare.Name = "button_compare";
            this.button_compare.Size = new System.Drawing.Size(75, 23);
            this.button_compare.TabIndex = 0;
            this.button_compare.Text = "Compare all";
            this.button_compare.UseVisualStyleBackColor = true;
            this.button_compare.Click += new System.EventHandler(this.button_compare_Click);
            // 
            // button_clear
            // 
            this.button_clear.Enabled = false;
            this.button_clear.Location = new System.Drawing.Point(174, 12);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(75, 23);
            this.button_clear.TabIndex = 0;
            this.button_clear.Text = "Clear result";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 41);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 409);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView_table);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Table";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView_graph);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 383);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Graph";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView_graph
            // 
            this.dataGridView_graph.AllowDrop = true;
            this.dataGridView_graph.AllowUserToAddRows = false;
            this.dataGridView_graph.AllowUserToDeleteRows = false;
            this.dataGridView_graph.AllowUserToResizeRows = false;
            this.dataGridView_graph.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_graph.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_graph.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_graph.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView_graph.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_graph.Name = "dataGridView_graph";
            this.dataGridView_graph.ReadOnly = true;
            this.dataGridView_graph.RowHeadersVisible = false;
            this.dataGridView_graph.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_graph.Size = new System.Drawing.Size(746, 377);
            this.dataGridView_graph.TabIndex = 2;
            this.dataGridView_graph.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView_graph.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView_graph_DragDrop);
            this.dataGridView_graph.DragOver += new System.Windows.Forms.DragEventHandler(this.dataGridView_graph_DragOver);
            this.dataGridView_graph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView_graph_MouseDown);
            this.dataGridView_graph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridView_graph_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.button_compare);
            this.Controls.Add(this.button_openCsv);
            this.MinimumSize = new System.Drawing.Size(250, 150);
            this.Name = "Form1";
            this.Text = "CoinCompare";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_table)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_graph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_openCsv;
        private System.Windows.Forms.DataGridView dataGridView_table;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_compare;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView_graph;
    }
}


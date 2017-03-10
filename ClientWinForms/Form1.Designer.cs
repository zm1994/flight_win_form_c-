namespace ClientWinForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gMapControl = new GMap.NET.WindowsForms.GMapControl();
            this.txtBoxFrom = new System.Windows.Forms.TextBox();
            this.txtBoxTo = new System.Windows.Forms.TextBox();
            this.lstBoxVariants = new System.Windows.Forms.ListBox();
            this.numInfants = new System.Windows.Forms.NumericUpDown();
            this.numChild = new System.Windows.Forms.NumericUpDown();
            this.numAdults = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            ((System.ComponentModel.ISupportInitialize)(this.numInfants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChild)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdults)).BeginInit();
            this.SuspendLayout();
            // 
            // gMapControl
            // 
            this.gMapControl.Bearing = 0F;
            this.gMapControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gMapControl.CanDragMap = true;
            this.gMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl.GrayScaleMode = true;
            this.gMapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl.LevelsKeepInMemmory = 5;
            this.gMapControl.Location = new System.Drawing.Point(0, 0);
            this.gMapControl.MarkersEnabled = true;
            this.gMapControl.MaxZoom = 18;
            this.gMapControl.MinZoom = 2;
            this.gMapControl.MouseWheelZoomEnabled = true;
            this.gMapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl.Name = "gMapControl";
            this.gMapControl.NegativeMode = false;
            this.gMapControl.PolygonsEnabled = true;
            this.gMapControl.RetryLoadTile = 0;
            this.gMapControl.RoutesEnabled = true;
            this.gMapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl.ShowTileGridLines = false;
            this.gMapControl.Size = new System.Drawing.Size(1070, 261);
            this.gMapControl.TabIndex = 0;
            this.gMapControl.Zoom = 5D;
            this.gMapControl.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gMapControl_OnMarkerClick);
            // 
            // txtBoxFrom
            // 
            this.txtBoxFrom.Location = new System.Drawing.Point(13, 13);
            this.txtBoxFrom.Name = "txtBoxFrom";
            this.txtBoxFrom.Size = new System.Drawing.Size(100, 20);
            this.txtBoxFrom.TabIndex = 1;
            this.txtBoxFrom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBoxFrom_KeyUp);
            // 
            // txtBoxTo
            // 
            this.txtBoxTo.Location = new System.Drawing.Point(141, 13);
            this.txtBoxTo.Name = "txtBoxTo";
            this.txtBoxTo.Size = new System.Drawing.Size(100, 20);
            this.txtBoxTo.TabIndex = 2;
            this.txtBoxTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBoxTo_KeyUp);
            // 
            // lstBoxVariants
            // 
            this.lstBoxVariants.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBoxVariants.FormattingEnabled = true;
            this.lstBoxVariants.Location = new System.Drawing.Point(890, 12);
            this.lstBoxVariants.Name = "lstBoxVariants";
            this.lstBoxVariants.Size = new System.Drawing.Size(168, 238);
            this.lstBoxVariants.TabIndex = 3;
            this.lstBoxVariants.SelectedIndexChanged += new System.EventHandler(this.lstBoxVariants_SelectedIndexChanged);
            // 
            // numInfants
            // 
            this.numInfants.Location = new System.Drawing.Point(477, 13);
            this.numInfants.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numInfants.Name = "numInfants";
            this.numInfants.Size = new System.Drawing.Size(28, 20);
            this.numInfants.TabIndex = 5;
            // 
            // numChild
            // 
            this.numChild.Location = new System.Drawing.Point(402, 13);
            this.numChild.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numChild.Name = "numChild";
            this.numChild.Size = new System.Drawing.Size(28, 20);
            this.numChild.TabIndex = 6;
            // 
            // numAdults
            // 
            this.numAdults.Location = new System.Drawing.Point(303, 14);
            this.numAdults.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numAdults.Name = "numAdults";
            this.numAdults.Size = new System.Drawing.Size(28, 20);
            this.numAdults.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "adults";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = " children";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(436, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "infants";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(792, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(530, 12);
            this.monthCalendar.MinDate = new System.DateTime(2017, 3, 10, 0, 0, 0, 0);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.TabIndex = 12;
            this.monthCalendar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.monthCalendar_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 261);
            this.Controls.Add(this.monthCalendar);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numAdults);
            this.Controls.Add(this.numChild);
            this.Controls.Add(this.numInfants);
            this.Controls.Add(this.lstBoxVariants);
            this.Controls.Add(this.txtBoxTo);
            this.Controls.Add(this.txtBoxFrom);
            this.Controls.Add(this.gMapControl);
            this.Name = "Form1";
            this.Text = "Best Fly";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numInfants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChild)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMapControl;
        private System.Windows.Forms.TextBox txtBoxFrom;
        private System.Windows.Forms.TextBox txtBoxTo;
        private System.Windows.Forms.ListBox lstBoxVariants;
        private System.Windows.Forms.NumericUpDown numInfants;
        private System.Windows.Forms.NumericUpDown numChild;
        private System.Windows.Forms.NumericUpDown numAdults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.MonthCalendar monthCalendar;
    }
}


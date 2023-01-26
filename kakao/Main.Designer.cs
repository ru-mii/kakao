namespace kakao
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label_from = new System.Windows.Forms.Label();
            this.label_lastAverageBy = new System.Windows.Forms.Label();
            this.numericUpDown_averageBy = new System.Windows.Forms.NumericUpDown();
            this.label_averageTime = new System.Windows.Forms.Label();
            this.button_stopTimer = new System.Windows.Forms.Button();
            this.button_removeLastBest = new System.Windows.Forms.Button();
            this.comboBox_selectLevel = new System.Windows.Forms.ComboBox();
            this.label_bestTime = new System.Windows.Forms.Label();
            this.label_timer = new System.Windows.Forms.Label();
            this.rrcrxo = new System.Windows.Forms.Label();
            this.backgroundThread = new System.ComponentModel.BackgroundWorker();
            this.pressedStateThread = new System.ComponentModel.BackgroundWorker();
            this.buttonStateThread = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_settings = new System.Windows.Forms.Button();
            this.button_loadSettings = new System.Windows.Forms.Button();
            this.checkForUpdatesThread = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_averageBy)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_from
            // 
            this.label_from.Location = new System.Drawing.Point(317, 190);
            this.label_from.Name = "label_from";
            this.label_from.Size = new System.Drawing.Size(87, 18);
            this.label_from.TabIndex = 22;
            this.label_from.Text = "from total: ";
            this.label_from.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_lastAverageBy
            // 
            this.label_lastAverageBy.AutoSize = true;
            this.label_lastAverageBy.Location = new System.Drawing.Point(201, 190);
            this.label_lastAverageBy.Name = "label_lastAverageBy";
            this.label_lastAverageBy.Size = new System.Drawing.Size(23, 13);
            this.label_lastAverageBy.TabIndex = 21;
            this.label_lastAverageBy.Text = "last";
            // 
            // numericUpDown_averageBy
            // 
            this.numericUpDown_averageBy.Location = new System.Drawing.Point(230, 188);
            this.numericUpDown_averageBy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_averageBy.Name = "numericUpDown_averageBy";
            this.numericUpDown_averageBy.Size = new System.Drawing.Size(71, 20);
            this.numericUpDown_averageBy.TabIndex = 20;
            this.numericUpDown_averageBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown_averageBy.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label_averageTime
            // 
            this.label_averageTime.AutoSize = true;
            this.label_averageTime.Location = new System.Drawing.Point(13, 190);
            this.label_averageTime.Name = "label_averageTime";
            this.label_averageTime.Size = new System.Drawing.Size(74, 13);
            this.label_averageTime.TabIndex = 19;
            this.label_averageTime.Text = "average time: ";
            // 
            // button_stopTimer
            // 
            this.button_stopTimer.Location = new System.Drawing.Point(204, 159);
            this.button_stopTimer.Name = "button_stopTimer";
            this.button_stopTimer.Size = new System.Drawing.Size(97, 23);
            this.button_stopTimer.TabIndex = 18;
            this.button_stopTimer.Text = "stop timer";
            this.button_stopTimer.UseVisualStyleBackColor = true;
            this.button_stopTimer.Click += new System.EventHandler(this.button_stopTimer_Click);
            // 
            // button_removeLastBest
            // 
            this.button_removeLastBest.Location = new System.Drawing.Point(307, 159);
            this.button_removeLastBest.Name = "button_removeLastBest";
            this.button_removeLastBest.Size = new System.Drawing.Size(97, 23);
            this.button_removeLastBest.TabIndex = 17;
            this.button_removeLastBest.Text = "remove last best";
            this.button_removeLastBest.UseVisualStyleBackColor = true;
            this.button_removeLastBest.Click += new System.EventHandler(this.button_removeLastBest_Click);
            // 
            // comboBox_selectLevel
            // 
            this.comboBox_selectLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_selectLevel.Items.AddRange(new object[] {
            "The Ship",
            "Beavers\' Forest",
            "The Great Escape",
            "Great Trees",
            "River Raid",
            "Shaman\'s Cave",
            "Igloo Village",
            "Ice Cave",
            "Down The Mountain",
            "Crystal Mines",
            "The Station",
            "The Race",
            "Hostile Reef",
            "Deep Ocean",
            "Lair of Poison",
            "Trip to Island",
            "Treasure Island",
            "The Volcano",
            "Abandoned Town",
            "Hunter\'s Galleon",
            "Final Duel"});
            this.comboBox_selectLevel.Location = new System.Drawing.Point(13, 132);
            this.comboBox_selectLevel.Name = "comboBox_selectLevel";
            this.comboBox_selectLevel.Size = new System.Drawing.Size(362, 21);
            this.comboBox_selectLevel.TabIndex = 16;
            this.comboBox_selectLevel.SelectedIndexChanged += new System.EventHandler(this.comboBox_selectLevel_SelectedIndexChanged);
            // 
            // label_bestTime
            // 
            this.label_bestTime.AutoSize = true;
            this.label_bestTime.Location = new System.Drawing.Point(13, 164);
            this.label_bestTime.Name = "label_bestTime";
            this.label_bestTime.Size = new System.Drawing.Size(55, 13);
            this.label_bestTime.TabIndex = 15;
            this.label_bestTime.Text = "best time: ";
            // 
            // label_timer
            // 
            this.label_timer.BackColor = System.Drawing.Color.Black;
            this.label_timer.Font = new System.Drawing.Font("Microsoft Sans Serif", 58F);
            this.label_timer.ForeColor = System.Drawing.Color.LimeGreen;
            this.label_timer.Location = new System.Drawing.Point(13, 9);
            this.label_timer.Name = "label_timer";
            this.label_timer.Size = new System.Drawing.Size(391, 118);
            this.label_timer.TabIndex = 14;
            this.label_timer.Text = "00:00.000";
            this.label_timer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rrcrxo
            // 
            this.rrcrxo.AutoSize = true;
            this.rrcrxo.Location = new System.Drawing.Point(32000, 32000);
            this.rrcrxo.Name = "rrcrxo";
            this.rrcrxo.Size = new System.Drawing.Size(0, 13);
            this.rrcrxo.TabIndex = 24;
            // 
            // backgroundThread
            // 
            this.backgroundThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundThread_DoWork);
            // 
            // pressedStateThread
            // 
            this.pressedStateThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.pressedStateThread_DoWork);
            // 
            // buttonStateThread
            // 
            this.buttonStateThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.buttonStateThread_DoWork);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(117, 26);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // button_settings
            // 
            this.button_settings.Image = global::kakao.Properties.Resources.gear;
            this.button_settings.Location = new System.Drawing.Point(380, 131);
            this.button_settings.Name = "button_settings";
            this.button_settings.Size = new System.Drawing.Size(23, 23);
            this.button_settings.TabIndex = 23;
            this.button_settings.UseVisualStyleBackColor = true;
            this.button_settings.Click += new System.EventHandler(this.OpenSettings);
            // 
            // button_loadSettings
            // 
            this.button_loadSettings.Location = new System.Drawing.Point(32000, 32000);
            this.button_loadSettings.Name = "button_loadSettings";
            this.button_loadSettings.Size = new System.Drawing.Size(75, 23);
            this.button_loadSettings.TabIndex = 25;
            this.button_loadSettings.UseVisualStyleBackColor = true;
            this.button_loadSettings.Click += new System.EventHandler(this.button_loadSettings_Click);
            // 
            // checkForUpdatesThread
            // 
            this.checkForUpdatesThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.checkForUpdatesThread_DoWork);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 217);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.button_loadSettings);
            this.Controls.Add(this.rrcrxo);
            this.Controls.Add(this.button_settings);
            this.Controls.Add(this.label_from);
            this.Controls.Add(this.label_lastAverageBy);
            this.Controls.Add(this.numericUpDown_averageBy);
            this.Controls.Add(this.label_averageTime);
            this.Controls.Add(this.button_stopTimer);
            this.Controls.Add(this.button_removeLastBest);
            this.Controls.Add(this.comboBox_selectLevel);
            this.Controls.Add(this.label_bestTime);
            this.Controls.Add(this.label_timer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "kakao";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Click += new System.EventHandler(this.Main_Click);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_averageBy)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_from;
        private System.Windows.Forms.Label label_lastAverageBy;
        private System.Windows.Forms.NumericUpDown numericUpDown_averageBy;
        private System.Windows.Forms.Label label_averageTime;
        private System.Windows.Forms.Button button_stopTimer;
        private System.Windows.Forms.Button button_removeLastBest;
        private System.Windows.Forms.ComboBox comboBox_selectLevel;
        private System.Windows.Forms.Label label_bestTime;
        private System.Windows.Forms.Label label_timer;
        private System.Windows.Forms.Button button_settings;
        private System.Windows.Forms.Label rrcrxo;
        private System.ComponentModel.BackgroundWorker backgroundThread;
        private System.ComponentModel.BackgroundWorker pressedStateThread;
        private System.ComponentModel.BackgroundWorker buttonStateThread;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Button button_loadSettings;
        private System.ComponentModel.BackgroundWorker checkForUpdatesThread;
    }
}


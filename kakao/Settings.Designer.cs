namespace kakao
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.checkBox_LEVELS_includePelicanDialogueSkip = new System.Windows.Forms.CheckBox();
            this.label_includePelicanDialogueSkip = new System.Windows.Forms.Label();
            this.comboBox_TIMER_compareAgainst = new System.Windows.Forms.ComboBox();
            this.label_compareAgainst = new System.Windows.Forms.Label();
            this.button_resetToDefault = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_WINDOW_topMost = new System.Windows.Forms.CheckBox();
            this.label_topMost = new System.Windows.Forms.Label();
            this.button_WINDOW_formFontColor = new System.Windows.Forms.Button();
            this.label_formFontColor = new System.Windows.Forms.Label();
            this.checkBox_WINDOW_showBorder = new System.Windows.Forms.CheckBox();
            this.label_showBorder = new System.Windows.Forms.Label();
            this.checkBox_WINDOW_showTimerOnly = new System.Windows.Forms.CheckBox();
            this.label_showTimerOnly = new System.Windows.Forms.Label();
            this.button_gamepad = new System.Windows.Forms.Button();
            this.button_WINDOW_formBackColor = new System.Windows.Forms.Button();
            this.label_formBackColor = new System.Windows.Forms.Label();
            this.label_titleProgram = new System.Windows.Forms.Label();
            this.phpcmi = new System.Windows.Forms.Label();
            this.checkBox_LEVELS_includeTheShipInBeaversForest = new System.Windows.Forms.CheckBox();
            this.label_includeTheShipInBeaversForest = new System.Windows.Forms.Label();
            this.textBox_LEVELS_loadLevelHotkey = new System.Windows.Forms.TextBox();
            this.label_loadLevelHotkey = new System.Windows.Forms.Label();
            this.label_titleLevels = new System.Windows.Forms.Label();
            this.button_TIMER_timeBehindColor = new System.Windows.Forms.Button();
            this.label_timeBehindColor = new System.Windows.Forms.Label();
            this.label_titleTimer = new System.Windows.Forms.Label();
            this.button_TIMER_timeAheadColor = new System.Windows.Forms.Button();
            this.button_TIMER_backColor = new System.Windows.Forms.Button();
            this.sioyui = new System.Windows.Forms.Label();
            this.label_timeAheadColor = new System.Windows.Forms.Label();
            this.label_backColor = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.rrcrxo = new System.Windows.Forms.Label();
            this.label_livesplitCompatibility = new System.Windows.Forms.Label();
            this.checkBox_LEVELS_livesplitCompatibility = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox_LEVELS_includePelicanDialogueSkip
            // 
            this.checkBox_LEVELS_includePelicanDialogueSkip.AutoSize = true;
            this.checkBox_LEVELS_includePelicanDialogueSkip.Checked = true;
            this.checkBox_LEVELS_includePelicanDialogueSkip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_LEVELS_includePelicanDialogueSkip.Location = new System.Drawing.Point(298, 215);
            this.checkBox_LEVELS_includePelicanDialogueSkip.Name = "checkBox_LEVELS_includePelicanDialogueSkip";
            this.checkBox_LEVELS_includePelicanDialogueSkip.Size = new System.Drawing.Size(15, 14);
            this.checkBox_LEVELS_includePelicanDialogueSkip.TabIndex = 66;
            this.checkBox_LEVELS_includePelicanDialogueSkip.UseVisualStyleBackColor = true;
            this.checkBox_LEVELS_includePelicanDialogueSkip.CheckedChanged += new System.EventHandler(this.CheckboxClick);
            // 
            // label_includePelicanDialogueSkip
            // 
            this.label_includePelicanDialogueSkip.AutoSize = true;
            this.label_includePelicanDialogueSkip.Location = new System.Drawing.Point(18, 215);
            this.label_includePelicanDialogueSkip.Name = "label_includePelicanDialogueSkip";
            this.label_includePelicanDialogueSkip.Size = new System.Drawing.Size(148, 13);
            this.label_includePelicanDialogueSkip.TabIndex = 65;
            this.label_includePelicanDialogueSkip.Text = "include Pelican Dialogue Skip";
            // 
            // comboBox_TIMER_compareAgainst
            // 
            this.comboBox_TIMER_compareAgainst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TIMER_compareAgainst.IntegralHeight = false;
            this.comboBox_TIMER_compareAgainst.Items.AddRange(new object[] {
            "best",
            "average"});
            this.comboBox_TIMER_compareAgainst.Location = new System.Drawing.Point(241, 111);
            this.comboBox_TIMER_compareAgainst.Name = "comboBox_TIMER_compareAgainst";
            this.comboBox_TIMER_compareAgainst.Size = new System.Drawing.Size(72, 21);
            this.comboBox_TIMER_compareAgainst.TabIndex = 64;
            this.comboBox_TIMER_compareAgainst.SelectedIndexChanged += new System.EventHandler(this.DropdownChanged);
            // 
            // label_compareAgainst
            // 
            this.label_compareAgainst.AutoSize = true;
            this.label_compareAgainst.Location = new System.Drawing.Point(15, 114);
            this.label_compareAgainst.Name = "label_compareAgainst";
            this.label_compareAgainst.Size = new System.Drawing.Size(85, 13);
            this.label_compareAgainst.TabIndex = 63;
            this.label_compareAgainst.Text = "compare against";
            // 
            // button_resetToDefault
            // 
            this.button_resetToDefault.Location = new System.Drawing.Point(8, 471);
            this.button_resetToDefault.Name = "button_resetToDefault";
            this.button_resetToDefault.Size = new System.Drawing.Size(305, 23);
            this.button_resetToDefault.TabIndex = 62;
            this.button_resetToDefault.Text = "reset to default (timer will reopen)";
            this.button_resetToDefault.UseVisualStyleBackColor = true;
            this.button_resetToDefault.Click += new System.EventHandler(this.button_resetToDefault_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(-78, 462);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(500, 2);
            this.label1.TabIndex = 61;
            // 
            // checkBox_WINDOW_topMost
            // 
            this.checkBox_WINDOW_topMost.AutoSize = true;
            this.checkBox_WINDOW_topMost.Location = new System.Drawing.Point(298, 432);
            this.checkBox_WINDOW_topMost.Name = "checkBox_WINDOW_topMost";
            this.checkBox_WINDOW_topMost.Size = new System.Drawing.Size(15, 14);
            this.checkBox_WINDOW_topMost.TabIndex = 60;
            this.checkBox_WINDOW_topMost.UseVisualStyleBackColor = true;
            this.checkBox_WINDOW_topMost.CheckedChanged += new System.EventHandler(this.CheckboxClick);
            // 
            // label_topMost
            // 
            this.label_topMost.AutoSize = true;
            this.label_topMost.Location = new System.Drawing.Point(18, 432);
            this.label_topMost.Name = "label_topMost";
            this.label_topMost.Size = new System.Drawing.Size(47, 13);
            this.label_topMost.TabIndex = 59;
            this.label_topMost.Text = "top most";
            // 
            // button_WINDOW_formFontColor
            // 
            this.button_WINDOW_formFontColor.BackColor = System.Drawing.Color.White;
            this.button_WINDOW_formFontColor.Location = new System.Drawing.Point(292, 360);
            this.button_WINDOW_formFontColor.Name = "button_WINDOW_formFontColor";
            this.button_WINDOW_formFontColor.Size = new System.Drawing.Size(23, 17);
            this.button_WINDOW_formFontColor.TabIndex = 58;
            this.button_WINDOW_formFontColor.UseVisualStyleBackColor = false;
            this.button_WINDOW_formFontColor.Click += new System.EventHandler(this.SelectColor);
            // 
            // label_formFontColor
            // 
            this.label_formFontColor.AutoSize = true;
            this.label_formFontColor.Location = new System.Drawing.Point(17, 363);
            this.label_formFontColor.Name = "label_formFontColor";
            this.label_formFontColor.Size = new System.Drawing.Size(74, 13);
            this.label_formFontColor.TabIndex = 57;
            this.label_formFontColor.Text = "form font color";
            // 
            // checkBox_WINDOW_showBorder
            // 
            this.checkBox_WINDOW_showBorder.AutoSize = true;
            this.checkBox_WINDOW_showBorder.Checked = true;
            this.checkBox_WINDOW_showBorder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_WINDOW_showBorder.Location = new System.Drawing.Point(298, 409);
            this.checkBox_WINDOW_showBorder.Name = "checkBox_WINDOW_showBorder";
            this.checkBox_WINDOW_showBorder.Size = new System.Drawing.Size(15, 14);
            this.checkBox_WINDOW_showBorder.TabIndex = 56;
            this.checkBox_WINDOW_showBorder.UseVisualStyleBackColor = true;
            this.checkBox_WINDOW_showBorder.CheckedChanged += new System.EventHandler(this.CheckboxClick);
            // 
            // label_showBorder
            // 
            this.label_showBorder.AutoSize = true;
            this.label_showBorder.Location = new System.Drawing.Point(18, 409);
            this.label_showBorder.Name = "label_showBorder";
            this.label_showBorder.Size = new System.Drawing.Size(65, 13);
            this.label_showBorder.TabIndex = 55;
            this.label_showBorder.Text = "show border";
            // 
            // checkBox_WINDOW_showTimerOnly
            // 
            this.checkBox_WINDOW_showTimerOnly.AutoSize = true;
            this.checkBox_WINDOW_showTimerOnly.Location = new System.Drawing.Point(298, 386);
            this.checkBox_WINDOW_showTimerOnly.Name = "checkBox_WINDOW_showTimerOnly";
            this.checkBox_WINDOW_showTimerOnly.Size = new System.Drawing.Size(15, 14);
            this.checkBox_WINDOW_showTimerOnly.TabIndex = 54;
            this.checkBox_WINDOW_showTimerOnly.UseVisualStyleBackColor = true;
            this.checkBox_WINDOW_showTimerOnly.CheckedChanged += new System.EventHandler(this.CheckboxClick);
            // 
            // label_showTimerOnly
            // 
            this.label_showTimerOnly.AutoSize = true;
            this.label_showTimerOnly.Location = new System.Drawing.Point(18, 386);
            this.label_showTimerOnly.Name = "label_showTimerOnly";
            this.label_showTimerOnly.Size = new System.Drawing.Size(79, 13);
            this.label_showTimerOnly.TabIndex = 53;
            this.label_showTimerOnly.Text = "show timer only";
            // 
            // button_gamepad
            // 
            this.button_gamepad.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_gamepad.Location = new System.Drawing.Point(214, 258);
            this.button_gamepad.Name = "button_gamepad";
            this.button_gamepad.Size = new System.Drawing.Size(23, 16);
            this.button_gamepad.TabIndex = 52;
            this.button_gamepad.Text = "G";
            this.button_gamepad.UseVisualStyleBackColor = true;
            this.button_gamepad.Click += new System.EventHandler(this.button_gamepad_Click);
            // 
            // button_WINDOW_formBackColor
            // 
            this.button_WINDOW_formBackColor.BackColor = System.Drawing.Color.Black;
            this.button_WINDOW_formBackColor.Location = new System.Drawing.Point(293, 338);
            this.button_WINDOW_formBackColor.Name = "button_WINDOW_formBackColor";
            this.button_WINDOW_formBackColor.Size = new System.Drawing.Size(23, 17);
            this.button_WINDOW_formBackColor.TabIndex = 51;
            this.button_WINDOW_formBackColor.UseVisualStyleBackColor = false;
            this.button_WINDOW_formBackColor.Click += new System.EventHandler(this.SelectColor);
            // 
            // label_formBackColor
            // 
            this.label_formBackColor.AutoSize = true;
            this.label_formBackColor.Location = new System.Drawing.Point(18, 340);
            this.label_formBackColor.Name = "label_formBackColor";
            this.label_formBackColor.Size = new System.Drawing.Size(80, 13);
            this.label_formBackColor.TabIndex = 50;
            this.label_formBackColor.Text = "form back color";
            // 
            // label_titleProgram
            // 
            this.label_titleProgram.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.label_titleProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_titleProgram.ForeColor = System.Drawing.Color.White;
            this.label_titleProgram.Location = new System.Drawing.Point(18, 303);
            this.label_titleProgram.Name = "label_titleProgram";
            this.label_titleProgram.Size = new System.Drawing.Size(298, 23);
            this.label_titleProgram.TabIndex = 49;
            this.label_titleProgram.Text = "window";
            this.label_titleProgram.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // phpcmi
            // 
            this.phpcmi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.phpcmi.Location = new System.Drawing.Point(-86, 291);
            this.phpcmi.Name = "phpcmi";
            this.phpcmi.Size = new System.Drawing.Size(500, 2);
            this.phpcmi.TabIndex = 48;
            // 
            // checkBox_LEVELS_includeTheShipInBeaversForest
            // 
            this.checkBox_LEVELS_includeTheShipInBeaversForest.AutoSize = true;
            this.checkBox_LEVELS_includeTheShipInBeaversForest.Checked = true;
            this.checkBox_LEVELS_includeTheShipInBeaversForest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_LEVELS_includeTheShipInBeaversForest.Location = new System.Drawing.Point(298, 192);
            this.checkBox_LEVELS_includeTheShipInBeaversForest.Name = "checkBox_LEVELS_includeTheShipInBeaversForest";
            this.checkBox_LEVELS_includeTheShipInBeaversForest.Size = new System.Drawing.Size(15, 14);
            this.checkBox_LEVELS_includeTheShipInBeaversForest.TabIndex = 47;
            this.checkBox_LEVELS_includeTheShipInBeaversForest.UseVisualStyleBackColor = true;
            this.checkBox_LEVELS_includeTheShipInBeaversForest.CheckedChanged += new System.EventHandler(this.CheckboxClick);
            // 
            // label_includeTheShipInBeaversForest
            // 
            this.label_includeTheShipInBeaversForest.AutoSize = true;
            this.label_includeTheShipInBeaversForest.Location = new System.Drawing.Point(18, 192);
            this.label_includeTheShipInBeaversForest.Name = "label_includeTheShipInBeaversForest";
            this.label_includeTheShipInBeaversForest.Size = new System.Drawing.Size(174, 13);
            this.label_includeTheShipInBeaversForest.TabIndex = 46;
            this.label_includeTheShipInBeaversForest.Text = "include The Ship in Beavers\' Forest";
            // 
            // textBox_LEVELS_loadLevelHotkey
            // 
            this.textBox_LEVELS_loadLevelHotkey.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.textBox_LEVELS_loadLevelHotkey.Location = new System.Drawing.Point(241, 258);
            this.textBox_LEVELS_loadLevelHotkey.Multiline = true;
            this.textBox_LEVELS_loadLevelHotkey.Name = "textBox_LEVELS_loadLevelHotkey";
            this.textBox_LEVELS_loadLevelHotkey.ReadOnly = true;
            this.textBox_LEVELS_loadLevelHotkey.Size = new System.Drawing.Size(72, 16);
            this.textBox_LEVELS_loadLevelHotkey.TabIndex = 45;
            this.textBox_LEVELS_loadLevelHotkey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_LEVELS_loadLevelHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_LEVELS_loadLevelHotkey_KeyDown);
            // 
            // label_loadLevelHotkey
            // 
            this.label_loadLevelHotkey.AutoSize = true;
            this.label_loadLevelHotkey.Location = new System.Drawing.Point(18, 261);
            this.label_loadLevelHotkey.Name = "label_loadLevelHotkey";
            this.label_loadLevelHotkey.Size = new System.Drawing.Size(87, 13);
            this.label_loadLevelHotkey.TabIndex = 44;
            this.label_loadLevelHotkey.Text = "load level hotkey";
            // 
            // label_titleLevels
            // 
            this.label_titleLevels.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.label_titleLevels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_titleLevels.ForeColor = System.Drawing.Color.White;
            this.label_titleLevels.Location = new System.Drawing.Point(15, 156);
            this.label_titleLevels.Name = "label_titleLevels";
            this.label_titleLevels.Size = new System.Drawing.Size(298, 23);
            this.label_titleLevels.TabIndex = 43;
            this.label_titleLevels.Text = "levels";
            this.label_titleLevels.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_TIMER_timeBehindColor
            // 
            this.button_TIMER_timeBehindColor.BackColor = System.Drawing.Color.Red;
            this.button_TIMER_timeBehindColor.Location = new System.Drawing.Point(290, 89);
            this.button_TIMER_timeBehindColor.Name = "button_TIMER_timeBehindColor";
            this.button_TIMER_timeBehindColor.Size = new System.Drawing.Size(23, 17);
            this.button_TIMER_timeBehindColor.TabIndex = 42;
            this.button_TIMER_timeBehindColor.UseVisualStyleBackColor = false;
            this.button_TIMER_timeBehindColor.Click += new System.EventHandler(this.SelectColor);
            // 
            // label_timeBehindColor
            // 
            this.label_timeBehindColor.AutoSize = true;
            this.label_timeBehindColor.Location = new System.Drawing.Point(15, 91);
            this.label_timeBehindColor.Name = "label_timeBehindColor";
            this.label_timeBehindColor.Size = new System.Drawing.Size(87, 13);
            this.label_timeBehindColor.TabIndex = 41;
            this.label_timeBehindColor.Text = "time behind color";
            // 
            // label_titleTimer
            // 
            this.label_titleTimer.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.label_titleTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_titleTimer.ForeColor = System.Drawing.Color.White;
            this.label_titleTimer.Location = new System.Drawing.Point(15, 8);
            this.label_titleTimer.Name = "label_titleTimer";
            this.label_titleTimer.Size = new System.Drawing.Size(298, 23);
            this.label_titleTimer.TabIndex = 40;
            this.label_titleTimer.Text = "timer";
            this.label_titleTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_TIMER_timeAheadColor
            // 
            this.button_TIMER_timeAheadColor.BackColor = System.Drawing.Color.LimeGreen;
            this.button_TIMER_timeAheadColor.Location = new System.Drawing.Point(290, 66);
            this.button_TIMER_timeAheadColor.Name = "button_TIMER_timeAheadColor";
            this.button_TIMER_timeAheadColor.Size = new System.Drawing.Size(23, 17);
            this.button_TIMER_timeAheadColor.TabIndex = 39;
            this.button_TIMER_timeAheadColor.UseVisualStyleBackColor = false;
            this.button_TIMER_timeAheadColor.Click += new System.EventHandler(this.SelectColor);
            // 
            // button_TIMER_backColor
            // 
            this.button_TIMER_backColor.BackColor = System.Drawing.Color.Black;
            this.button_TIMER_backColor.Location = new System.Drawing.Point(290, 43);
            this.button_TIMER_backColor.Name = "button_TIMER_backColor";
            this.button_TIMER_backColor.Size = new System.Drawing.Size(23, 17);
            this.button_TIMER_backColor.TabIndex = 38;
            this.button_TIMER_backColor.UseVisualStyleBackColor = false;
            this.button_TIMER_backColor.Click += new System.EventHandler(this.SelectColor);
            // 
            // sioyui
            // 
            this.sioyui.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sioyui.Location = new System.Drawing.Point(-8, 144);
            this.sioyui.Name = "sioyui";
            this.sioyui.Size = new System.Drawing.Size(500, 2);
            this.sioyui.TabIndex = 37;
            // 
            // label_timeAheadColor
            // 
            this.label_timeAheadColor.AutoSize = true;
            this.label_timeAheadColor.Location = new System.Drawing.Point(15, 68);
            this.label_timeAheadColor.Name = "label_timeAheadColor";
            this.label_timeAheadColor.Size = new System.Drawing.Size(85, 13);
            this.label_timeAheadColor.TabIndex = 36;
            this.label_timeAheadColor.Text = "time ahead color";
            // 
            // label_backColor
            // 
            this.label_backColor.AutoSize = true;
            this.label_backColor.Location = new System.Drawing.Point(15, 45);
            this.label_backColor.Name = "label_backColor";
            this.label_backColor.Size = new System.Drawing.Size(57, 13);
            this.label_backColor.TabIndex = 35;
            this.label_backColor.Text = "back color";
            // 
            // rrcrxo
            // 
            this.rrcrxo.AutoSize = true;
            this.rrcrxo.Location = new System.Drawing.Point(32000, 32000);
            this.rrcrxo.Name = "rrcrxo";
            this.rrcrxo.Size = new System.Drawing.Size(0, 13);
            this.rrcrxo.TabIndex = 67;
            // 
            // label_livesplitCompatibility
            // 
            this.label_livesplitCompatibility.AutoSize = true;
            this.label_livesplitCompatibility.Location = new System.Drawing.Point(18, 238);
            this.label_livesplitCompatibility.Name = "label_livesplitCompatibility";
            this.label_livesplitCompatibility.Size = new System.Drawing.Size(101, 13);
            this.label_livesplitCompatibility.TabIndex = 68;
            this.label_livesplitCompatibility.Text = "livesplit compatibility";
            // 
            // checkBox_LEVELS_livesplitCompatibility
            // 
            this.checkBox_LEVELS_livesplitCompatibility.AutoSize = true;
            this.checkBox_LEVELS_livesplitCompatibility.Checked = true;
            this.checkBox_LEVELS_livesplitCompatibility.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_LEVELS_livesplitCompatibility.Location = new System.Drawing.Point(298, 238);
            this.checkBox_LEVELS_livesplitCompatibility.Name = "checkBox_LEVELS_livesplitCompatibility";
            this.checkBox_LEVELS_livesplitCompatibility.Size = new System.Drawing.Size(15, 14);
            this.checkBox_LEVELS_livesplitCompatibility.TabIndex = 69;
            this.checkBox_LEVELS_livesplitCompatibility.UseVisualStyleBackColor = true;
            this.checkBox_LEVELS_livesplitCompatibility.CheckedChanged += new System.EventHandler(this.CheckboxClick);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 499);
            this.Controls.Add(this.checkBox_LEVELS_livesplitCompatibility);
            this.Controls.Add(this.label_livesplitCompatibility);
            this.Controls.Add(this.rrcrxo);
            this.Controls.Add(this.checkBox_LEVELS_includePelicanDialogueSkip);
            this.Controls.Add(this.label_includePelicanDialogueSkip);
            this.Controls.Add(this.comboBox_TIMER_compareAgainst);
            this.Controls.Add(this.label_compareAgainst);
            this.Controls.Add(this.button_resetToDefault);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_WINDOW_topMost);
            this.Controls.Add(this.label_topMost);
            this.Controls.Add(this.button_WINDOW_formFontColor);
            this.Controls.Add(this.label_formFontColor);
            this.Controls.Add(this.checkBox_WINDOW_showBorder);
            this.Controls.Add(this.label_showBorder);
            this.Controls.Add(this.checkBox_WINDOW_showTimerOnly);
            this.Controls.Add(this.label_showTimerOnly);
            this.Controls.Add(this.button_gamepad);
            this.Controls.Add(this.button_WINDOW_formBackColor);
            this.Controls.Add(this.label_formBackColor);
            this.Controls.Add(this.label_titleProgram);
            this.Controls.Add(this.phpcmi);
            this.Controls.Add(this.checkBox_LEVELS_includeTheShipInBeaversForest);
            this.Controls.Add(this.label_includeTheShipInBeaversForest);
            this.Controls.Add(this.textBox_LEVELS_loadLevelHotkey);
            this.Controls.Add(this.label_loadLevelHotkey);
            this.Controls.Add(this.label_titleLevels);
            this.Controls.Add(this.button_TIMER_timeBehindColor);
            this.Controls.Add(this.label_timeBehindColor);
            this.Controls.Add(this.label_titleTimer);
            this.Controls.Add(this.button_TIMER_timeAheadColor);
            this.Controls.Add(this.button_TIMER_backColor);
            this.Controls.Add(this.sioyui);
            this.Controls.Add(this.label_timeAheadColor);
            this.Controls.Add(this.label_backColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.Text = "settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.Click += new System.EventHandler(this.Settings_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_LEVELS_includePelicanDialogueSkip;
        private System.Windows.Forms.Label label_includePelicanDialogueSkip;
        private System.Windows.Forms.ComboBox comboBox_TIMER_compareAgainst;
        private System.Windows.Forms.Label label_compareAgainst;
        private System.Windows.Forms.Button button_resetToDefault;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_WINDOW_topMost;
        private System.Windows.Forms.Label label_topMost;
        private System.Windows.Forms.Button button_WINDOW_formFontColor;
        private System.Windows.Forms.Label label_formFontColor;
        private System.Windows.Forms.CheckBox checkBox_WINDOW_showBorder;
        private System.Windows.Forms.Label label_showBorder;
        private System.Windows.Forms.CheckBox checkBox_WINDOW_showTimerOnly;
        private System.Windows.Forms.Label label_showTimerOnly;
        private System.Windows.Forms.Button button_gamepad;
        private System.Windows.Forms.Button button_WINDOW_formBackColor;
        private System.Windows.Forms.Label label_formBackColor;
        private System.Windows.Forms.Label label_titleProgram;
        private System.Windows.Forms.Label phpcmi;
        private System.Windows.Forms.CheckBox checkBox_LEVELS_includeTheShipInBeaversForest;
        private System.Windows.Forms.Label label_includeTheShipInBeaversForest;
        private System.Windows.Forms.TextBox textBox_LEVELS_loadLevelHotkey;
        private System.Windows.Forms.Label label_loadLevelHotkey;
        private System.Windows.Forms.Label label_titleLevels;
        private System.Windows.Forms.Button button_TIMER_timeBehindColor;
        private System.Windows.Forms.Label label_timeBehindColor;
        private System.Windows.Forms.Label label_titleTimer;
        private System.Windows.Forms.Button button_TIMER_timeAheadColor;
        private System.Windows.Forms.Button button_TIMER_backColor;
        private System.Windows.Forms.Label sioyui;
        private System.Windows.Forms.Label label_timeAheadColor;
        private System.Windows.Forms.Label label_backColor;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label rrcrxo;
        private System.Windows.Forms.Label label_livesplitCompatibility;
        private System.Windows.Forms.CheckBox checkBox_LEVELS_livesplitCompatibility;
    }
}
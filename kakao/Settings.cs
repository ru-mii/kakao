using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kakao
{
    public partial class Settings : Form
    {
        // initializing component, ignore
        public Settings() { InitializeComponent(); }

        // for converting colors
        ColorConverter colorConverter = new ColorConverter();

        // runs on form load
        private void Settings_Load(object sender, EventArgs e)
        {
            // select oob control for better visuals
            rrcrxo.Select();

            // select default dropdown
            comboBox_TIMER_compareAgainst.SelectedIndex = 1;

            // load settings into form
            LoadSettingsToForm();
        }

        // when color selecting button is clicked
        private void SelectColor(object sender, EventArgs e)
        {
            // show color dialog
            colorDialog.ShowDialog();

            // get sender
            Button clickedButton = sender as Button;

            // change button color
            clickedButton.BackColor = colorDialog.Color;

            // convert color to hex
            string colorHex = (colorDialog.Color.ToArgb() & 0x00FFFFFF).ToString("X6");

            // save color as hex in settings
            Saves.Save("settings", clickedButton.Name, colorHex);

            // reload settings to program
            LoadSettingsToMain();
        }

        // dropdown changed
        private void DropdownChanged(object sender, EventArgs e)
        {
            // get sender
            ComboBox changedDropdown = sender as ComboBox;

            // save settings
            Saves.Save("settings", changedDropdown.Name, changedDropdown.SelectedIndex.ToString());
        }

        // when checkbox is clicked
        private void CheckboxClick(object sender, EventArgs e)
        {
            // get sender
            CheckBox clickedCheckbox = sender as CheckBox;

            // save settings
            Saves.Save("settings", clickedCheckbox.Name, clickedCheckbox.Checked.ToString());

            // reload settings to program
            LoadSettingsToMain();
        }

        // get hotkey from keyboard and save it
        private void textBox_LEVELS_loadLevelHotkey_KeyDown(object sender, KeyEventArgs e)
        {
            Saves.Save("settings", "textBox_LEVELS_loadLevelHotkey", e.KeyCode.ToString());
            Saves.Save("settings", "textBox_LEVELS_loadLevelHotkeyValue", e.KeyValue.ToString());
            textBox_LEVELS_loadLevelHotkey.Text = e.KeyCode.ToString();

            // reload settings
            LoadSettingsToMain();
        }

        // reload settings by using hidden load settings button from main form
        private void LoadSettingsToMain()
        {
            Button button_loadSettings = Application.OpenForms["Main"].Controls["button_loadSettings"] as Button;
            button_loadSettings.PerformClick();
        }

        // loads saved settings
        private void LoadSettingsToForm()
        {
            // ---------------------- TIMER

            // button_TIMER_backColor
            string timer1 = Saves.Read("settings", "button_TIMER_backColor");
            if (timer1 != "") button_TIMER_backColor.BackColor = (Color)colorConverter.ConvertFromString("#" + timer1);
            else button_TIMER_backColor.BackColor = Color.Black;

            // button_TIMER_timeAheadColor
            string timer2 = Saves.Read("settings", "button_TIMER_timeAheadColor");
            if (timer2 != "") button_TIMER_timeAheadColor.BackColor = (Color)colorConverter.ConvertFromString("#" + timer2);
            else button_TIMER_timeAheadColor.BackColor = Color.LimeGreen;

            // button_TIMER_timeBehindColor
            string timer3 = Saves.Read("settings", "button_TIMER_timeBehindColor");
            if (timer3 != "") button_TIMER_timeBehindColor.BackColor = (Color)colorConverter.ConvertFromString("#" + timer3);
            else button_TIMER_timeBehindColor.BackColor = Color.Red;

            // comboBox_TIMER_compareAgainst
            string timer4 = Saves.Read("settings", "comboBox_TIMER_compareAgainst");
            if (timer4 != "") comboBox_TIMER_compareAgainst.SelectedIndex = int.Parse(timer4);
            else comboBox_TIMER_compareAgainst.SelectedIndex = 1;

            // ---------------------- LEVELS

            // checkBox_LEVELS_includeTheShipInBeaversForest
            string levels1 = Saves.Read("settings", "checkBox_LEVELS_includeTheShipInBeaversForest");
            if (levels1 != "") checkBox_LEVELS_includeTheShipInBeaversForest.Checked = bool.Parse(levels1);
            else checkBox_LEVELS_includeTheShipInBeaversForest.Checked = true;

            // textBox_LEVELS_loadLevelHotkey
            string levels2 = Saves.Read("settings", "textBox_LEVELS_loadLevelHotkey");
            if (levels2 != "") textBox_LEVELS_loadLevelHotkey.Text = levels2;
            else textBox_LEVELS_loadLevelHotkey.Text = "";

            string levels3 = Saves.Read("settings", "checkBox_LEVELS_includePelicanDialogueSkip");
            if (levels3 != "") checkBox_LEVELS_includePelicanDialogueSkip.Checked = bool.Parse(levels3);
            else checkBox_LEVELS_includePelicanDialogueSkip.Checked = true;

            // ---------------------- WINDOW

            // button_WINDOW_formBackColor
            string window1 = Saves.Read("settings", "button_WINDOW_formBackColor");
            if (window1 != "") button_WINDOW_formBackColor.BackColor = (Color)colorConverter.ConvertFromString("#" + window1);
            else button_WINDOW_formBackColor.BackColor = Color.Black;

            // button_WINDOW_formFontColor
            string window2 = Saves.Read("settings", "button_WINDOW_formFontColor");
            if (window2 != "") button_WINDOW_formFontColor.BackColor = (Color)colorConverter.ConvertFromString("#" + window2);
            else button_WINDOW_formFontColor.BackColor = Color.White;

            // checkBox_WINDOW_showTimerOnly
            string window3 = Saves.Read("settings", "checkBox_WINDOW_showTimerOnly");
            if (window3 != "") checkBox_WINDOW_showTimerOnly.Checked = bool.Parse(window3);
            else checkBox_WINDOW_showTimerOnly.Checked = false;

            // checkBox_WINDOW_showBorder
            string window4 = Saves.Read("settings", "checkBox_WINDOW_showBorder");
            if (window4 != "") checkBox_WINDOW_showBorder.Checked = bool.Parse(window4);
            else checkBox_WINDOW_showBorder.Checked = true;

            // checkBox_WINDOW_topMost
            string window5 = Saves.Read("settings", "checkBox_WINDOW_topMost");
            if (window5 != "") checkBox_WINDOW_topMost.Checked = bool.Parse(window5);
            else checkBox_WINDOW_topMost.Checked = false;
        }

        // select oob control for better visuals when form is clicked
        private void Settings_Click(object sender, EventArgs e) { rrcrxo.Select(); }

        // reset to default settings
        private void button_resetToDefault_Click(object sender, EventArgs e)
        {
            // load path to variable
            string settingsPath = Path.Combine(Saves.savesPath, "settings");

            // remove settings files
            if (Directory.Exists(settingsPath)) Directory.Delete(Path.Combine(Saves.savesPath, "settings"), true);

            // restart program
            Process.Start(AppDomain.CurrentDomain.FriendlyName);
            Application.Exit();
        }

        // open gamepad console window
        private void button_gamepad_Click(object sender, EventArgs e)
        {
            // show gamepad form
            GamepadButtonSelect formGamepadButtonSelect = new GamepadButtonSelect();
            formGamepadButtonSelect.ShowDialog();
        }
    }
}

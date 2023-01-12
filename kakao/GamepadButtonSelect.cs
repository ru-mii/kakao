using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kakao
{
    public partial class GamepadButtonSelect : Form
    {
        // initializing component
        public GamepadButtonSelect()
        { InitializeComponent(); }

        // runs on form load
        private void GamepadButtonSelect_Load(object sender, EventArgs e)
        { rlqooo.Select(); } // select for visuals

        // all gamepad buttons and their index
        enum buttons
        {
            p_ltrigger = 0,
            p_rtrigger = 1,
            p_lbumper = 2,
            p_rbumper = 3,
            p_lstick = 4,
            p_rstick = 5,
            p_back = 6,
            p_start = 7,
            p_ldpad = 8,
            p_udpad = 9,
            p_rdpad = 10,
            p_ddpad = 11,
            p_x = 12,
            p_y = 13,
            p_b = 14,
            p_a = 15
        }

        // choose hotkey
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            // get hotkey textbox
            TextBox keyText = Application.OpenForms["Settings"].Controls["textBox_LEVELS_loadLevelHotkey"] as TextBox;

            // get pressed button
            Button pressed = sender as Button;

            // get pressed button values
            int value = 1000 + int.Parse(pressed.Name.Replace("button", ""));
            string name = ((buttons)value - 1000).ToString();

            // save settings
            Saves.Save("settings", "textBox_LEVELS_loadLevelHotkey", name);
            Saves.Save("settings", "textBox_LEVELS_loadLevelHotkeyValue", value.ToString());

            // update textbox showing hotkey
            keyText.Text = name;

            // reload settings
            LoadSettingsToMain();

            // close form
            Close();
        }

        // reload settings by using hidden load settings button from main form
        private void LoadSettingsToMain()
        {
            Button button_loadSettings = Application.OpenForms["Main"].Controls["button_loadSettings"] as Button;
            button_loadSettings.PerformClick();
        }
    }
}

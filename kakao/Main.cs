using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kakao
{
    public partial class Main : Form
    {
        // build version, adding new line because github adds it to their file
        // and the version is being compared with one written in github file in repo
        public static string softwareVersion = "10" + "\n";

        // initiate forms
        Settings formSettings = new Settings();

        // settings values
        static string
            button_TIMER_backColor, button_TIMER_timeAheadColor, button_TIMER_timeBehindColor,
            comboBox_TIMER_compareAgainst,

            checkBox_LEVELS_includeTheShipInBeaversForest, textBox_LEVELS_loadLevelHotkey,
            textBox_LEVELS_loadLevelHotkeyValue, checkBox_LEVELS_includePelicanDialogueSkip,
            checkBox_LEVELS_livesplitCompatibility,

            button_WINDOW_formBackColor, button_WINDOW_formFontColor, checkBox_WINDOW_showTimerOnly,
            checkBox_WINDOW_showBorder, checkBox_WINDOW_topMost,

            numericAverage, selectLevelIndex;

        // global game process
        public static Process mainGameProcess = new Process();
        public static uint moduleAddress = 0;

        // initiate main object for functions
        Toolkit toolkit = new Toolkit();

        // holds all buttons, 0 = not pressed, 1 = pressed
        public int[] gamepadTable = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        // gamepad one time click forcers
        public int[] gamepadTableReady = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        // timer variables
        public int timerStage = 0;
        public int playedLevel = 0;

        // for converting colors
        ColorConverter colorConverter = new ColorConverter();

        // best and average holders, are loaded with
        // every level change on the dropdown
        string storedBestTime = "";
        long storedBestTimeLong = 0;

        string storedAverageTime = "";
        long storedAverageTimeLong = 0;

        // used for livesplit compatibility
        string globalSuffix = "";

        //
        bool isLoaded = false;

        // initializing component, ignore
        public Main() { InitializeComponent(); }

        // selected level changed
        private void comboBox_selectLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // save index to settings
            Saves.Save("settings", "comboBox_selectLevel", comboBox_selectLevel.SelectedIndex.ToString());

            // reset timer values
            playedLevel = 0;
            timerStage = 0;
            label_timer.Text = "00:00.000";

            // read average and best to variables
            storedBestTime = GetBestTime();
            storedAverageTime = GetAverageTime();

            // get and show best/average time
            label_bestTime.Text = "best time: " + storedBestTime;
            label_averageTime.Text = "average time: " + storedAverageTime;
        }

        // runs on form load
        private void Main_Load(object sender, EventArgs e)
        {
            // disable north korean thread control, who cares
            CheckForIllegalCrossThreadCalls = false;

            // show version in the window title on the top
            // by using global variable that's used for update cheks
            Text = "kakao v" + softwareVersion;

            // load settings
            LoadSettings();

            // get and show average/best time
            label_bestTime.Text = "best time: " + GetBestTime();
            label_averageTime.Text = "average time: " + GetAverageTime();

            // select oob control for better visuals
            rrcrxo.Select();

            // check if kao is open and assign process info
            foreach (Process process in Process.GetProcessesByName("kao2"))
            {
                // if found process with kao2 name
                if (process.MainWindowTitle == "Kao - Round 2")
                {
                    // asigning process for future use
                    mainGameProcess = process;

                    // assign main module address
                    moduleAddress = (uint)process.MainModule.BaseAddress;
                }
            }

            // run background threads
            backgroundThread.RunWorkerAsync();
            pressedStateThread.RunWorkerAsync();
            buttonStateThread.RunWorkerAsync();
            checkForUpdatesThread.RunWorkerAsync();
        }

        // gets best time from selected level
        private string GetBestTime()
        {
            // get current level name
            string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();

            // file path, holds all the times
            string filePath = Path.Combine(Saves.savesPath, "times", currentLevel + "_BEST" + globalSuffix + "." + Saves.extension);

            // check if level file times exists
            if (File.Exists(filePath))
            {
                // read all times
                string[] times = File.ReadAllLines(filePath);

                // check if at least 1 time there
                if (times.Length > 0)
                {
                    // get raw best in ms
                    long bestTime = long.Parse(times[times.Length - 1]);

                    // store time in milliseconds
                    storedBestTimeLong = bestTime;

                    // convert time to visual representation
                    return TimeSpan.FromMilliseconds(bestTime).ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);
                }
            }

            // if none above got through return empty text
            return "";
        }

        // check for new updates
        private void checkForUpdatesThread_DoWork(object sender, DoWorkEventArgs e)
        { Updates.CheckForUpdates(); }

        // gets average time from selected level
        private string GetAverageTime()
        {
            // get current level name
            string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();

            // file path, holds all the times
            string filePath = Path.Combine(Saves.savesPath, "times", currentLevel + "_AVERAGE" + globalSuffix + Saves.extension);

            // total times
            int total = 0;

            // check if level file times exists
            if (File.Exists(filePath))
            {
                // read all times
                string[] times = File.ReadAllLines(filePath);
                long[] timesLong = Array.ConvertAll(times, long.Parse);

                // check if at least 1 time there
                if (times.Length > 0)
                {
                    // vars
                    long averageTime = 0;
                    int averageBy = (int)numericUpDown_averageBy.Value;

                    // check if exceeds average by
                    if (timesLong.Length > averageBy) averageTime = (long)timesLong.Skip(timesLong.Length - averageBy).ToArray().Average();
                    else if (timesLong.Length <= averageBy) averageTime = (long)timesLong.ToArray().Average();

                    // assign total
                    total = times.Length;

                    // assign total times text
                    label_from.Text = "from total: " + total;

                    // store time in milliseconds
                    storedAverageTimeLong = averageTime;

                    // convert time to visual representation
                    return TimeSpan.FromMilliseconds(averageTime).ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);
                }
            }

            // assign total times text
            label_from.Text = "from total: " + total;

            // return nothing if no times found
            return "";
        }

        private void button_removeLastBest_Click(object sender, EventArgs e)
        {
            // file path, holds all the times
            string level = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
            string filePath = Path.Combine(Saves.savesPath, "times", level + "_BEST" + globalSuffix + "." + Saves.extension);

            // check if times file exists
            if (File.Exists(filePath))
            {
                // read all times
                string[] times = File.ReadAllLines(filePath);

                // check if at least 1 time there
                if (times.Length == 1)
                {
                    File.Delete(filePath);
                    label_bestTime.Text = "best time: ";
                }
                else if (times.Length > 1)
                {
                    string[] newTimes = times.Take(times.Length - 1).ToArray();
                    File.WriteAllLines(filePath, newTimes);

                    //  
                    long bestTime = long.Parse(newTimes[newTimes.Length - 1]);
                    label_bestTime.Text = "best time: " + TimeSpan.FromMilliseconds(bestTime).ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);
                }
            }
        }

        // select oob for visuals
        private void Main_Click(object sender, EventArgs e)
        { rrcrxo.Select(); }

        // resets timer
        private void button_stopTimer_Click(object sender, EventArgs e)
        {
            playedLevel = 0;
            timerStage = 0;
            label_timer.Text = "00:00.000";
        }


        // save new value to file
        private void numericUpDown_averageBy_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            Saves.Save("settings", numeric.Name, numeric.Value.ToString());

            // recalculate average time
            storedAverageTime = GetAverageTime();
            label_averageTime.Text = "average time: " + storedAverageTime;
        }

        // checks if time we got is best time and saves if so
        private void SubmitNewTime(string level, long time)
        {
            // check if times directory exists, if not create one
            if (!Directory.Exists(Path.Combine(Saves.savesPath, "times")))
                Directory.CreateDirectory(Path.Combine(Saves.savesPath, "times"));

            // file path, holds all the times
            string filePath = Path.Combine(Saves.savesPath, "times", level + "_BEST" + globalSuffix + "." + Saves.extension);

            // convert time to visual representation
            string timeShowcase = TimeSpan.FromMilliseconds(time).ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

            // check if times file exists
            if (File.Exists(filePath))
            {
                // ########### BEST

                // read all times
                string[] timesBest = File.ReadAllLines(filePath);

                // check if at least 1 time there
                if (timesBest.Length > 0)
                {
                    // convert best time to long
                    // it will be at the end
                    long bestTime = long.Parse(timesBest[timesBest.Length - 1]);

                    // check if we got new best and save if so
                    if (time < bestTime)
                    {
                        // save new best time
                        File.AppendAllText(filePath, time.ToString() + Environment.NewLine);

                        // show new best time
                        label_bestTime.Text = "best time: " + timeShowcase;
                    }
                }
                else
                {
                    File.AppendAllText(filePath, time.ToString() + Environment.NewLine);
                    label_bestTime.Text = "best time: " + timeShowcase;
                }
            }
            else
            {
                File.AppendAllText(filePath, time.ToString() + Environment.NewLine);
                label_bestTime.Text = "best time: " + timeShowcase;
            }

            // ########### AVERAGE

            // change file path to average
            filePath = Path.Combine(Saves.savesPath, "times", level + "_AVERAGE" + globalSuffix + Saves.extension);

            // if total times exceed 100, remove oldest one
            /*if (File.Exists(filePath))
            {
                // read all times
                string[] timesAverage = File.ReadAllLines(filePath);

                // check if exceeds 100 times
                if (timesAverage.Length >= 100)
                {
                    // skipfirst time on the list
                    timesAverage = timesAverage.Skip(1).ToArray();

                    // write back with 1 time removed
                    File.WriteAllLines(filePath, timesAverage);
                }
            }*/

            // save time to array
            string[] times = new string[1];
            times[0] = time.ToString();

            // append new average to file
            File.AppendAllLines(filePath, times);

            // calculate average
            times = File.ReadAllLines(filePath);
            long[] timesLong = Array.ConvertAll(times, long.Parse);

            // vars
            long averageTime = 0;
            int averageBy = (int)numericUpDown_averageBy.Value;

            // check if exceeds average by
            if (timesLong.Length > averageBy) averageTime = (long)timesLong.Skip(timesLong.Length - averageBy).ToArray().Average();
            else if (timesLong.Length <= averageBy) averageTime = (long)timesLong.ToArray().Average();

            // assign total times text
            label_from.Text = "from total: " + timesLong.Length;

            // convert time to visual representation
            label_averageTime.Text = "average time: " + TimeSpan.FromMilliseconds(averageTime).ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

            // read average and best to variables
            storedBestTime = GetBestTime();
            storedAverageTime = GetAverageTime();
        }

        // runs in the background
        private void backgroundThread_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                long startTime = 0;
                long currentTime = 0;
                TimeSpan difference = new TimeSpan();

                uint progressAddress = 0;
                uint loadingAddress = 0;
                uint loaderAddress = 0;
                uint successAddress = 0;
                uint cutsceneAddress = 0;

                long startLoadingTime = 0;

                while (true)
                {
                    // control colors
                    if (timerStage != 0)
                    {
                        // time difference
                        double currentDifference = 1;

                        // best
                        if (comboBox_TIMER_compareAgainst == "0")
                        {
                            if (storedBestTime != "") currentDifference = (currentTime - startTime) - storedBestTimeLong;
                            else currentDifference = 0;
                        }


                        // average
                        else if (comboBox_TIMER_compareAgainst == "1" || comboBox_TIMER_compareAgainst == "")
                        {
                            if (storedAverageTime != "") currentDifference = (currentTime - startTime) - storedAverageTimeLong;
                            else currentDifference = 0;
                        }

                        // ahead
                        if (currentDifference <= 0 || label_timer.Text == "00:00.000")
                        {
                            if (button_TIMER_timeAheadColor != "") label_timer.ForeColor = (Color)colorConverter.ConvertFromString("#" + button_TIMER_timeAheadColor);
                            else label_timer.ForeColor = Color.LimeGreen; // default setting
                        }

                        // behind
                        else
                        {
                            if (button_TIMER_timeBehindColor != "") label_timer.ForeColor = (Color)colorConverter.ConvertFromString("#" + button_TIMER_timeBehindColor);
                            else label_timer.ForeColor = Color.Red; // default setting
                        }
                    }

                    // the ship
                    if (playedLevel == 1)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 0);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(1));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                cutsceneAddress = moduleAddress + 0x751680;
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            if (toolkit.ReadMemoryInt32(cutsceneAddress) == 1)
                            {
                                SetStats(0, 0);
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 4;
                            }
                        }

                        else if (timerStage == 4)
                        {
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryInt32(loaderAddress) == 2 && difference.TotalSeconds > 8)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // beavers' forest
                    else if (playedLevel == 3)
                    {
                        if (checkBox_LEVELS_includeTheShipInBeaversForest == "False" || checkBox_LEVELS_livesplitCompatibility == "True")
                        {
                            if (timerStage == 1)
                            {
                                loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;

                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(3));
                                timerStage = 2;
                            }

                            else if (timerStage == 2)
                            {
                                if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                                {
                                    SetStats(0, 0);
                                    startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                    successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;

                                    timerStage = 3;
                                }
                            }

                            else if (timerStage == 3)
                            {
                                currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                                label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                                if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                                {
                                    string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                    SubmitNewTime(currentLevel, currentTime - startTime);
                                    playedLevel = 0;
                                    timerStage = 0;
                                }
                            }
                        }
                        else // TRUE
                        {
                            if (timerStage == 1)
                            {
                                loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(1));
                                timerStage = 2;
                            }

                            else if (timerStage == 2)
                            {
                                if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                                {
                                    cutsceneAddress = moduleAddress + 0x751680;
                                    timerStage = 3;
                                }
                            }

                            else if (timerStage == 3)
                            {
                                if (toolkit.ReadMemoryInt32(cutsceneAddress) == 1)
                                {
                                    SetStats(0, 0);
                                    loadingAddress = moduleAddress + 0x73B7F4;
                                    startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                    timerStage = 4;
                                }
                            }

                            else if (timerStage == 4)
                            {
                                if (toolkit.ReadMemoryInt32(loadingAddress) == 1)
                                {
                                    startLoadingTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                    timerStage = 5;
                                }

                                successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                                currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                                label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                                if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                                {
                                    string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                    SubmitNewTime(currentLevel, currentTime - startTime);
                                    playedLevel = 0;
                                    timerStage = 0;
                                }
                            }

                            else if (timerStage == 5)
                            {
                                if (toolkit.ReadMemoryInt32(loadingAddress) == 0)
                                {
                                    startTime += DateTimeOffset.Now.ToUnixTimeMilliseconds() - startLoadingTime;
                                    timerStage = 4;
                                }
                            }
                        }
                    }

                    // the great escape
                    else if (playedLevel == 4)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 0);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(4));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // great trees
                    else if (playedLevel == 5)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 0);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(5));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // river raid
                    else if (playedLevel == 6)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 0);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(6));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // shaman's cave
                    else if (playedLevel == 7)
                    {
                        if (timerStage == 1)
                        {
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(7));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                SetStats(0, 0);
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryInt32(loaderAddress) == 2 && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // igloo village
                    else if (playedLevel == 8)
                    {
                        if (timerStage == 1)
                        {
                            isLoaded = false;
                            SetStats(0, 0);
                            loadingAddress = moduleAddress + 0x73B7F4;
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;

                            if (checkBox_LEVELS_livesplitCompatibility == "True")
                            {
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(1));
                                timerStage = 2;
                            }
                            else
                            {
                                loadingAddress = moduleAddress + 0x73B7F4;
                                loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(8));

                                timerStage = 6;
                            }
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                progressAddress = moduleAddress + 0x734CC8;
                                toolkit.WriteMemory(progressAddress, BitConverter.GetBytes(1f));
                                UnlockOneLevel(7);

                                loadingAddress = moduleAddress + 0x73B7F4;
                                loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(2));

                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                SetStats(0, 0);
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 4;
                            }
                        }

                        else if (timerStage == 4)
                        {
                            if (toolkit.ReadMemoryInt32(loadingAddress) == 1)
                            {
                                startLoadingTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 5;
                            }

                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }

                        else if (timerStage == 5)
                        {
                            if (toolkit.ReadMemoryInt32(loadingAddress) == 0)
                            {
                                startTime += DateTimeOffset.Now.ToUnixTimeMilliseconds() - startLoadingTime;
                                timerStage = 4;
                            }
                        }

                        else if (timerStage == 6)
                        {
                            if (toolkit.ReadMemoryInt32(loadingAddress) == 1)
                            {
                                timerStage = 7;
                            }

                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            if (isLoaded) label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }

                        else if (timerStage == 7)
                        {
                            if (toolkit.ReadMemoryInt32(loadingAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                isLoaded = true;
                                timerStage = 6;
                            }
                        }
                    }

                    // ice cave
                    else if (playedLevel == 9)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 0);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(9));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // down the mountain
                    else if (playedLevel == 10)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 0);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(10));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // crystal mines
                    else if (playedLevel == 11)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 0);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(11));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // the station
                    else if (playedLevel == 12)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 50);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(12));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryInt32(loaderAddress) == 2 && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // the race
                    else if (playedLevel == 13)
                    {
                        if (timerStage == 1)
                        {
                            loadingAddress = moduleAddress + 0x73B7F4;
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;

                            if (checkBox_LEVELS_livesplitCompatibility == "True")
                            {
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(1));
                                timerStage = 2;
                            }
                            else
                            {
                                loadingAddress = moduleAddress + 0x73B7F4;
                                loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(13));
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                progressAddress = moduleAddress + 0x734CC8;
                                toolkit.WriteMemory(progressAddress, BitConverter.GetBytes(2f));
                                UnlockOneLevel(12);

                                loadingAddress = moduleAddress + 0x73B7F4;
                                loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(2));

                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                SetStats(0, 50);
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 4;
                            }
                        }

                        else if (timerStage == 4)
                        {
                            if (toolkit.ReadMemoryInt32(loadingAddress) == 1)
                            {
                                startLoadingTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 5;
                            }

                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x29C;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }

                        else if (timerStage == 5)
                        {
                            if (toolkit.ReadMemoryInt32(loadingAddress) == 0)
                            {
                                startTime += DateTimeOffset.Now.ToUnixTimeMilliseconds() - startLoadingTime;
                                timerStage = 4;
                            }
                        }
                    }

                    // hostile reef
                    else if (playedLevel == 14)
                    {
                        if (timerStage == 1)
                        {
                            loadingAddress = moduleAddress + 0x73B7F4;
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;

                            if (checkBox_LEVELS_livesplitCompatibility == "True")
                            {
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(1));
                                timerStage = 2;
                            }
                            else
                            {
                                loadingAddress = moduleAddress + 0x73B7F4;
                                loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(14));
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                progressAddress = moduleAddress + 0x734CC8;
                                toolkit.WriteMemory(progressAddress, BitConverter.GetBytes(5f));
                                UnlockOneLevel(13);

                                loadingAddress = moduleAddress + 0x73B7F4;
                                loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(2));

                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                SetStats(0, 50);
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 4;
                            }
                        }

                        else if (timerStage == 4)
                        {
                            if (toolkit.ReadMemoryInt32(loadingAddress) == 1)
                            {
                                startLoadingTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 5;
                            }

                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }

                        else if (timerStage == 5)
                        {
                            if (toolkit.ReadMemoryInt32(loadingAddress) == 0)
                            {
                                startTime += DateTimeOffset.Now.ToUnixTimeMilliseconds() - startLoadingTime;
                                timerStage = 4;
                            }
                        }
                    }

                    // deep ocean
                    else if (playedLevel == 15)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 50);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(15));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // lair of poison
                    else if (playedLevel == 16)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 50);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(16));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryInt32(loaderAddress) == 2 && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // trip to island
                    else if (playedLevel == 17)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 50);

                            loadingAddress = moduleAddress + 0x73B7F4;
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;

                            if (checkBox_LEVELS_livesplitCompatibility == "True")
                            {
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(1));
                                timerStage = 2;
                            }
                            else
                            {
                                loadingAddress = moduleAddress + 0x73B7F4;
                                loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(17));
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                progressAddress = moduleAddress + 0x734CC8;
                                toolkit.WriteMemory(progressAddress, BitConverter.GetBytes(3f));
                                UnlockOneLevel(16);

                                loadingAddress = moduleAddress + 0x73B7F4;
                                loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(2));

                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            SetStats(0, 50);
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 4;
                            }
                        }

                        else if (timerStage == 4)
                        {
                            if (toolkit.ReadMemoryInt32(loadingAddress) == 1)
                            {
                                startLoadingTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 5;
                            }

                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }

                        else if (timerStage == 5)
                        {
                            if (toolkit.ReadMemoryInt32(loadingAddress) == 0)
                            {
                                startTime += DateTimeOffset.Now.ToUnixTimeMilliseconds() - startLoadingTime;
                                timerStage = 4;
                            }
                        }
                    }

                    // treasure island
                    else if (playedLevel == 18)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 50);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(18));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // the volcano
                    else if (playedLevel == 19)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 50);
                            progressAddress = moduleAddress + 0x734CC8;
                            UnlockOneLevel(1);
                            toolkit.WriteMemory(progressAddress, BitConverter.GetBytes(1f));
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(19));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryInt32(loaderAddress) == 2 && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // abandoned town
                    else if (playedLevel == 21)
                    {
                        if (timerStage == 1)
                        {
                            loadingAddress = moduleAddress + 0x73B7F4;
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;

                            if (checkBox_LEVELS_livesplitCompatibility == "True")
                            {
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(1));
                                timerStage = 2;
                            }
                            else
                            {
                                loadingAddress = moduleAddress + 0x73B7F4;
                                loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(21));
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                progressAddress = moduleAddress + 0x734CC8;

                                if (checkBox_LEVELS_includePelicanDialogueSkip == "True" || checkBox_LEVELS_includePelicanDialogueSkip == "") // default setting
                                    toolkit.WriteMemory(progressAddress, BitConverter.GetBytes(3f));

                                else toolkit.WriteMemory(progressAddress, BitConverter.GetBytes(-3f));


                                UnlockOneLevel(20);

                                loadingAddress = moduleAddress + 0x73B7F4;
                                loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                                toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(2));

                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            SetStats(3000, 50);
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 4;
                            }
                        }

                        else if (timerStage == 4)
                        {
                            if (toolkit.ReadMemoryInt32(loadingAddress) == 1)
                            {
                                startLoadingTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 5;
                            }

                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }

                        else if (timerStage == 5)
                        {
                            if (toolkit.ReadMemoryInt32(loadingAddress) == 0)
                            {
                                startTime += DateTimeOffset.Now.ToUnixTimeMilliseconds() - startLoadingTime;
                                timerStage = 4;
                            }
                        }
                    }

                    // hunter's galleon
                    else if (playedLevel == 22)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 50);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(22));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        else if (timerStage == 3)
                        {
                            successAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734DF8) + 0x1A0;
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);

                            if (toolkit.ReadMemoryFloat(successAddress) == 40f && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    // final duel
                    else if (playedLevel == 23)
                    {
                        if (timerStage == 1)
                        {
                            SetStats(0, 50);
                            loaderAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;
                            toolkit.WriteMemory(loaderAddress, BitConverter.GetBytes(23));
                            timerStage = 2;
                        }

                        else if (timerStage == 2)
                        {
                            if (toolkit.ReadMemoryInt32(loaderAddress) == 0)
                            {
                                cutsceneAddress = moduleAddress + 0x751680;
                                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                                timerStage = 3;
                            }
                        }

                        if (timerStage > 2)
                        {
                            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                            difference = TimeSpan.FromMilliseconds(currentTime - startTime);
                            label_timer.Text = difference.ToString("mm':'ss'.'fff", CultureInfo.InvariantCulture);
                        }

                        if (timerStage == 3)
                        {
                            if (toolkit.ReadMemoryInt32(cutsceneAddress) == 1)
                                timerStage = 4;
                        }

                        if (timerStage == 4)
                        {
                            if (toolkit.ReadMemoryInt32(cutsceneAddress) == 0)
                                timerStage = 5;
                        }

                        if (timerStage == 5)
                        {
                            if (toolkit.ReadMemoryInt32(cutsceneAddress) == 1 && difference.TotalSeconds > 10)
                            {
                                string currentLevel = comboBox_selectLevel.GetItemText(comboBox_selectLevel.SelectedItem).ToLower();
                                SubmitNewTime(currentLevel, currentTime - startTime);
                                playedLevel = 0;
                                timerStage = 0;
                            }
                        }
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception ex) { Saves.Save("logs", "error_log.txt", ex.ToString()); }

        }

        // check for hotkey presses
        private void pressedStateThread_DoWork(object sender, DoWorkEventArgs e)
        {
            // infinite loop
            while (true)
            {
                // make sure hotkey is set and settings form is not open
                if (textBox_LEVELS_loadLevelHotkeyValue != "" && !formSettings.Visible)
                {
                    // parse hotkey value string
                    int hotkeyValue = int.Parse(textBox_LEVELS_loadLevelHotkeyValue);

                    // check if saved hotkey is gamepad or keyboard
                    if (hotkeyValue < 1000)
                    {
                        // start timer if key pressed
                        if (toolkit.IsKeyPressed(hotkeyValue))
                            StartTimerWithLevel();
                    }

                    // check gamepad press
                    else if (gamepadTable.Sum() > 0)
                    {
                        // load level
                        if (gamepadTable[hotkeyValue - 1000] != 0 && gamepadTableReady[hotkeyValue - 1000] == 0)
                            StartTimerWithLevel();
                    }
                }

                // sleep for 1 ms
                Thread.Sleep(1);
            }
        }

        // reads currently pressed keys on a gamepad
        private void buttonStateThread_DoWork(object sender, DoWorkEventArgs e)
        {
            // gamepad object
            X.Gamepad gamepad = X.Gamepad_1;

            // infinite loop
            while (true)
            {
                // syncs with actions on the gamepad
                // for all signs check https://github.com/ru-mii/kangur/blob/main/gamepadSigns.png?raw=true
                if (gamepad.Update())
                {
                    // 0
                    if (gamepad.LTrigger_N != 0) gamepadTable[0] = 1;
                    else { gamepadTable[0] = 0; gamepadTableReady[0] = 0; }

                    // 1
                    if (gamepad.RTrigger_N != 0) gamepadTable[1] = 1;
                    else { gamepadTable[1] = 0; gamepadTableReady[1] = 0; }

                    // 2
                    if (gamepad.LBumper_down) gamepadTable[2] = 1;
                    else { gamepadTable[2] = 0; gamepadTableReady[2] = 0; }

                    // 3
                    if (gamepad.RBumper_down) gamepadTable[3] = 1;
                    else { gamepadTable[3] = 0; gamepadTableReady[3] = 0; }

                    // 4
                    if (gamepad.LStick_down) gamepadTable[4] = 1;
                    else { gamepadTable[4] = 0; gamepadTableReady[4] = 0; }

                    // 5
                    if (gamepad.RStick_down) gamepadTable[5] = 1;
                    else { gamepadTable[5] = 0; gamepadTableReady[5] = 0; }

                    // 6
                    if (gamepad.Back_down) gamepadTable[6] = 1;
                    else { gamepadTable[6] = 0; gamepadTableReady[6] = 0; }

                    // 7
                    if (gamepad.Start_down) gamepadTable[7] = 1;
                    else { gamepadTable[7] = 0; gamepadTableReady[7] = 0; }

                    // 8
                    if (gamepad.Dpad_Left_down) gamepadTable[8] = 1;
                    else { gamepadTable[8] = 0; gamepadTableReady[8] = 0; }

                    // 9
                    if (gamepad.Dpad_Up_down) gamepadTable[9] = 1;
                    else { gamepadTable[9] = 0; gamepadTableReady[9] = 0; }

                    // 10
                    if (gamepad.Dpad_Right_down) gamepadTable[10] = 1;
                    else { gamepadTable[10] = 0; gamepadTableReady[10] = 0; }

                    // 11
                    if (gamepad.Dpad_Down_down) gamepadTable[11] = 1;
                    else { gamepadTable[11] = 0; gamepadTableReady[11] = 0; }

                    // 12
                    if (gamepad.X_down) gamepadTable[12] = 1;
                    else { gamepadTable[12] = 0; gamepadTableReady[12] = 0; }

                    // 13
                    if (gamepad.Y_down) gamepadTable[13] = 1;
                    else { gamepadTable[13] = 0; gamepadTableReady[13] = 0; }

                    // 14
                    if (gamepad.B_down) gamepadTable[14] = 1;
                    else { gamepadTable[14] = 0; gamepadTableReady[14] = 0; }

                    // 15
                    if (gamepad.A_down) gamepadTable[15] = 1;
                    else { gamepadTable[15] = 0; gamepadTableReady[15] = 0; }
                }

                // sleep for performance
                Thread.Sleep(1);
            }
        }

        // loads level from combobox
        private void StartTimerWithLevel()
        {
            // select oob control for better visuals
            rrcrxo.Select();

            // check if kao is open
            foreach (Process process in Process.GetProcessesByName("kao2"))
            {
                // if found process with kao2 name
                if (process.MainWindowTitle == "Kao - Round 2")
                {
                    // asigning process for future use
                    mainGameProcess = process;

                    // assign main module address
                    moduleAddress = (uint)process.MainModule.BaseAddress;

                    // get required pointer
                    uint levelLoadAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x73D868) + 0x3754;

                    // make sure we got correct level address right
                    if (levelLoadAddress != 0)
                    {
                        // gets currently loading level
                        uint currentlyLoading = toolkit.ReadMemoryInt32(levelLoadAddress);

                        // make sure level is not loading at the moment
                        // make sure we have correct index
                        if (currentlyLoading == 0 && comboBox_selectLevel.SelectedIndex >= 0)
                        {
                            // disable saves (stars, gold, progress)
                            byte[] instruction = { 0x90, 0x90, 0x90 };
                            toolkit.WriteMemory(moduleAddress + 0xB4FF3, instruction);

                            // adjust selected level index
                            playedLevel = comboBox_selectLevel.SelectedIndex;
                            if (comboBox_selectLevel.SelectedIndex == 0) playedLevel = 1;
                            else if (comboBox_selectLevel.SelectedIndex > 17) playedLevel += 3;
                            else playedLevel += 2;

                            // set stage to first stage
                            timerStage = 1;

                            // reset timer visually
                            label_timer.Text = "00:00.000";

                            // assign ahead color at first
                            if (button_TIMER_timeAheadColor != "") label_timer.ForeColor = (Color)colorConverter.ConvertFromString("#" + button_TIMER_timeAheadColor);
                            else label_timer.ForeColor = Color.LimeGreen; // default setting
                        }
                    }
                }
            }
        }

        // locks all the leves and unlocks one we want
        private void UnlockOneLevel(int level)
        {
            // list of all levels
            uint levelsTableAddress = toolkit.ReadMemoryInt32(moduleAddress + 0x734CA0);

            // go through each level
            for (uint i = 0; i < 23; i++)
            {
                // if it's unlock level change lock value
                byte value = 0; if (i == level) value = 1;

                // write now lock value
                uint unlockAddress = toolkit.ReadMemoryInt32(levelsTableAddress + (i * 4)) + 0x20;
                toolkit.WriteMemory(unlockAddress, BitConverter.GetBytes(value));
            }
        }

        // stats setter, used before level load
        private void SetStats(int gold, int stars)
        {
            toolkit.WriteMemory(moduleAddress + 0x734DC8, BitConverter.GetBytes(gold));
            toolkit.WriteMemory(moduleAddress + 0x734DD0, BitConverter.GetBytes(stars));
        }

        // open settings form
        private void OpenSettings(object sender, EventArgs e)
        {
            formSettings = new Settings();
            formSettings.ShowDialog();
        }

        // load settings button wrapper
        private void button_loadSettings_Click(object sender, EventArgs e) { LoadSettings(); }

        // loads saved settings
        private void LoadSettings()
        {
            // reset timer
            playedLevel = 0;
            timerStage = 0;
            label_timer.Text = "00:00.000";

            // load data values
            button_TIMER_backColor = Saves.Read("settings", "button_TIMER_backColor");
            button_TIMER_timeAheadColor = Saves.Read("settings", "button_TIMER_timeAheadColor");
            button_TIMER_timeBehindColor = Saves.Read("settings", "button_TIMER_timeBehindColor");
            comboBox_TIMER_compareAgainst = Saves.Read("settings", "comboBox_TIMER_compareAgainst");
            checkBox_LEVELS_includeTheShipInBeaversForest = Saves.Read("settings", "checkBox_LEVELS_includeTheShipInBeaversForest");
            textBox_LEVELS_loadLevelHotkey = Saves.Read("settings", "textBox_LEVELS_loadLevelHotkey");
            textBox_LEVELS_loadLevelHotkeyValue = Saves.Read("settings", "textBox_LEVELS_loadLevelHotkeyValue");
            checkBox_LEVELS_includePelicanDialogueSkip = Saves.Read("settings", "checkBox_LEVELS_includePelicanDialogueSkip");
            checkBox_LEVELS_livesplitCompatibility = Saves.Read("settings", "checkBox_LEVELS_livesplitCompatibility");
            button_WINDOW_formBackColor = Saves.Read("settings", "button_WINDOW_formBackColor");
            button_WINDOW_formFontColor = Saves.Read("settings", "button_WINDOW_formFontColor");
            checkBox_WINDOW_showTimerOnly = Saves.Read("settings", "checkBox_WINDOW_showTimerOnly");
            checkBox_WINDOW_showBorder = Saves.Read("settings", "checkBox_WINDOW_showBorder");
            checkBox_WINDOW_topMost = Saves.Read("settings", "checkBox_WINDOW_topMost");
            numericAverage = Saves.Read("settings", "numericUpDown_averageBy");
            selectLevelIndex = Saves.Read("settings", "comboBox_selectLevel");

            // implement read settings
            if (button_TIMER_backColor != "") label_timer.BackColor = (Color)colorConverter.ConvertFromString("#" + button_TIMER_backColor);
            if (button_TIMER_timeAheadColor != "") label_timer.ForeColor = (Color)colorConverter.ConvertFromString("#" + button_TIMER_timeAheadColor);
            if (button_WINDOW_formBackColor != "") BackColor = (Color)colorConverter.ConvertFromString("#" + button_WINDOW_formBackColor);
            if (button_WINDOW_formFontColor != "") label_bestTime.ForeColor = (Color)colorConverter.ConvertFromString("#" + button_WINDOW_formFontColor);

            if (checkBox_WINDOW_showTimerOnly == "True")
            {
                FormBorderStyle = FormBorderStyle.None;
                label_timer.Location = new Point(0, 0);
                Size = new Size(385, 116);
            }
            else if (checkBox_WINDOW_showTimerOnly == "False")
            {
                FormBorderStyle = FormBorderStyle.FixedSingle;
                label_timer.Location = new Point(12, 9);
                Size = new Size(432, 256);
            }

            if (checkBox_WINDOW_showBorder == "True") FormBorderStyle = FormBorderStyle.FixedSingle;
            else if (checkBox_WINDOW_showBorder == "False") FormBorderStyle = FormBorderStyle.None;

            if (checkBox_WINDOW_topMost == "True") TopMost = true;
            else if (checkBox_WINDOW_topMost == "False") TopMost = false;

            if (numericAverage != "") numericUpDown_averageBy.Value = int.Parse(numericAverage);

            if (selectLevelIndex != "") comboBox_selectLevel.SelectedIndex = int.Parse(selectLevelIndex);
            else comboBox_selectLevel.SelectedIndex = 0;

            if (checkBox_LEVELS_livesplitCompatibility == "") checkBox_LEVELS_livesplitCompatibility = "True";
            if (checkBox_LEVELS_livesplitCompatibility == "True") globalSuffix = "";
            else globalSuffix = "_NOCOM";

            // reload times
            storedBestTime = GetBestTime();
            storedAverageTime = GetAverageTime();

            // get and show best/average time
            label_bestTime.Text = "best time: " + storedBestTime;
            label_averageTime.Text = "average time: " + storedAverageTime;
        }
    }
}
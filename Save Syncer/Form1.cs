using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Save_Syncer
{
    public partial class Form1 : Form
    {
        public List<SyncObject> syncObjects = new List<SyncObject>();

        public bool fillingBoxes = false, manualEntry = false, helpOpen = false;

        public List<string> outputLines = new List<string>();

        public string appdata = "", userprofile = "";

        public Form1()
        {
            InitializeComponent();
            appdata = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.None);
            userprofile = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile, Environment.SpecialFolderOption.None);
            domainUpDown1.Text = "Add an item";
            LoadObjects();
            DisplayObjects();
            numericUpDownTimer.Value = (decimal)Properties.Settings.Default.TimerInterval / 1000m;
            numericUpDownWrite.Value = (decimal)Properties.Settings.Default.MinimumWriteDifference;
            numericUpDownOutput.Value = (decimal)Properties.Settings.Default.OutputLines;
            timerSyncTick.Interval = (int)(numericUpDownTimer.Value * 1000m);
            timerSyncTick.Enabled = true;
            timerSyncTick.Start();
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            AddNewSyncObject();
        }

        public void SaveObjects()
        {
            Properties.Settings.Default.FolderAList.Clear();
            Properties.Settings.Default.FolderBList.Clear();
            Properties.Settings.Default.FileList.Clear();
            for (int i = 0; i < syncObjects.Count; i++)
            {
                Properties.Settings.Default.FolderAList.Add(syncObjects[i].folderA);
                Properties.Settings.Default.FolderBList.Add(syncObjects[i].folderB);
                Properties.Settings.Default.FileList.Add(syncObjects[i].fileName);
            }
            Properties.Settings.Default.Save();
        }

        public void LoadObjects()
        {
            for (int i = 0; i < Properties.Settings.Default.FileList.Count; i++)
            {
                SyncObject syncObject = new SyncObject();
                syncObject.folderA = Properties.Settings.Default.FolderAList[i];
                syncObject.folderB = Properties.Settings.Default.FolderBList[i];
                syncObject.fileName = Properties.Settings.Default.FileList[i];
                syncObjects.Add(syncObject);
            }
        }

        public void DisplayObjects(bool showLast = false)
        {
            string selectedItem = "";
            int newIndex = -1;
            if (domainUpDown1.SelectedItem != null)
                selectedItem = domainUpDown1.SelectedItem.ToString();
            domainUpDown1.Items.Clear();
            for (int i = 0; i < syncObjects.Count; i++)
            {
                if (selectedItem.Length > 0 && selectedItem == syncObjects[i].fileName)
                    newIndex = i;
                domainUpDown1.Items.Add(syncObjects[i].fileName);
            }
            if (newIndex == -1 && domainUpDown1.Items.Count > 0) newIndex = 0;
            if (showLast && domainUpDown1.Items.Count > 0) newIndex = domainUpDown1.Items.Count - 1;
            if (newIndex != -1) domainUpDown1.SelectedIndex = newIndex;
            else
            {
                domainUpDown1.Text = "Add an item";
                textBoxFile.Text = "";
                textBoxFolderA.Text = "";
                textBoxFolderB.Text = "";
            }
        }

        public void FillBoxes()
        {
            fillingBoxes = true;
            if (domainUpDown1.SelectedItem != null)
            {
                int index = domainUpDown1.SelectedIndex;
                textBoxFile.ReadOnly = true;
                textBoxFolderA.ReadOnly = true;
                textBoxFolderB.ReadOnly = true;
                timer1.Enabled = true;
                timer1.Start();
                textBoxFolderA.Text = Properties.Settings.Default.FolderAList[index];
                textBoxFolderB.Text = Properties.Settings.Default.FolderBList[index];
                textBoxFile.Text = Properties.Settings.Default.FileList[index];
            }
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            if (!manualEntry) FillBoxes();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBoxFile.ReadOnly = false;
            textBoxFolderA.ReadOnly = false;
            textBoxFolderB.ReadOnly = false;
            fillingBoxes = false;
            manualEntry = false;
            timer1.Stop();
            timer1.Enabled = false;
        }

        public void UpdateObject(bool manual = false)
        {
            if (domainUpDown1.SelectedItem != null)
            {
                if (manual) manualEntry = true;
                int index = domainUpDown1.SelectedIndex;
                Properties.Settings.Default.FolderAList[index] = textBoxFolderA.Text;
                Properties.Settings.Default.FolderBList[index] = textBoxFolderB.Text;
                Properties.Settings.Default.FileList[index] = textBoxFile.Text;
                syncObjects[index].folderA = textBoxFolderA.Text;
                syncObjects[index].folderB = textBoxFolderB.Text;
                syncObjects[index].fileName = textBoxFile.Text;
                domainUpDown1.Items[index] = textBoxFile.Text;
                Properties.Settings.Default.Save();
                timer1.Enabled = true;
                timer1.Start();
            }
        }

        public void Output(string outputString)
        {
            outputLines.Add($"{DateTime.Now.ToLongTimeString()} : {outputString}");
            while (outputLines.Count > (int)numericUpDownOutput.Value)
                outputLines.RemoveAt(0);
            StringBuilder builder = new StringBuilder();
            for (int x = outputLines.Count - 1; x >= 0; x--)
                builder.AppendLine(outputLines[x]);
            richTextBox1.Text = builder.ToString();
        }

        private void textBoxFile_TextChanged(object sender, EventArgs e)
        {
            if (!fillingBoxes) UpdateObject(true);
        }

        private void textBoxFolderB_TextChanged(object sender, EventArgs e)
        {
            if (!fillingBoxes) UpdateObject(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (domainUpDown1.SelectedItem != null)
            {
                int index = domainUpDown1.SelectedIndex;
                if (domainUpDown1.Items.Count > 1) domainUpDown1.SelectedIndex = 0;
                Properties.Settings.Default.FolderAList.RemoveAt(index);
                Properties.Settings.Default.FolderBList.RemoveAt(index);
                Properties.Settings.Default.FileList.RemoveAt(index);
                domainUpDown1.Items.RemoveAt(index);
                syncObjects.RemoveAt(index);
                SaveObjects();
                DisplayObjects();
            }
        }

        private void timerSyncTick_Tick(object sender, EventArgs e)
        {
            if (syncState == null) syncState = SyncState();

            bool moreWork = syncState.MoveNext();

            labelActivityNotice.Text = labelActivityNotice.Text.Replace('|', 'a').Replace('!', 'b').Replace('a', '!').Replace('b', '|');

            if (!moreWork)
            {
                try
                {
                    syncState.Dispose();
                    syncState = null;
                }
                catch { }
            }
        }

        public IEnumerator<bool> syncState;

        public IEnumerator<bool> SyncState()
        {
            while (true)
            {
                bool yieldRequired = true;
                for (int i = 0; i < syncObjects.Count; i++)
                {
                    if (i > 0)
                    {
                        yield return true;
                        yieldRequired = false;
                    }
                    try
                    {
                        bool folderAExists = Directory.Exists(syncObjects[i].folderA), folderBExists = Directory.Exists(syncObjects[i].folderB);
                        if (folderAExists && folderBExists)
                        {
                            bool aExists = File.Exists(syncObjects[i].FileA()), bExists = File.Exists(syncObjects[i].FileB());
                            if (aExists && bExists) Output($"{syncObjects[i].fileName} found in both locations");
                            else if (aExists || bExists) Output($"{syncObjects[i].fileName} found in one location");
                            else Output($"{syncObjects[i].fileName} not found");
                            if (aExists || bExists)
                            {
                                if (aExists && bExists)
                                {
                                    DateTime aModification = File.GetLastWriteTimeUtc(syncObjects[i].FileA()),
                                                bModification = File.GetLastWriteTimeUtc(syncObjects[i].FileB());
                                    TimeSpan difference = Difference(aModification, bModification);
                                    if (difference.TotalMilliseconds == 0.0) Output("Both files are synced");
                                    else
                                    {
                                        if (aModification > bModification) Output($"File in folder '{syncObjects[i].folderA}' is {difference.TotalSeconds} newer");
                                        else Output($"File in folder '{syncObjects[i].folderB}' is {difference.TotalSeconds} newer");
                                    }
                                    if (difference.TotalSeconds >= (double)numericUpDownWrite.Value)
                                    {
                                        if (aModification > bModification)
                                            Sync(syncObjects[i].FileA(), syncObjects[i].FileB());
                                        else
                                            Sync(syncObjects[i].FileB(), syncObjects[i].FileA());
                                    }
                                }
                                else
                                {
                                    Output("Syncing only file to other location");
                                    if (aExists) Sync(syncObjects[i].FileA(), syncObjects[i].FileB());
                                    else Sync(syncObjects[i].FileB(), syncObjects[i].FileA());
                                }
                            }
                        }
                        else
                        {
                            if (!folderAExists)
                            {
                                if (syncObjects[i].folderA.Length == 0) Output($"Please choose Folder A for object {syncObjects[i].fileName}");
                                else Output($"Folder {syncObjects[i].folderA} for object {syncObjects[i].fileName} doesn't exist");
                            }
                            if (!folderBExists)
                            {
                                if (syncObjects[i].folderB.Length == 0) Output($"Please choose Folder B for object {syncObjects[i].fileName}");
                                else Output($"Folder {syncObjects[i].folderB} for object {syncObjects[i].fileName} doesn't exist");
                            }
                        }
                    }
                    catch { }
                }
                if (yieldRequired) yield return true;
            }
        }

        public void Sync(string fileOrigin, string fileDestination)
        {
            try
            {
                if (File.Exists(fileDestination)) File.Delete(fileDestination);
                File.Copy(fileOrigin, fileDestination);
                if (File.Exists(fileDestination)) Output("Sync successful");
                if (File.GetLastWriteTimeUtc(fileDestination) != File.GetLastWriteTimeUtc(fileOrigin))
                {
                    File.SetLastWriteTimeUtc(fileDestination, File.GetLastWriteTimeUtc(fileOrigin));
                    Output("Adjusting last write time to match original file used to sync");
                }
            }
            catch { }
        }

        public TimeSpan Difference(DateTime a, DateTime b)
        {
            if (a > b) return a - b;
            return b - a;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int interval = (int)(numericUpDownTimer.Value * 1000m);
            Properties.Settings.Default.TimerInterval = interval;
            Properties.Settings.Default.Save();
            timerSyncTick.Stop();
            timerSyncTick.Enabled = false;
            timerSyncTick.Interval = interval;
            timerSyncTick.Enabled = true;
            timerSyncTick.Start();
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.OutputLines = (int)numericUpDownOutput.Value;
            Properties.Settings.Default.Save();
        }

        private void numericUpDownWrite_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MinimumWriteDifference = (double)numericUpDownWrite.Value;
            Properties.Settings.Default.Save();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            this.Hide();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!helpOpen)
            {
                helpOpen = true;
                HelpForm form = new HelpForm();
                form.FormClosed += HelpClosed;
                form.Show();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (syncObjects.Count == 0) AddNewSyncObject();
            if (domainUpDown1.SelectedItem != null)
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        fillingBoxes = true;
                        textBoxFolderA.Text = fbd.SelectedPath;
                        UpdateObject(true);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (syncObjects.Count == 0) AddNewSyncObject();
            if (domainUpDown1.SelectedItem != null)
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        fillingBoxes = true;
                        textBoxFolderB.Text = fbd.SelectedPath;
                        UpdateObject(true);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (syncObjects.Count == 0) AddNewSyncObject();
            if (domainUpDown1.SelectedItem != null)
            {
                using (var fbd = new OpenFileDialog())
                {
                    DialogResult result = fbd.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        fillingBoxes = true;
                        int index = domainUpDown1.SelectedIndex;
                        textBoxFile.Text = Path.GetFileName(fbd.FileName);
                        string folder = fbd.FileName.Substring(0, fbd.FileName.LastIndexOf(@"\"));
                        if (syncObjects[index].folderA.Length == 0 || syncObjects[index].folderB.Length == 0)
                        {
                            if (syncObjects[index].folderA.Length == 0 && (syncObjects[index].folderB.Length == 0 || syncObjects[index].folderB != folder))
                                textBoxFolderA.Text = folder;
                            else if (syncObjects[index].folderB.Length == 0 && (syncObjects[index].folderA.Length == 0 || syncObjects[index].folderA != folder))
                                textBoxFolderB.Text = folder;
                        }
                        UpdateObject(true);
                    }
                }
            }
        }

        public void AddNewSyncObject()
        {
            syncObjects.Add(new SyncObject());
            SaveObjects();
            DisplayObjects(true);
        }

        private void appdataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (syncObjects.Count == 0) AddNewSyncObject();
            if (domainUpDown1.SelectedItem != null)
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    fbd.RootFolder = Environment.SpecialFolder.ApplicationData;
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        fillingBoxes = true;
                        if (syncObjects[domainUpDown1.SelectedIndex].folderA.Length == 0 && (syncObjects[domainUpDown1.SelectedIndex].folderB.Length == 0 || syncObjects[domainUpDown1.SelectedIndex].folderB != fbd.SelectedPath))
                            textBoxFolderA.Text = fbd.SelectedPath;
                        if (syncObjects[domainUpDown1.SelectedIndex].folderB.Length == 0 && (syncObjects[domainUpDown1.SelectedIndex].folderA.Length == 0 || syncObjects[domainUpDown1.SelectedIndex].folderA != fbd.SelectedPath))
                            textBoxFolderB.Text = fbd.SelectedPath;
                        UpdateObject(true);
                    }
                }
            }
        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (syncObjects.Count == 0) AddNewSyncObject();
            if (domainUpDown1.SelectedItem != null)
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    fbd.RootFolder = Environment.SpecialFolder.UserProfile;
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        fillingBoxes = true;
                        if (syncObjects[domainUpDown1.SelectedIndex].folderA.Length == 0 && (syncObjects[domainUpDown1.SelectedIndex].folderB.Length == 0 || syncObjects[domainUpDown1.SelectedIndex].folderB != fbd.SelectedPath))
                            textBoxFolderA.Text = fbd.SelectedPath;
                        if (syncObjects[domainUpDown1.SelectedIndex].folderB.Length == 0 && (syncObjects[domainUpDown1.SelectedIndex].folderA.Length == 0 || syncObjects[domainUpDown1.SelectedIndex].folderA != fbd.SelectedPath))
                            textBoxFolderB.Text = fbd.SelectedPath;
                        UpdateObject(true);
                    }
                }
            }
        }

        private void factorioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folder = appdata + @"\Factorio\saves";
            if (Directory.Exists(folder))
            {
                fillingBoxes = true;
                if (syncObjects.Count == 0) AddNewSyncObject();
                if (syncObjects[domainUpDown1.SelectedIndex].folderA.Length == 0 || syncObjects[domainUpDown1.SelectedIndex].folderB.Length > 0)
                    textBoxFolderA.Text = folder;
                else textBoxFolderB.Text = folder;
                UpdateObject(true);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer2.Start();
        }

        int mainIndex = 0, subIndex = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (syncObjects.Count == 0)
            {
                if (mainIndex != 0)
                {
                    subIndex = 0;
                    ResetButtons();
                }
                mainIndex = 0;
                richTextBox2.Text = "You don't have any sync objects set. Press 'Select File' and choose a file to sync";
                switch (subIndex)
                {
                    case 0:
                        button5.BackColor = Color.Red;
                        subIndex = 1;
                        break;
                    case 1:
                        button5.BackColor = Color.Green;
                        subIndex = 0;
                        break;
                }
            }
            else if (syncObjects[0].folderA.Length == 0)
            {
                if (mainIndex != 1)
                {
                    subIndex = 0;
                    ResetButtons();
                }
                mainIndex = 1;
                richTextBox2.Text = "Select Folder A";
                switch (subIndex)
                {
                    case 0:
                        button3.BackColor = Color.Red;
                        subIndex = 1;
                        break;
                    case 1:
                        button3.BackColor = Color.Green;
                        subIndex = 0;
                        break;
                }
            }
            else if (syncObjects[0].folderB.Length == 0)
            {
                if (mainIndex != 2)
                {
                    subIndex = 0;
                    ResetButtons();
                }
                mainIndex = 2;
                richTextBox2.Text = "Select Folder B";
                switch (subIndex)
                {
                    case 0:
                        button4.BackColor = Color.Red;
                        subIndex = 1;
                        break;
                    case 1:
                        button4.BackColor = Color.Green;
                        subIndex = 0;
                        break;
                }
            }
            else if (syncObjects[0].fileName.Length > 0 && !File.Exists(syncObjects[0].FileA()) && !File.Exists(syncObjects[0].FileB()))
            {
                if (mainIndex != 3)
                {
                    subIndex = 0;
                    ResetButtons();
                }
                mainIndex = 3;
                richTextBox2.Text = "Select a file or enter the full name including the extension excluding the path, e.g. 'Notes.txt'";
                switch (subIndex)
                {
                    case 0:
                        button5.BackColor = Color.Red;
                        textBoxFile.BackColor = Color.FromArgb(240, 240, 240);
                        subIndex = 1;
                        break;
                    case 1:
                        button5.BackColor = Color.Green;
                        textBoxFile.BackColor = Color.FromArgb(200, 240, 240);
                        subIndex = 0;
                        break;
                }
            }
            else
            {
                richTextBox2.Text = "";
                ResetButtons();
            }
        }

        public void ResetButtons()
        {
            button5.BackColor = button2.BackColor;
            button3.BackColor = button2.BackColor;
            button4.BackColor = button2.BackColor;
            textBoxFile.BackColor = textBoxFolderA.BackColor;
        }

        private void spaceEngineersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folder = appdata + @"\SpaceEngineers\Saves";
            if (Directory.Exists(folder))
            {
                fillingBoxes = true;
                if (syncObjects.Count == 0) AddNewSyncObject();
                if (syncObjects[domainUpDown1.SelectedIndex].folderA.Length == 0 || syncObjects[domainUpDown1.SelectedIndex].folderB.Length > 0)
                    textBoxFolderA.Text = folder;
                else textBoxFolderB.Text = folder;
                UpdateObject(true);
            }
        }

        private void HelpClosed(object sender, EventArgs e)
        {
            helpOpen = false;
        }

        private void textBoxFolderA_TextChanged(object sender, EventArgs e)
        {
            if (!fillingBoxes)
            {
                manualEntry = true;
                UpdateObject(true);
            }
        }
    }

    public class SyncObject
    {
        public string folderA = "", folderB = "", fileName = "Temp";

        public string FileA()
        {
            return folderA + @"\" + fileName;
        }
        public string FileB()
        {
            return folderB + @"\" + fileName;
        }
    }
}

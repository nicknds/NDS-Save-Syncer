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

        public Form1()
        {
            InitializeComponent();
            domainUpDown1.Text = "Add an item";
            LoadObjects();
            DisplayObjects();
            numericUpDownTimer.Value = (decimal)Properties.Settings.Default.TimerInterval / 1000m;
            numericUpDownWrite.Value = (decimal)Properties.Settings.Default.MinimumWriteDifference;
            numericUpDownOutput.Value = (decimal)Properties.Settings.Default.OutputLines;
            timerSyncTick.Enabled = true;
            timerSyncTick.Start();
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            string folderA = "", folderB = "";
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    folderA = fbd.SelectedPath;
                }
            }
            if (folderA.Length > 0)
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        if (folderA != fbd.SelectedPath)
                            folderB = fbd.SelectedPath;
                    }
                }
            }
            if (folderA.Length > 0 && folderB.Length > 0)
            {
                SyncObject syncObject = new SyncObject();
                syncObject.folderA = folderA;
                syncObject.folderB = folderB;
                syncObject.fileName = "Temp";
                syncObjects.Add(syncObject);
                SaveObjects();
                DisplayObjects();
            }
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

        public void DisplayObjects()
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

        public void UpdateObject()
        {
            if (domainUpDown1.SelectedItem != null)
            {
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
            if (!fillingBoxes)
            {
                manualEntry = true;
                UpdateObject();
            }
        }

        private void textBoxFolderB_TextChanged(object sender, EventArgs e)
        {
            if (!fillingBoxes)
            {
                manualEntry = true;
                UpdateObject();
            }
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
                bool requireYield = true;
                for (int i = 0; i < syncObjects.Count; i++)
                {
                    try
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
                    catch { }
                    requireYield = false;
                    yield return true;
                }
                if (requireYield) yield return true;
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
                UpdateObject();
            }
        }
    }

    public class SyncObject
    {
        public string folderA = "", folderB = "", fileName = "";

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

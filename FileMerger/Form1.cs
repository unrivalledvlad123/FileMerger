using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileMerger
{
    public partial class Form1 : Form
    {

        public static List<string> AllFilesList = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            int index = 1;
            DirSearch(tbSource.Text);

            string sourcePath = tbSource.Text;
            string targetPath = tbDestination.Text;

            if (Directory.Exists(sourcePath))
            {
                foreach (string str in Directory.GetFiles(sourcePath))
                {
                    AllFilesList.Add(str);
                }

                foreach (string s in AllFilesList)
                {
                                      
                    string fileName = Path.GetFileName(s);
                    string destFile = Path.Combine(targetPath, fileName);
                   // File.Copy(s, destFile, false);
                    try
                    {
                        File.Copy(s, destFile, false);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            List<string> temp = fileName.Split('.').ToList();
                            File.Copy(s, Path.Combine(targetPath, temp[0] + "(" + index + ")." + temp[1]), false);
                        }
                        catch (Exception)
                        {
                            index++;
                            List<string> temp = fileName.Split('.').ToList();
                            File.Copy(s, Path.Combine(targetPath, temp[0] + "(" + index + ")." + temp[1]), false);
                        }
                      
                    }
                   
                }
                MessageBox.Show("the merging is complete", "Protoss Archon",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Source path does not exist!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void DirSearch(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        AllFilesList.Add(f);
                    }
                    DirSearch(d);
                }
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
    }
}

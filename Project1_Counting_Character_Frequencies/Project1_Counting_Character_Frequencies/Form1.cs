﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Project1_Counting_Character_Frequencies
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        private void btn_open_Click(object sender, EventArgs e)
        {
            StreamReader inStream = null;
            string line;
            Dictionary<string, int> wordList = new Dictionary<string,int> {};
            int wordsize=0;
         

            OpenFileDialog openFileDiag = new OpenFileDialog();
            openFileDiag.InitialDirectory = @"C:\";
            openFileDiag.Title = "Open Text File";
            openFileDiag.CheckFileExists = true;
            openFileDiag.CheckPathExists = true;
            openFileDiag.Filter="text file (*.txt)|*.txt";
            

            if (openFileDiag.ShowDialog() == DialogResult.OK)
            {
                txt_filepath.Text = openFileDiag.FileName;
                

                try
                {
                    if ((inStream = File.OpenText(openFileDiag.FileName)) != null)
                    {
                        using (inStream)
                        {
                            while ((line = inStream.ReadLine()) != null) // while not end of file
                            {
                                if (line != string.Empty) // if line not empty
                                {
                                    //https://msdn.microsoft.com/en-us/library/bb383973.aspx

                                    // read each word from each sentence and put it into array
                                    string[] words = line.Split(' ');

                                    // check if the word is already in the dictionary or not
                                    foreach (string word in words)
                                    {
                                        if (word.Length > wordsize)
                                        {
                                            wordsize = word.Length;
                                        }
                                        if (wordList.ContainsKey(word))
                                        {
                                            wordList[word]++; // if yes, then increase it value by 1
                                        }
                                        else
                                        {
                                            wordList.Add(word, 1);// if not, add that word into the dictionary
                                        }
                                    }

                                    //foreach (var word in line)
                                    //{
                                    //    if (wordList.ContainsKey(word.ToString()))
                                    //    {
                                    //        wordList[word.ToString()]++;
                                    //    }
                                    //    else
                                    //    {
                                    //        wordList.Add(word.ToString(),1);
                                    //    }
                                    //}//end foreach
                                    

                                }// end if

                            }//end while

                        }//end using
                    }//end if

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: unable to open file");
                }
            }//end if

            string str = "";

            foreach (var temp in wordList)
            {
                if (temp.Key.ToString().Length == wordsize)
                {
                    str += " " + temp.Key.ToString();
                }
                
            }
            

            label1.Text = wordList["the"].ToString() + "   " + wordList["principles"].ToString();
            //label2.Text = wordList["t"].ToString();
            label2.Text = str;

        }// end btn_open click
    }
}

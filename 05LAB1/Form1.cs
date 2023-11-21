using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationTextFile
{
    public partial class frmRegistration : Form
    {
        public frmRegistration()
        {
            InitializeComponent();
        }

        public string FileName(string fileName)
        {
            string txtPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            try
            {
                using (FileStream txtCheck = new FileStream(Path.Combine(txtPath, fileName), FileMode.CreateNew))
                { }
            }
            catch (IOException)
            {
                throw new FileNameException("Error: File name/Student Number's file name already existed. Please try again.");
            }
            return fileName;
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            string[] ListOfProgram = new string[]{
                "BS Information Technology",
                "BS Computer Science",
                "BS Information Systems",
                "BS in Accountancy",
                "BS in Hospitality Management",
                "BS in Tourism Management"
        };

            for (int i = 0; i < 6; i++)
            {
                cbPrograms.Items.Add(ListOfProgram[i].ToString());
            }
        }

        public static string SetFileName;
        private void btnRegister_Click(object sender, EventArgs e)
        {
            frmRegistration.SetFileName = txtStudentNo.Text + ".txt";

            try
            {
                FileName(SetFileName);

                long getStudentNo = long.Parse(txtStudentNo.Text);
                long getContactNo = long.Parse(txtContactNo.Text);
                string getProgram = cbPrograms.Text;
                string getGender = cbGender.Text;
                string getFullName = txtLastName.Text + ", " + txtFirstName.Text + " " + txtMiddleInitial.Text;
                string getBirthday = dtBday.Text;
                int getAge = int.Parse(txtAge.Text);

                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, frmRegistration.SetFileName)))
                {
                    string studentInfo = $"Student No.: {getStudentNo} \nFull Name: {getFullName} \nProgram: {getProgram} \n" +
                                         $"Gender: {getGender} \nAge: {getAge} \nBirthday: {getBirthday} \nContact Number: {getContactNo}";

                    outputFile.WriteLine(studentInfo);
                    MessageBox.Show("Success!");
                }
            }
            catch (FileNameException fne)
            {
                MessageBox.Show(fne.Message, "File Name Exception");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmStudentRecord frmStudRec = new FrmStudentRecord();
            frmStudRec.ShowDialog();
        }
    }
}

public class FileNameException : IOException
{
    public FileNameException(string filename) : base(filename)
    {
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;

namespace BLLAlgorithm
{
    public partial class Form1 : Form
    {


        public static ClassDB ClassDB { get; set; }

        public Form1()
        {
            InitializeComponent();
            
            ClassDB = new ClassDB();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Algorithm a = new Algorithm();
            a.RunAlgorithm();
            // dataGridView1.DataSource = classDB.GetAllJobs().ToList();
            //List<Jobs_Dto> lstJobs = ClassDB.GetAllJobs();
            //List<Jobs_to_shift_Dto> lstJobToShift;
            //foreach (var item in lstJobs)
            //{
            //    lstJobToShift = ClassDB.GetJobsToShiftByJob(item.job_id);
            //    dataGridView1.DataSource = lstJobToShift.ToList();
            //    MessageBox.Show("continue");
            //}

        }



        //Build in the suitable mat cell a list of list of suitable shift placement
        public void BuildSuitableShiftsPerJob(int dayShift, int numShift, int jobId, int numEmployeesRequired)
        {
            //PlacementLevelA p = MatSuitableEmployeesToTheWeek[numShift, dayShift].Where(x => x.JobId == jobId).FirstOrDefault();
            //// עושים את כל השיבוצים האפשריים ברמה של פר גוב וכך נוכל גם לבדוק פיזור שנות וותק
            //CombPerJob combLst = new CombPerJob(jobId, numEmployeesRequired);
            //combLst.LstComb = GetAllCombinationsForEmployeesList(p.Employees, numEmployeesRequired);
        }

       

       



        //Set priority to each cell in the mat
        //public void SetPriority()
        //{
        //    for (int i = 1; i < NUMSHIFTS; i++)
        //    {
        //        for (int j = 1; j < NUMDAYSINWEEK; j++)
        //        {
        //            if (MatSuitableShiftsPerJobToTheWeek[i, j] != null)
        //            {
        //                foreach (var itemMat in MatSuitableShiftsPerJobToTheWeek[i, j])
        //                {
        //                    foreach (var item in itemMat.Employees)
        //                    {
        //                        itemMat.SumPointsPriority += item.priority_id;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}





        /// <summary>
        /// The function sorts the lst as per size of the inner list in each organ in the list:lst. 
        /// from the smallest to the largest 
        /// </summary>
        /// <param name="lst"></param>
        /// <returns>The sort list</returns>
        public List<CombPerJob> SortSmallToBig(List<CombPerJob> lst)
        {
            List<CombPerJob> sortLst = new List<CombPerJob>();
            CombPerJob l = new CombPerJob();
            int min = int.MaxValue, min_i = 0;
            while (lst != null)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].LstComb.Count() < min)
                    {
                        min = lst[i].LstComb.Count();
                        min_i = i;
                        l = lst[i];
                    }
                }
                lst.RemoveAt(min_i);
                sortLst.Add(l);
            }
            return sortLst;
        }

       
       
        





        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

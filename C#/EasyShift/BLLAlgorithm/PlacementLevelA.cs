using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using BLL;
using DAL;

namespace BLLAlgorithm
{
    public class PlacementLevelA
    {
        EasyShiftEntities db = new EasyShiftEntities();
        public static ClassDB classDB;

        private readonly int NUMSHIFTS = 3 + 1;
        public int NumShifts() { return this.NUMSHIFTS; }
        private readonly int NUMDAYSINWEEK = 7 + 1;
        public int NumDaysInWeek() { return this.NUMDAYSINWEEK; }
        public LstByJobLevelA[,] MatSuitableEmployeesToTheWeek { get; set; }

        public void InitilizeMat()
        {
            //mat initialization
            MatSuitableEmployeesToTheWeek = new LstByJobLevelA[NUMSHIFTS, NUMDAYSINWEEK];
            for (int i = 1; i < NUMSHIFTS; i++)
            {
                for (int j = 1; j < NUMDAYSINWEEK; j++)
                {
                    MatSuitableEmployeesToTheWeek[i, j] = new LstByJobLevelA();
                }
            }
        }


        public void SaveInMat(int shift_id, int job_id, List<Employee_request_Dto> l)
        {
            //הפעולה מקבלת מס משמרת ומס תפקיד ורשימת העובדים שיכולים להתאים לכך ושומרת במטריצה
            List<Shift_Dto> allShifts = classDB.GetAllShifts();
            //לבדוק אם אפשר לסמוך על המספור האוטומטי שלא תהיה לי חריגה מגבולות המטריצה
            int shiftType = allShifts.Where(x => x.shift_id == shift_id).FirstOrDefault().shift_type_id;
            int day = allShifts.Where(x => x.shift_id == shift_id).FirstOrDefault().day;
            this.MatSuitableEmployeesToTheWeek[shiftType, day]=new LstByJobLevelA(job_id, l);
        }



    }
}

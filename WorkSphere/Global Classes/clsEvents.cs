using System;

namespace WorkSphere.Global_Classes
{
    public class EmployeeidAndStepidEventArgs : EventArgs
    {
        public int EmployeeID { get; }
        public int StepID { get; }

        public EmployeeidAndStepidEventArgs(int empID, int stepID)
        {
            this.EmployeeID = empID;
            this.StepID = stepID;
        }
    }

    public class BoolEventArgs : EventArgs
    {
        public bool result { get; set; } = false;
    }

    public static class clsEventHub
    {
        public static event EventHandler<BoolEventArgs> AttendanceChanged;
        public static event EventHandler<BoolEventArgs> AddNewEmployee;
        public static event EventHandler<BoolEventArgs> AddNewAbsence;
        public static event EventHandler<BoolEventArgs> LeaveChanged;

        public static void RaiseAttendaneChanged()
        {
            AttendanceChanged?.Invoke(null, new BoolEventArgs { result = true });
        }
        public static void RaiseAddNewEmployee()
        {
            AddNewEmployee?.Invoke(null, new BoolEventArgs { result = true });
        }
        public static void RaiseAddNewAbsence()
        {
            AddNewAbsence?.Invoke(null, new BoolEventArgs { result = true });
        }
        public static void RaiseLeaveChanged()
        {
            LeaveChanged?.Invoke(null, new BoolEventArgs { result = true });
        }
    }

}

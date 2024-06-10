using System;

namespace WpfApp1.Model
{
    public class Admin : User
    {
        private DateTime dateOfHire;
        private decimal salary;

        public DateTime DateOfHire
        {
            get { return dateOfHire; }
            set
            {
                if (dateOfHire != value)
                {
                    dateOfHire = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Salary
        {
            get { return salary; }
            set
            {
                if (salary != value)
                {
                    salary = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}

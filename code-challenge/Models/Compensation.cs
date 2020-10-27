using System;

namespace challenge.Models
{
    public class Compensation
    {
        public Guid Id { get; set; }

        public Employee Employee { get; set; }

        public double Salary { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}

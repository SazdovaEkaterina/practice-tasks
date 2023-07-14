
using System.Text;

namespace Task1
{
    public class Employee
    {
        public Guid Guid { get; set; } 
        public string Name { get; set; }
        public string LastName { get; set; }
        protected DateTime DateOfBirth { get; set; }
        protected DateTime DateStartedWorking { get; set; }
        public double PayPerHour { get; set; }
        protected Currency PayCurrency { get; set; }
        public Sector Sector { get; set; }
        public int WorkingHours { get; set; }
        public List<Item> Inventory { get; set; }
        
        protected List<DateOnly> SickDays { get; set; }

        protected DateTime DateEndedWorking { get; set; }

        private const double BonusEighteenMonths = 0.1;
        private const double BonusThirtySixMonths = 0.15;

        private const double BonusLessThanThreeSickDays = 0.02;
        private const double BonusMoreThanTwelveSickDays = -0.02;

        public Employee(string name, string lastName, DateTime dateOfBirth, 
            DateTime dateStartedWorking, double payPerHour, Currency payCurrency, Sector sector)
        {
            Guid = Guid.NewGuid();
            Name = name;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            DateStartedWorking = dateStartedWorking;
            PayPerHour = payPerHour;
            PayCurrency = payCurrency;
            Sector = sector;
            Inventory = new List<Item>();
            SickDays = new List<DateOnly>();
        }

        /// <summary>
        /// Calculates the number of full years worked at the firm.
        /// </summary>
        /// <returns>The number of years worked at the firm.</returns>
        protected int YearsWorkedAtTheFirm()
        {
            var start = DateStartedWorking;
            var end = DateTime.Now;
            return (end.Year - start.Year - 1) +
                   (((end.Month > start.Month) || 
                     ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }

        /// <summary>
        /// Calculates the number of full months worked at the firm.
        /// </summary>
        /// <returns>The number of months worked at the firm</returns>
        private int MonthsWorkedAtTheFirm()
        {
            var start = DateStartedWorking;
            var end = DateTime.Now;
            return Math.Abs(12 * (start.Year - end.Year) + start.Month - end.Month);
        }

        /// <summary>
        /// Checks if an employee is a newcomer i.e. has worked at the firm for less than 3 full months.
        /// </summary>
        /// <returns>Whether the employee is a newcomer.</returns>
        public bool IsNewcomer()
        {
            return MonthsWorkedAtTheFirm() < 3;
        }

        /// <summary>
        /// Resets the employee sick days i.e. deletes all sick days which are more than 3 months old.
        /// </summary>
        public void ResetSickDays()
        {
            foreach (DateOnly sickDay in SickDays)
            {
                if (Math.Abs(12 * (sickDay.Year - DateTime.Now.Year) + sickDay.Month - DateTime.Now.Month) > 3)
                {
                    SickDays.Remove(sickDay);
                }
            }
        }

        /// <summary>
        /// Calculates the employee's salary based on hourly pay, hours worked & bonuses.
        /// </summary>
        /// <returns>The salary of the employee.</returns>
        protected double Salary()
        {
            double baseSalary =  WorkingHours * PayPerHour;
            double bonus = 0;
            
            ResetSickDays();

            if (MonthsWorkedAtTheFirm() > 36)
            {
                bonus += BonusThirtySixMonths;
            } else if (MonthsWorkedAtTheFirm() > 18)
            {
                bonus += BonusEighteenMonths;
            }

            if (SickDays.Count < 3)
            {
                bonus += BonusLessThanThreeSickDays;
            } else if (SickDays.Count > 12)
            {
                bonus += BonusMoreThanTwelveSickDays;
            }

            var salary = baseSalary + baseSalary * bonus;

            return salary;
        }

        /// <summary>
        /// Borrowing an item from the supervisor.
        /// </summary>
        /// <param name="supervisor">The employee's supervisor.</param>
        /// <param name="item">The item the employee wants to borrow.</param>
        public virtual void BorrowItemFromSupervisor(Supervisor supervisor, Item item)
        {
            if (Sector != supervisor.Sector)
            {
                Console.WriteLine($"{supervisor.Name} {supervisor.LastName} isn't your supervisor, " +
                                  $"you can't borrow items from them");
            }
            else
            {
                supervisor.Inventory.Remove(item);
                this.Inventory.Add(item);
                Console.WriteLine($"You have successfully borrowed {item} from your supervisor " +
                                  $"{supervisor.Name} {supervisor.LastName}.");
            }
        }

        /// <summary>
        /// Requesting a list of sick days from the supervisor.
        /// </summary>
        /// <param name="supervisor">The employee's supervisor.</param>
        /// <param name="sickDays">The dates which they want to take as sick days.</param>
        public virtual void RequestSickDaysFromSupervisor(Supervisor supervisor, List<DateOnly> sickDays)
        {
            if (Sector != supervisor.Sector)
            {
                //if requesting from other supervisor, that isn't their own
                Console.WriteLine($"{supervisor.Name} {supervisor.LastName} isn't your supervisor, " +
                                  $"you can't request sick days from them");
            }
            else
            {
                SickDays.AddRange(sickDays);
                Console.WriteLine(@$"Employee {Name} {LastName} has gotten {sickDays.Count} new sick days 
                                  from supervisor {supervisor.Name} {supervisor.LastName}");
            }
        }

        /// <summary>
        /// Transferring the employee to another sector.
        /// </summary>
        /// <param name="otherSector">The sector to which the employee is transferring to.</param>
        public void TransferToAnotherSector(Sector otherSector)
        {
            this.Sector.Employees.Remove(this);
            otherSector.Employees.Add(this);
            this.Sector = otherSector;
            Console.WriteLine($"Employee {Name} {LastName} has transferred to {otherSector}");
        }

        /// <summary>
        /// Employee resigns from the firm.
        /// </summary>
        public void Resign()
        {
            if (IsNewcomer())
            {
                DateEndedWorking = DateTime.Now;
                Console.WriteLine($"Employee {Name} {LastName} has resigned.");
                this.Sector.Employees.Remove(this);
            }
            else
            {
                DateEndedWorking = DateTime.Now.AddDays(7);
                Console.WriteLine($"Employee {Name} {LastName} will resign in 7 days - {DateEndedWorking}");
            }
        }
        
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{Name} {LastName}");
            stringBuilder.AppendLine($"Date of birth: {DateOfBirth}");
            stringBuilder.AppendLine($"Started working on: {DateStartedWorking}");
            stringBuilder.AppendLine($"Years in firm: {YearsWorkedAtTheFirm()}");
            stringBuilder.AppendLine($"Pay: {PayCurrency} {PayPerHour}/hr");
            stringBuilder.AppendLine($"Hours: {WorkingHours}");
            stringBuilder.AppendLine($"Salary: {PayCurrency} {Salary()}");
            stringBuilder.Append("Inventory: ");
            foreach (Item item in Inventory)
            {
                stringBuilder.Append($"{item}, ");
            }
            stringBuilder.Append("\n");
            return stringBuilder.ToString();
        }
    }
}
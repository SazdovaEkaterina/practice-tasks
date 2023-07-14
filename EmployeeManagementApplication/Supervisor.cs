
using System.Text;

namespace Task1
{
    public class Supervisor : Employee
    {
        public string Email { get; set; }
        public Sector Sector { get; set; }

        public Supervisor(string name, string lastName, DateTime dateOfBirth,
            DateTime dateStartedWorking, double payPerHour, Currency payCurrency, Sector sector, string email)
            : base(name, lastName, dateOfBirth,  dateStartedWorking, payPerHour, payCurrency, sector)
        {
            Sector = sector;
            Email = email;
        }

        public void DisplayAllEmployees()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("\n======================================================================");
            stringBuilder.AppendLine($"List of employees supervised by {Name} {LastName}");
            foreach (Employee employee in Sector.Employees)
            {
                stringBuilder.AppendLine("-----");
                stringBuilder.Append(employee);
            }

            Console.Write(stringBuilder.ToString());
        }

        public void DisplayAllNewcomers()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("\n======================================================================");
            stringBuilder.AppendLine($"List of newcomers supervised by {Name} {LastName}");
            
            var newcomers = Sector.Employees.Where(e => e.IsNewcomer()).ToList();
            foreach (Employee newcomer in newcomers)
            {
                stringBuilder.AppendLine("-----");
                stringBuilder.Append(newcomer);
            }

            Console.Write(stringBuilder.ToString());
        }

        public void SetEmployeeHours(Employee employee, int hours)
        {
            if (employee.Sector != Sector)
            {
                Console.WriteLine($"Employee {employee.Name} {employee.LastName} doesn't belong in your sector.");
            }
            else if (employee.IsNewcomer())
            {
                Console.WriteLine($"Employee {employee.Name} {employee.LastName} employee is a newcomer, the working hours are fixed.");
            }
            else
            {
                employee.WorkingHours = hours;
                Console.WriteLine($"The working hours of employee {employee.Name} {employee.LastName} have changed to {hours}");
            }
        }

        public void SetEmployeePayPerHour(Employee employee, double payPerHour)
        {
            if (employee.Sector != Sector)
            {
                Console.WriteLine($"The employee {employee.Name} {employee.LastName} doesn't belong in your sector.");
            }
            else
            {
                employee.PayPerHour = payPerHour;
                Console.WriteLine($"The pay for hour for employee {employee.Name} {employee.LastName} " +
                                  $"has successfully been changed to {payPerHour}");
            }
        }
        
        public void LendItemToEmployee(Employee employee, Item item)
        {
            if (employee.GetType() == typeof(Supervisor))
            {
                Console.WriteLine("You cannot lend items to supervisors. " +
                                  "Only regular employees can borrow items");
            }
            else if (Sector != employee.Sector)
            {
                Console.WriteLine($"{employee.Name} {employee.LastName} isn't your employee, " +
                                  $"you can't lend items to them");
            }
            else
            {
                this.Inventory.Remove(item);
                employee.Inventory.Add(item);
                Console.WriteLine($"You have successfully lended {item} to your employee " +
                                  $"{employee.Name} {employee.LastName}.");
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"<<SUPERVISOR>> {Name} {LastName}");
            stringBuilder.AppendLine($"Date of birth: {DateOfBirth}");
            stringBuilder.AppendLine($"Email address: {Email}");
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

        public override void BorrowItemFromSupervisor(Supervisor supervisor, Item item)
        {
            Console.WriteLine("You are a supervisor, so you cannot borrow items. " +
                              "Only regular employees can borrow items from their supervisors.");
        }

        public override void RequestSickDaysFromSupervisor(Supervisor supervisor, List<DateOnly> sickDays)
        {
            SickDays.AddRange(sickDays);
            Console.WriteLine($"Supervisor {Name} {LastName} has gotten {sickDays.Count} new sick days.");
        }
    }
}

namespace Task1
{
	public class Sector
	{
		public Guid Guid { get; set; }
		public string Name { get; set; }
		
		public List<Employee> Employees { get; set; }
		public Supervisor Supervisor { get; set; }

		public Sector(string name)
		{
			Name = name;
			Employees = new List<Employee>();
			Guid = Guid.NewGuid();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}


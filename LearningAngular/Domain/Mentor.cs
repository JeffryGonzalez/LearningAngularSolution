using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningAngular.Domain
{

	public abstract class Person
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}

	public class Mentor : Person
	{

		public bool TakingNewStudents { get; set; }
		public int StudentCount { get; set; }
		public virtual ICollection<Student> Students { get; set; }
		public virtual ICollection<Level> LevelsApprovedToTeach { get; set; }
	}

	public class Student : Person
	{
		public virtual Mentor Mentor { get; set; }
		public virtual Level Level { get; set; }
	}

	public class Level
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<Mentor> Mentors { get; set; } 
	}

}
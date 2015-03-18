using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace LearningAngular.Domain
{
	public class MentoringContext : DbContext
	{
		public MentoringContext()
		{
			Database.SetInitializer<MentoringContext>(new MentoringInitializer());
		}
		public DbSet<Mentor> Mentors { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Level> Levels { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Level>().HasMany(t => t.Mentors).WithMany(t => t.LevelsApprovedToTeach);
			base.OnModelCreating(modelBuilder);
		}
	}

	public class MentoringInitializer : DropCreateDatabaseAlways<MentoringContext>
	{
		protected override void Seed(MentoringContext context)
		{
			var levels = new List<Level>
			{
				new Level() {Name = "Beginner"},
				new Level() {Name = "Intermediate"},
				new Level() {Name = "Advanced"}
			};

			context.Levels.AddOrUpdate(d=>d.Name,
				levels.ToArray());


			var studentPhil = new Student()
			{
				FirstName = "Phil",
				LastName = "Jones",
				Email = "phil@aol.com",
				Level = levels[0]
			};
			var studentMary = new Student()
			{
				FirstName = "Mary",
				LastName = "Hall",
				Level = levels[1],
				Email = "mary@aol.com"
			};
			var studentHenry = new Student()
			{
				FirstName = "Henry",
				LastName = "Gonzalez",
				Level = levels[2],
				Email = "henry@aol.com"
			};

			var studentViolet = new Student()
			{
				FirstName = "Violet",
				LastName = "Gonzalez",
				Email = "violet@aol.com",
				Level = levels[1]
			};

			var teacherPaul = new Mentor()
			{
				FirstName = "Paul",
				LastName = "Sink",
				StudentCount = 3,
				TakingNewStudents = true,
				LevelsApprovedToTeach = new List<Level>()
				{
					levels[0]
				},
				Students = new List<Student>()
				{
					studentHenry
				}
			};
			var teacherGeorge = new Mentor()
			{
				FirstName = "George",
				LastName = "Jones",
				StudentCount = 18,
				TakingNewStudents = false,
				LevelsApprovedToTeach = new List<Level>()
				{
					levels[0],
					levels[1]
				},
				Students = new List<Student>()
				{
					studentMary,
					studentPhil
				}
			};
			var teacherRinger = new Mentor()
			{
				FirstName = "Ringo",
				LastName = "Star",
				StudentCount = 0,
				TakingNewStudents = true,
				LevelsApprovedToTeach = new List<Level>()
				{
					levels[0],
					levels[1],
					levels[2]
				},
				Students = new List<Student>()
				{
					studentViolet
				}
			};
			context.Mentors.AddOrUpdate(m => m.LastName,
				 teacherPaul, teacherGeorge, teacherRinger

					);
		}
	}
}
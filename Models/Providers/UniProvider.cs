using HTMLprojectMVC.Models.Context;
using HTMLprojectMVC.Models.Context.Entities;
using System;
using System.Linq;

namespace HTMLprojectMVC.Models.Providers
{
     static class UniProvider
    {
        public static void AddStudent(int groupRefId, string firstName, string lastName, string middleName = null)
        {
            if (string.IsNullOrWhiteSpace(middleName))
            {
                middleName = null;
            }

            using (var context = new UniContext())
            {
                var group = context.Groups.SingleOrDefault(g => g.Id.Equals(groupRefId));

                if (group != null)
                {
                    var student = new Student
                    {
                        Group = group,
                        FirstName = firstName,
                        MiddleName = middleName,
                        LastName = lastName
                    };

                    context.Students.Add(student);

                    context.SaveChanges();
                }
            }
        }

        public static void RemoveStudent(int id)
        {
            using (var context = new UniContext())
            {
                var student = context.Students.FirstOrDefault(s => s.Id.Equals(id));

                if (student != null)
                {
                    context.Students.Remove(student);

                    context.SaveChanges();
                }
            }
        }

        public static void UpdateStudent(int id, int groupRefId, string firstName, string lastName, string middleName = null)
        {
            if (string.IsNullOrWhiteSpace(middleName))
            {
                middleName = null;
            }

            using (var context = new UniContext())
            {
                var group = context.Groups.SingleOrDefault(g => g.Id.Equals(groupRefId));

                var student = context.Students.SingleOrDefault(s => s.Id.Equals(id));
                if (group != null && student != null)
                {
                    student.FirstName = firstName;
                    student.LastName = lastName;
                    student.MiddleName = middleName;
                    student.Group = group;

                    context.SaveChanges();
                }
            }
        }
    }
}

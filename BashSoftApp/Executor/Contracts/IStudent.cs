using System;
using System.Collections.Generic;

namespace Executor.Contracts
{
    public interface IStudent : IComparable<IStudent>
    {
        string UserName { get; set; }

        IReadOnlyDictionary<string, ICourse> EnrolledCourses { get; }

        IReadOnlyDictionary<string, double> MarksByCourseName { get; }

        void EnrollInCourse(ICourse course);

        void SetMarkOnCourse(string courseName, params int[] scores);

        string GetMarkForCourse(string courseName);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using Session2_EF.Data;

namespace Session2_EF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<UniversityContext>().Options;
            using (var context = new UniversityContext(options))
            {
                context.Database.EnsureCreated();
                //context.Students.Where(e => e.ID == 1);
            }
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Models{
    public class ExpensesTrackerDbContext: DbContext
    {
        public DbSet<Expense> Expenses {get; set;}

        public ExpensesTrackerDbContext(DbContextOptions<ExpensesTrackerDbContext> options)
        : base (options){

        }



    }
}




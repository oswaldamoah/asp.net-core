using System;
using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.Models;

public class Expense
{
    public int Id {get; set;}

    public decimal Value {get; set;}

    [Required] // Description is now required
    public string? Description {get; set;} 
    // ? makes it nullable (can be empty)


}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExpensesTracker.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExpensesTracker.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ExpensesTrackerDbContext _context;
    public HomeController(ILogger<HomeController> logger, ExpensesTrackerDbContext context)
    {
        _logger = logger;
        _context = context;

    }

// Routing for Pages : Returns the view with the same name as the method
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

      public IActionResult Expenses() // Routing for expenses
    { 
        // Converts all expenses in db to list
        var allExpenses = _context.Expenses.ToList();
        // pass the list as a View argument

        var totalExpenses = allExpenses.Sum(expense => expense.Value); // Sum of expense values

        // Viewbag: Bag that allows the view to access controller variables:
        ViewBag.Total = totalExpenses;

        return View(allExpenses);
    }
    

    public IActionResult CreateEditExpense(int? id) // Routing for CreateEditExpense 
    // nullable ID for create condition (id doesn't exist until after creation) 
    {
        if(id!=null){
            // editing condition: Only condition because this method is only used for EDIT
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
         return View(expenseInDb); // returns form view and sends pre-edited expense's id to the form 
        }
        // else the form view should still be shown and form logic adds new expense
        _context.SaveChanges();
        return View();
    }

        public IActionResult DeleteExpense(int id){
        var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
        _context.Expenses.Remove(expenseInDb);
        _context.SaveChanges();
        // if ID matches with ID in the database, delete it from the database (100% probability)
        return RedirectToAction("Expenses");
    }

    public IActionResult CreateEditExpenseForm(Expense model) // Routing for form's submit button
    {
        if(model.Id ==0){
            // Create if form ID was not sent to the form
            _context.Expenses.Add(model);
        }
        else{
            //Edit if form ID was sent to the form
            _context.Update(model);
        }
        
        
        _context.SaveChanges();

        return RedirectToAction("Expenses"); // Redirects you to Expenses page after submit
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

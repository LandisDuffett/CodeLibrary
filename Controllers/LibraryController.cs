using System;
using CodeLibrary.Models;
using CodeLibrary.Services;
namespace CodeLibrary.Controllers
{
  class LibraryController
  {
    private LibraryService _service { get; set; } = new LibraryService();
    private bool _running { get; set; } = true;
    public void Run()
    {
      Console.Clear();
      while (_running)
      {
        GetUserInput();
      }
    }
    private void GetUserInput()
    {
      Console.WriteLine(
      @"Your choices are:
          1) add book
          2) get info
          3) check out a book
          4) return a book
          5) remove book
          6) quit");
      string choice = Console.ReadLine();
      switch (choice)
      {
        case "1":
          Add();
          break;
        case "2":
          Info();
          break;
        case "3":
          ToggleStatus(true);
          break;
        case "4":
          ToggleStatus(false);
          break;
        case "5":
          Delete();
          break;
        case "6":
          _running = false;
          break;
        default:
          Console.WriteLine("Choice not found. Please choose again.");
          break;
      }
    }

    private void Add()
    {
      Console.Write("Title: ");
      string title = Console.ReadLine();
      Console.Write("Author: ");
      string author = Console.ReadLine();
      Console.Write("Description: ");
      string description = Console.ReadLine();
      Book newBook = new Book(title, author, description);
      _service.Add(newBook);
      Console.Write($"Successfully added {title} to the collection");
    }

    private void Info()
    {
      Console.Clear();
      Console.WriteLine(@"
      Welcome to C# Library Adminobot.
      You can perform several basic librarian administrative functions
      using this service. Just make your selection of task from the 
      main menu.
      Press <Enter> to return to the main menu.");
      Console.ReadLine();
      GetUserInput();
    }

    private void ToggleStatus(bool available)
    {
      Console.Clear();
      Console.WriteLine(_service.GetBooks(available));
      string status = available ? "check out" : "return";
      Console.WriteLine($"Which book would you like to {status}?");
      string selectionStr = Console.ReadLine();
      Console.Clear();
      if (int.TryParse(selectionStr, out int selection) && selection > 0)
      {
        Console.WriteLine(_service.ToggleStatus(selection - 1, available));
        return;
      }
      else
      {
        Console.WriteLine("Invalid selection. You must choose a number greater than 0.");
      }
    }

    private void Delete()
    {
      Console.WriteLine(_service.GetBooks(true));
      Console.WriteLine("Enter title of book to remove:");
      string title = Console.ReadLine().ToLower();
      int index = _service.FindIndexByTitle(title);
      if (index == -1)
      {
        Console.WriteLine("No Book by that Title");
      }
      Console.WriteLine("Delete book? Type 'confirm' to remove book.");
      string confirm = Console.ReadLine();
      if (confirm != "confirm")
      {
        Console.WriteLine("Invalid input, book cannot be deleted.");
        Delete();
      }
      _service.Remove(index);
      Console.Clear();
      Console.WriteLine("Successfully Removed Book");
    }
  }
}
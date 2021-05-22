using System.Collections.Generic;
using CodeLibrary.Models;
namespace CodeLibrary.Services
{
  class LibraryService
  {
    private List<Book> Books { get; set; }

    internal string GetBooks(bool available)
    {
      string list = "";
      var filtered = Books.FindAll(b => b.IsAvailable == available);
      if (available)
      {
        list += "Available Books: \n";
      }
      else
      {
        list += "Checked Out Books: \n";
      }
      if (filtered.Count == 0)
      {
        list = "No books match search criteria.";
      }
      for (int i = 0; i < filtered.Count; i++)
      {
        var book = filtered[i];
        if (book.IsAvailable == available)
        {
          list += $"{i + 1}. {book.Title} - by {book.Author} - content: {book.Description}\n";
        }
      }
      return list;
    }

    internal string ToggleStatus(int selection, bool available)
    {
      var books = Books.FindAll(b => b.IsAvailable == available);
      if (selection < books.Count)
      {
        books[selection].IsAvailable = !books[selection].IsAvailable;
        return available ? "Book checked out." : "Book returned.";
        /*return available ? ($"The book {books[selection].Title} is now checked out.") : ($"The book {books[selection].Title} has been returned.");*/
      }
      return "Invalid Input, please provide a number listed.";
    }

    internal void Add(Book newBook)
    {
      Books.Add(newBook);
    }

    internal string All()
    {
      string list = "";
      for (int i = 0; i < Books.Count; i++)
      {
        var book = Books[i];
        {
          list += $"{i + 1}. {book.Title}\n";
        }
      }
      return list;
    }
    public LibraryService()
    {
      Books = new List<Book>(){
        new Book("Moby Dick", "Herman Melville", "A man and a whale."),
        new Book("Ulysses", "James Joyce", "?"),
        new Book("Demons", "Fyodor Dostoevsky", "A town possessed by demons, sort of."),
        new Book("Paradise Lost", "John Milton", "Man's first disobedience and its fruits.")
      };
    }

    internal int FindIndexByTitle(string title)
    {
      return Books.FindIndex(b => b.Title.ToLower() == title);
    }

    internal string FindDescriptionByIndex(int index)
    {
      return Books[index].Description;
    }

    internal string FindTitleByIndex(int index)
    {
      return Books[index].Title;
    }
    internal void Remove(int index)
    {
      Books.RemoveAt(index);
    }
  }
}
using System;
using CodeLibrary.Controllers;

namespace CodeLibrary
{
  class Program
  {
    static void Main(string[] args)
    {
      new LibraryController().Run();
    }
  }
}

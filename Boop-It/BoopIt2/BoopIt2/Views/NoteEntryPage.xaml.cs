﻿using BoopIt2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoopIt2.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NoteEntryPage : ContentPage
  {
    public string ItemId
    {
      set
      {
        LoadNote(value);
      }
    }

    public NoteEntryPage()
    {
      InitializeComponent();

      // Set the BindingContext of the page to a new Note.
      BindingContext = new Notes();
    }

    void LoadNote(string filename)
    {
      try
      {
        // Retrieve the note and set it as the BindingContext of the page.
        Notes notes = new Notes
        {
          Filename = filename,
          Text = File.ReadAllText(filename),
          Date = File.GetCreationTime(filename)
        };
        BindingContext = notes;
      }
      catch (Exception)
      {
        Console.WriteLine("Failed to load note.");
      }
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
      var note = (Notes)BindingContext;

      if (string.IsNullOrWhiteSpace(note.Filename))
      {
        // Save the file.
        var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
        File.WriteAllText(filename, note.Text);
      }
      else
      {
        // Update the file.
        File.WriteAllText(note.Filename, note.Text);
      }

      // Navigate backwards
      await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
      var note = (Notes)BindingContext;

      // Delete the file.
      if (File.Exists(note.Filename))
      {
        File.Delete(note.Filename);
      }

      // Navigate backwards
      await Shell.Current.GoToAsync("..");
    }
  }
}

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
  public partial class NotesPage : ContentPage
  {
    string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");

    public NotesPage()
    {
      InitializeComponent();
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      var notes = new List<Notes>();

      // Create a Note object from each file.
      var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
      foreach (var filename in files)
      {
        notes.Add(new Notes
        {
          Filename = filename,
          Text = File.ReadAllText(filename),
          Date = File.GetCreationTime(filename)
        });
      }

      // Set the data source for the CollectionView to a
      // sorted collection of notes.
      collectionView.ItemsSource = notes
          .OrderBy(d => d.Date)
          .ToList();
    }

    async void OnAddClicked(object sender, EventArgs e)
    {
      // Navigate to the NoteEntryPage, without passing any data.
      await Shell.Current.GoToAsync(nameof(NoteEntryPage));
    }

    async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (e.CurrentSelection != null)
      {
        // Navigate to the NoteEntryPage, passing the filename as a query parameter.
        Notes notes = (Notes)e.CurrentSelection.FirstOrDefault();
        await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={notes.Filename}");
      }
    }
  }
}
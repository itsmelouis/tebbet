using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System.Collections.Generic;
using System.IO;
using Tebbet.ViewModels;

namespace Tebbet.Controls
{
    public partial class AdminControl : UserControl
    {
        private AdminViewModel viewModel;
        private string? filePath { get; set; }
        public AdminControl()
        {
            InitializeComponent();
            viewModel = new AdminViewModel();
            DataContext = viewModel;
            AddCircuit.Click += Handler;
            SaveCircuit.Click += Handler;
            CancelCircuit.Click += Handler;
            ImageCircuit.Click += Handler;
        }

        private void Handler(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                switch (button.Name)
                {
                    case "AddCircuit":
                        viewModel.ModalAddCircuit = true;
                        break;
                    case "CancelCircuit":
                        viewModel.ModalAddCircuit = false;
                        break;
                    case "ImageCircuit":
                        OpenFile();
                        break;
                    case "SaveCircuit":
                        CheckForm();
                        break;
                }
            }
        }

        private void CheckForm()
        {
            string Name = NameCircuit.Text;
            string Place = PlaceCircuit.Text;
            string City = CityCircuit.Text;
            string Country = CountryCircuit.Text;
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Place) && !string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Country) && !string.IsNullOrEmpty(filePath))
            {
                viewModel.AddCircuit(Name, filePath, Place, City, Country);
            }
            else
            {
                // Ajouter une liste d'erreurs.
            }
        }

        private async void OpenFile()
        {
            var topLevel = TopLevel.GetTopLevel(this);
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Sélectionner une image",
                FileTypeFilter = new[] { FilePickerFileTypes.ImageAll },
                AllowMultiple = false
            });

            if (files.Count >= 1)
            {
                filePath = files[0].Path.AbsolutePath;
            }

        }
    }
}

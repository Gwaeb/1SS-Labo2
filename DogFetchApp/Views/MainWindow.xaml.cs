using ApiHelper;
using ApiHelper.Models;
using DogFetchApp.Commands;
using DogFetchApp.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DogFetchApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel currentViewModel;
        App app;

        public DogPicturesModel dogPictures { get; set; }
        int imgIndex;

        public MainWindow(App app)
        {
            InitializeComponent();
            ApiHelper.ApiHelper.InitializeClient();
            currentViewModel = new MainViewModel();
            DataContext = currentViewModel;

            this.app = app;

            currentViewModel.RestartCommand = new DelegateCommand<string>(Restart);
        }

        private async Task LoadBreedList()
        {
            var dogBreeds = await DogApiProcessor.LoadBreedList();
            var breeds = dogBreeds.breeds;
            foreach (var breed in breeds)
            {
                if (breed.Value.Count == 0)
                {
                    BreedsListCB.Items.Add(breed.Key);
                }
                else
                {
                    //var a = breed.Value;
                    for (int i = 0; i < breed.Value.Count; i++)
                    {
                        BreedsListCB.Items.Add(breed.Key + "/" + breed.Value[i]);
                    }
                }
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BreedsListCB.Items.Add("ALL");
            await LoadBreedList();
            Previous.IsEnabled = false;
            Next.IsEnabled = false;
        }

        private void Restart(string obj)
        {
            var result = MessageBox.Show("Pour appliquer les changements, il faut redémarrer l'application.", "Message", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                app.Restart();
            }
        }

        private async void Fetch_Click(object sender, RoutedEventArgs e)
        {
            imgIndex = 0;
            string nbrOfPictures = NbrOfPictures.Text;
            string breed = BreedsListCB.Text;
            if (nbrOfPictures != "" && breed != "")
            {
                dogPictures = await DogApiProcessor.GetImageUrl(breed, nbrOfPictures);
                var r = dogPictures.Pictures[imgIndex];
                ImageGoesHere.Source = new BitmapImage(new Uri(r));
                if(nbrOfPictures != "1")
                {
                    Next.IsEnabled = true;
                }
                if (nbrOfPictures == "1")
                {
                    Next.IsEnabled = false;
                    Previous.IsEnabled = false;
                }

            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            imgIndex--;
            var r = dogPictures.Pictures[imgIndex];
            ImageGoesHere.Source = new BitmapImage(new Uri(r));
            Next.IsEnabled = true;
            if (imgIndex == 0)
            {
                Previous.IsEnabled = false;
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            imgIndex++;
            var r = dogPictures.Pictures[imgIndex];
            ImageGoesHere.Source = new BitmapImage(new Uri(r));
            Previous.IsEnabled = true;
            if (imgIndex == dogPictures.Pictures.Count - 1)
            {
                Next.IsEnabled = false;
            }
        }
    }
}

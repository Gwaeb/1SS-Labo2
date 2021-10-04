using ApiHelper.Models;
using DogFetchApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;


namespace DogFetchApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public DelegateCommand<string> RestartCommand { get; set; }

        public DelegateCommand<string> ChangeLanguageCommand { get; set; }

        public MainViewModel()
        {
            ChangeLanguageCommand = new DelegateCommand<string>(ChangeLanguage);
        }

        private void ChangeLanguage(string param)
        {
            Properties.Settings.Default.Language = param;
            Properties.Settings.Default.Save();

            RestartCommand?.Execute("");
        }
    }
}

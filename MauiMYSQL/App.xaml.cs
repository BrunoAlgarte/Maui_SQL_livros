﻿namespace MauiMYSQL
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ListaLivros();
        }
    }
}

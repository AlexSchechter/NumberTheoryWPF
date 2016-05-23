﻿using NumberTheoryWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace NumberTheoryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Analysis fAnalysis;
        MethodTimerWrapper<ulong> fWrapper;

        public MainWindow()
        {
            InitializeComponent();
            fAnalysis = new Analysis();

            fWrapper = new MethodTimerWrapper<ulong>();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ulong userInput; 
            if (UInt64.TryParse(UserInput.Text, out userInput))
            {

                if ((bool)isPrime.IsChecked)
                {
                    MethodTimerWrapper<bool> isPrimeWrapper = new MethodTimerWrapper<bool>(fAnalysis.IsPrime, userInput);
                    isPrimeWrapper.ExecuteMethod();
                    DataContext = isPrimeWrapper;
                }
                else if((bool)primeFactors.IsChecked)
                {
                    MethodTimerWrapper<List<ulong>> primeFactorsWraper = new MethodTimerWrapper<List<ulong>>(fAnalysis.CalculatePrimeFactors, userInput);
                    primeFactorsWraper.ExecuteMethod();
                    DataContext = primeFactorsWraper;
                }
                else
                {

                }
                fWrapper = new MethodTimerWrapper<ulong>(fAnalysis.CalculateLargestPrime, userInput);
                fWrapper.ExecuteMethod();
                DataContext = fWrapper;
            }
                     
        }
    }
}

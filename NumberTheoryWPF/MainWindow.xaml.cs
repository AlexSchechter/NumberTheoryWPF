using NumberTheoryWPF;
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
        MultiThreadingAnalysis fAnalysisParallel;
        MethodTimerWrapper<ulong> fWrapper;
        
        public MainWindow()
        {
            InitializeComponent();
            fAnalysis = new Analysis();
            fAnalysisParallel = new MultiThreadingAnalysis();
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
                    MethodTimerWrapper<List<ulong>> primeFactorsWrapper = new MethodTimerWrapper<List<ulong>>(fAnalysis.CalculatePrimeFactors, userInput);
                    primeFactorsWrapper.ExecuteMethod();
                    DataContext = primeFactorsWrapper;
                }
                else if((bool)largestPrime.IsChecked)
                {
                    MethodTimerWrapper<ulong> largestPrimeWrapper = new MethodTimerWrapper<ulong>(fAnalysis.CalculateLargestPrime, userInput);
                    largestPrimeWrapper.ExecuteMethod();
                    DataContext = largestPrimeWrapper;
                    MethodTimerWrapper<ulong> largestPrimeWrapperParallel = new MethodTimerWrapper<ulong>(fAnalysisParallel.CalculateLargestPrime, userInput);
                    largestPrimeWrapperParallel.ExecuteMethod();
                }            
            }                     
        }
    }
}

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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_App_LabWork3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool canExecute_Convert()
        {
            int inputBase = Convert.ToInt32(inputNumberBaseTextBox.Text);
            int outputBase = Convert.ToInt32(outputNumberBaseTextBox.Text);
            if (inputBase >= 2 && inputBase <= 36 && outputBase >= 2 && outputBase <= 36)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void execute_ConvertInputNumber(object sender, RoutedEventArgs e)
        {
            if (canExecute_Convert() == true)
            {
                NumberSystemsConvertor numConv = new NumberSystemsConvertor();

                outputNumberTextBox.Text = numConv.convertNumber(
                    Convert.ToByte(inputNumberBaseTextBox.Text),
                    Convert.ToByte(outputNumberBaseTextBox.Text),
                    inputNumberTextBox.Text
                    );

                decimalNumberTextBox.Text = Convert.ToString(numConv.getInputDecNumber());
            }
            else
            {
                MessageBox.Show("Wrong base number! Input range = [2 ; 36]");
            }
        }

        void execute_Clear(object sender, RoutedEventArgs e)
        {
            inputNumberBaseTextBox.Text = outputNumberBaseTextBox.Text
                = inputNumberTextBox.Text = decimalNumberTextBox.Text
                = outputNumberTextBox.Text = "";
        }
    }
}
internal class NumberSystemsConvertor
{
    // основа заданої та шуканої системи числення
    private byte inputBase, outputBase;
    // задане число у десятковій системі числення
    private long inputNumber;
    // задане число у шуканій системі числення
    private string outputNumber = "";

    public string convertNumber(int inputBase, int outputBase, string inputNumber)
    {
        setInputBase(inputBase);
        setOutputBase(outputBase);
        setInputNumber(inputNumber);
        convertOutputNumber();
        return outputNumber;
    }

    public void setInputBase(int inputBase)
    {
        if (inputBase >= 2 && inputBase <= 36)
        {
            this.inputBase = (byte)inputBase;
        }
        else
        {
            this.inputBase = 36;
        }
    }

    public void setOutputBase(int outputBase)
    {
        if (outputBase >= 2 && outputBase <= 36)
        {
            this.outputBase = (byte)outputBase;
        }
        else
        {
            this.outputBase = 36;
        }
    }

    public void setInputNumber(string enterNumber)
    {
        convertDecInputNumber(enterNumber);
    }

    public byte getInputBase()
    {
        return this.inputBase;
    }

    public byte getOutputBase()
    {
        return this.outputBase;
    }

    public long getInputDecNumber()
    {
        return inputNumber;
    }

    public string getOutputNumber()
    {
        convertOutputNumber();
        return outputNumber;
    }

    // переведення до десяткової системи числення
    void convertDecInputNumber(string enterNumber)
    {
        inputNumber = 0;

        int tmp;
        if (inputBase < 10)
        {
            for (int i = 0; i < enterNumber.Length; i++)
            {
                // основа в степені порядку
                tmp = 1;
                for (int j = 0; j < enterNumber.Length - i - 1; j++)
                {
                    tmp *= inputBase;
                }

                inputNumber += (enterNumber[i] - 48) * tmp;
            }
        }
        else
        {
            for (int i = 0; i < enterNumber.Length; i++)
            {
                // основа в степені порядку
                tmp = 1;
                for (int j = 0; j < enterNumber.Length - i - 1; j++)
                {
                    tmp *= inputBase;
                }

                // цифра від 0 до 9
                if (enterNumber[i] >= 48 && enterNumber[i] <= 57)
                {
                    inputNumber += (enterNumber[i] - 48) * tmp;
                }
                // малі літери латиниці
                else if (enterNumber[i] >= 97 && enterNumber[i] <= 122)
                {
                    inputNumber += (enterNumber[i] - 87) * tmp;
                }
                // великі літери латиниці
                else
                {
                    inputNumber += (enterNumber[i] - 55) * tmp;
                }
            }
        }
    }

    // переведення до заданої системи числення
    void convertOutputNumber()
    {
        long numTmp = inputNumber;
        short counter = 1;

        while (numTmp / outputBase != 0)
        {
            counter++;
            numTmp /= outputBase;
        }
        char[] strTmp = new char[counter];

        numTmp = inputNumber;

        long tmpLong;
        for (int i = 0; i < counter; i++)
        {
            tmpLong = numTmp % outputBase;
            numTmp /= outputBase;

            // якщо остача від ділення - число від 0 до 9
            if (tmpLong >= 0 && tmpLong <= 9)
            {
                strTmp[counter - i - 1] = (char)(48 + tmpLong);
            }
            // якщо остача від ділення більша за 10
            else
            {
                strTmp[counter - i - 1] = (char)(65 + tmpLong - 10);
            }
        }

        outputNumber = new string(strTmp);
    }

}
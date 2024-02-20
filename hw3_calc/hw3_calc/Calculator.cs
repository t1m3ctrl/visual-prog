using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Data;
using System.Data;

namespace Calculator;

public class CalculatorViewModel : INotifyPropertyChanged
{
    private string _displayValue = "0";
    private string _historyValue = "";

    private double currentValue = 0;
    private double previousValue = 0;
    private string lastOperation = string.Empty;
    private bool typingNextValue = false;

    public string DisplayValue
    {
        get { return _displayValue; }
        set { _ = SetField(ref _displayValue, value); }
    }
    public string HistoryValue
    {
        get { return _historyValue; }
        set { _ = SetField(ref _historyValue, value); }
    }

    public void ButtonClickedCommand(object sender)
    {
        if (sender is string text)
        {
            if ("0123456789,".Contains(text))
            {
                AppendToInput(text);
            }
            else if ("+-/x^mod".Contains(text))
            {
                if (!string.IsNullOrEmpty(DisplayValue)) 
                {
                    if (typingNextValue) PerformOperation();

                    lastOperation = text;
                    previousValue = currentValue;
                    HistoryValue = $"{previousValue} {lastOperation} ";
                    currentValue = 0;
                    DisplayValue = "0";
                    typingNextValue = true;
                }
            }
            else if ("factlnlgfloorceilsincostan".Contains(text))
            {
                if (!string.IsNullOrEmpty(DisplayValue))
                {
                    if (typingNextValue) PerformOperation();

                    lastOperation = text;
                    previousValue = currentValue;
                    HistoryValue = $"{lastOperation}({previousValue})";
                    PerformOperation();
                    currentValue = 0;
                }
            }
            else if (text == "=")
            {
                PerformOperation();
            }
            else if (text == "clr")
            {
                if (DisplayValue.Length > 0 && DisplayValue != "0")
                {
                    DisplayValue = DisplayValue.Substring(0, DisplayValue.Length - 1);
                    HistoryValue = HistoryValue.Substring(0, HistoryValue.Length - 1);
                }
            }
            else if (text == "C")
            {
                lastOperation = string.Empty;
                HistoryValue = string.Empty;
                currentValue = 0;
                previousValue = 0;
                typingNextValue = false;
                DisplayValue = "0";
            }
            else if (text == "CE")
            {
                currentValue = 0;
                DisplayValue = "0";
            }

        }
    }

    private void AppendToInput(string value)
    {
        if (DisplayValue == "0" && value != ",")
        {
            DisplayValue = value;
            if (!typingNextValue)
            {
                HistoryValue = value;
            }
            else HistoryValue = $"{previousValue} {lastOperation} {value}";
        }
        else if (value  == "," && !DisplayValue.Contains(","))
        { 
            DisplayValue += value;
            HistoryValue += value;
        }
        else if (value != ",")
        {
            DisplayValue += value;
            HistoryValue += value;
        }
        currentValue = double.Parse(DisplayValue);
    }

    private void PerformOperation()
    {
        
        switch (lastOperation)
        {
            case "+":
                HistoryValue = $"{previousValue} {lastOperation} {currentValue} =";
                previousValue += currentValue;
                break;
            case "-":
                HistoryValue = $"{previousValue} {lastOperation} {currentValue} =";
                previousValue -= currentValue;
                break;
            case "x":
                HistoryValue = $"{previousValue} {lastOperation} {currentValue} =";
                previousValue *= currentValue;
                break;
            case "/":
                HistoryValue = $"{previousValue} {lastOperation} {currentValue} =";
                if (currentValue != 0)
                    previousValue /= currentValue;
                else
                {
                    DisplayValue = "Cannot divide by zero!";
                    previousValue = currentValue;
                }
                break;
            case "^":
                HistoryValue = $"{previousValue} {lastOperation} {currentValue} =";
                previousValue = Math.Pow(previousValue, currentValue);
                break;
            case "fact":
                HistoryValue = $" {lastOperation}({previousValue}) =";
                int fact = 1;
                for (int i = 1; i <= currentValue; i++)
                {
                    fact *= i;
                }
                previousValue = fact;
                break;
            case "ln":
                HistoryValue = $" {lastOperation}({previousValue}) =";
                previousValue = Math.Log(currentValue);
                break;
            case "lg":
                HistoryValue = $" {lastOperation}({previousValue}) =";
                previousValue = Math.Log10(currentValue); 
                break;
            case "floor":
                HistoryValue = $" {lastOperation}({previousValue}) =";
                previousValue = Math.Floor(currentValue);
                break;
            case "ceil":
                HistoryValue = $" {lastOperation}({previousValue}) =";
                previousValue = Math.Ceiling(currentValue);
                break;
            case "mod":
                HistoryValue = $"{previousValue} {lastOperation} {currentValue} =";
                previousValue = (previousValue % currentValue) * Math.Sign(previousValue);
                break;
            case "cos":
                HistoryValue = $" {lastOperation}({previousValue}) =";
                previousValue = Math.Cos(currentValue);
                break;
            case "sin":
                HistoryValue = $" {lastOperation}({previousValue}) =";
                previousValue = Math.Sin(currentValue);
                break;
            case "tan":
                HistoryValue = $" {lastOperation}({previousValue}) =";
                previousValue = Math.Tan(currentValue);     
                break;
            default:
                previousValue = currentValue;
                break;
        }
        currentValue = previousValue;
        DisplayValue = currentValue.ToString();
        typingNextValue = false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

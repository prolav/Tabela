using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tabela.View_PC.Cards_List;

public partial class FaseCard : ContentView
{
    public static readonly BindableProperty EditarFaseCommandProperty =
        BindableProperty.Create(
            nameof(EditarFaseCommand),
            typeof(ICommand),
            typeof(FaseCard),
            defaultBindingMode: BindingMode.OneWay);

    public ICommand EditarFaseCommand
    {
        get => (ICommand)GetValue(EditarFaseCommandProperty);
        set => SetValue(EditarFaseCommandProperty, value);
    }

    public static readonly BindableProperty ExcluirFaseCommandProperty =
        BindableProperty.Create(
            nameof(ExcluirFaseCommand),
            typeof(ICommand),
            typeof(FaseCard),
            defaultBindingMode: BindingMode.OneWay);

    public ICommand ExcluirFaseCommand
    {
        get => (ICommand)GetValue(ExcluirFaseCommandProperty);
        set => SetValue(ExcluirFaseCommandProperty, value);
    }
    public FaseCard()
    {
        InitializeComponent();
    }
}
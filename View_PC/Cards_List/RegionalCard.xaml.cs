using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tabela.View_PC.Cards_List;

public partial class RegionalCard : ContentView
{
    public static readonly BindableProperty EditarRegionalCommandProperty =
        BindableProperty.Create(
            nameof(EditarRegionalCommand),
            typeof(ICommand),
            typeof(RegionalCard),
            defaultBindingMode: BindingMode.OneWay);

    public ICommand EditarRegionalCommand
    {
        get => (ICommand)GetValue(EditarRegionalCommandProperty);
        set => SetValue(EditarRegionalCommandProperty, value);
    }

    public static readonly BindableProperty ExcluirRegionalCommandProperty =
        BindableProperty.Create(
            nameof(ExcluirRegionalCommand),
            typeof(ICommand),
            typeof(RegionalCard),
            defaultBindingMode: BindingMode.OneWay);

    public ICommand ExcluirRegionalCommand
    {
        get => (ICommand)GetValue(ExcluirRegionalCommandProperty);
        set => SetValue(ExcluirRegionalCommandProperty, value);
    }
    public RegionalCard()
    {
        InitializeComponent();
    }
}
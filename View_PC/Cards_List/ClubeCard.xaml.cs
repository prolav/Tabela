using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tabela.Models;

namespace Tabela.View_PC.Cards_List;

public partial class ClubeCard : ContentView
{
    public static readonly BindableProperty EditarClubeCommandProperty =
        BindableProperty.Create(
            nameof(EditarClubeCommand),
            typeof(ICommand),
            typeof(ClubeCard),
            defaultBindingMode: BindingMode.OneWay);

    public ICommand EditarClubeCommand
    {
        get => (ICommand)GetValue(EditarClubeCommandProperty);
        set => SetValue(EditarClubeCommandProperty, value);
    }

    public static readonly BindableProperty ExcluirClubeCommandProperty =
        BindableProperty.Create(
            nameof(ExcluirClubeCommand),
            typeof(ICommand),
            typeof(ClubeCard),
            defaultBindingMode: BindingMode.OneWay);

    public ICommand ExcluirClubeCommand
    {
        get => (ICommand)GetValue(ExcluirClubeCommandProperty);
        set => SetValue(ExcluirClubeCommandProperty, value);
    }

    public ClubeCard()
    {
        InitializeComponent();
    }
}
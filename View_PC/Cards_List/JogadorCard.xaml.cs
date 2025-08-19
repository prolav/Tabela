using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tabela.View_PC.Cards_List;

public partial class JogadorCard : ContentView
{
    public static readonly BindableProperty EditarJogadorCommandProperty =
        BindableProperty.Create(
            nameof(EditarJogadorCommand),
            typeof(ICommand),
            typeof(FaseCard),
            defaultBindingMode: BindingMode.OneWay);

    public ICommand EditarJogadorCommand
    {
        get => (ICommand)GetValue(EditarJogadorCommandProperty);
        set => SetValue(EditarJogadorCommandProperty, value);
    }

    public static readonly BindableProperty ExcluirJogadorCommandProperty =
        BindableProperty.Create(
            nameof(ExcluirJogadorCommand),
            typeof(ICommand),
            typeof(FaseCard),
            defaultBindingMode: BindingMode.OneWay);

    public ICommand ExcluirJogadorCommand
    {
        get => (ICommand)GetValue(ExcluirJogadorCommandProperty);
        set => SetValue(ExcluirJogadorCommandProperty, value);
    }

    public JogadorCard()
    {
        InitializeComponent();
    }
}
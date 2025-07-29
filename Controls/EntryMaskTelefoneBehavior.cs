namespace Tabela.Controls;

public class EntryMaskTelefoneBehavior : Behavior<Entry>
{
    public string Mascara { get; set; }

    private Entry _entry;

    protected override void OnAttachedTo(Entry bindable)
    {
        _entry = bindable;
        bindable.TextChanged += OnTextChanged;
        base.OnAttachedTo(bindable);
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        bindable.TextChanged -= OnTextChanged;
        base.OnDetachingFrom(bindable);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Mascara))
            return;

        var textoSemMascara = new string(e.NewTextValue?.Where(char.IsDigit).ToArray());
        var novoTexto = "";
        int indice = 0;

        foreach (var ch in Mascara)
        {
            if (ch == '#')
            {
                if (indice < textoSemMascara.Length)
                    novoTexto += textoSemMascara[indice++];
                else
                    break;
            }
            else
            {
                novoTexto += ch;
            }
        }

        if (_entry.Text != novoTexto)
            _entry.Text = novoTexto;
    }
}

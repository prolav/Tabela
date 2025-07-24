using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tabela;
    /// <summary>
    /// Classe base para todas as ViewModels, com suporte a PropertyChanged e métodos auxiliares.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
    // Evento de notificação de mudança de propriedade
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Método que dispara o evento de mudança de propriedade.
    /// </summary>
    /// <param name="propertyName">Nome da propriedade (preenchido automaticamente).</param>
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Define o valor de uma propriedade e dispara a notificação se houver mudança.
    /// </summary>
    /// <typeparam name="T">Tipo da propriedade.</typeparam>
    /// <param name="field">Campo de backing (ref).</param>
    /// <param name="value">Novo valor.</param>
    /// <param name="propertyName">Nome da propriedade (preenchido automaticamente).</param>
    /// <returns>true se o valor foi alterado; false caso contrário.</returns>
    protected bool SetProperty<T>(
        ref T field,
        T value,
        [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    /// <summary>
    /// Indica se a ViewModel está executando uma tarefa.
    /// Pode ser usada para mostrar carregamento.
    /// </summary>
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    /// <summary>
    /// Título genérico da página, útil para bind em cabeçalhos.
    /// </summary>
    private string _title;
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}

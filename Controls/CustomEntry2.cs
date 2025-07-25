using Microsoft.Maui.Controls.Shapes;

namespace Tabela.Controls
{
public class CustomEntry2:ContentView
{
        private readonly Entry _entry;
        private readonly Border _border;

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEntry2), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(string), typeof(CustomEntry2), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomEntry2), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomEntry2), string.Empty);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomEntry2), Colors.Gray);

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(CustomEntry2), 8f);

        public static readonly BindableProperty StrokeThicknessProperty =
            BindableProperty.Create(nameof(StrokeThickness), typeof(float), typeof(CustomEntry2), 1f);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string TextColor
        {
            get => (string)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public float StrokeThickness
        {
            get => (float)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public CustomEntry2()
        {
            _entry = new Entry
            {
                BackgroundColor = Color.FromArgb("#F5D5C6"),
                TextColor = Color.FromArgb("#D12D43"),
                FontSize = 24,
                Margin = new Thickness(10, 5)
            };
            
            _entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), source: this, mode: BindingMode.TwoWay));
            _entry.SetBinding(Entry.TextColorProperty, new Binding(nameof(TextColor), source: this, mode: BindingMode.TwoWay));
            _entry.SetBinding(Entry.FontSizeProperty, new Binding(nameof(FontSize), source: this, mode: BindingMode.TwoWay));
            _entry.SetBinding(Entry.PlaceholderProperty, new Binding(nameof(Placeholder), source: this));

            _border = new Border
            {
                Padding = 0,
                BackgroundColor = Colors.White,
                Content = _entry
            };

            _border.SetBinding(Border.StrokeProperty, new Binding(nameof(BorderColor), source: this));
            _border.SetBinding(Border.StrokeThicknessProperty, new Binding(nameof(StrokeThickness), source: this));
            _border.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(CornerRadius) };

            Content = _border;
        }
    }
}

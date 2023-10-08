namespace Viruses;

using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

/// <summary>
/// Логика взаимодействия для ModalDialog.xaml
/// </summary>
public partial class ModalDialogChangeValue
{
	public enum EType
	{
		IREPRODUCTIBLE,
		HITMAN
	}

	public string Result => ValueInput.Text;

	[GeneratedRegex("[0-9]+")]
	private static partial Regex NumberRegex();

	public ModalDialogChangeValue(EType type)
	{
		InitializeComponent();

		switch (type)
		{
			case EType.IREPRODUCTIBLE:
			{
				ValuePlaceholder.Content = "Reproduction speed";
				OKButton.Click += OKButton_Click2;
				ValueInput.PreviewTextInput += ValueInput_PreviewTextInput;

			}
			break;

			case EType.HITMAN:
			{
				ValuePlaceholder.Content = "Victim name";
				OKButton.Click += OKButton_Click1;
			}
			break;
		}
	}

	public new bool? Show()
	{
		return ShowDialog();
	}

	private void OKButton_Click1(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(ValueInput.Text))
		{
			MessageBox.Show(this, "Fill missing fields first", "Missing fields", MessageBoxButton.OK, MessageBoxImage.Warning);
			return;
		}

		DialogResult = true;
	}

	private void OKButton_Click2(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(ValueInput.Text) || int.Parse(ValueInput.Text) == 0)
		{
			MessageBox.Show(this, "Value must be greater than 0", "Missing fields", MessageBoxButton.OK, MessageBoxImage.Warning);
			return;
		}

		DialogResult = true;
	}

	private void CancelButton_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = false;
	}

	private void ValueInput_GotFocus(object sender, RoutedEventArgs e)
	{
		ValuePlaceholder.Visibility = Visibility.Hidden;
	}

	private void ValueInput_LostFocus(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(ValueInput.Text))
			ValuePlaceholder.Visibility = Visibility.Visible;
	}

	private static void ValueInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		bool is_match = NumberRegex().IsMatch(e.Text);
		if (is_match)
		{
			int result = int.Parse(e.Text);
			e.Handled = !(is_match && result < 1000000);
			return;
		}

		e.Handled = true;
	}
}

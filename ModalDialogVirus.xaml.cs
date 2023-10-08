
namespace Viruses;

using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

/// <summary>
/// Логика взаимодействия для ModalDialog.xaml
/// </summary>
public partial class ModalDialogVirus
{
	public string ObjectName => ValueInput.Text;

	public int ReproductionSpeed => int.Parse(ReproductionSpeedInput.Text);

	[GeneratedRegex("[0-9]+")]
	private static partial Regex NumberRegex();

	public ModalDialogVirus(Type microorganism_type)
	{
		InitializeComponent();

		var attribute = microorganism_type.GetProperty("ReproductionSpeed")?.GetCustomAttribute<DefaultValueAttribute>();
		if (attribute == null) return;

		ReproductionSpeedInput.Text = attribute.Value!.ToString();
		ReproductionSpeedPlaceholder.Visibility = Visibility.Hidden;
	}

	public new bool? Show()
	{
		return ShowDialog();
	}

	private void OKButton_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(ValueInput.Text) || string.IsNullOrWhiteSpace(ReproductionSpeedInput.Text) || int.Parse(ReproductionSpeedInput.Text) == 0)
		{
			MessageBox.Show(this, "Fill missing fields first", "Missing fields", MessageBoxButton.OK, MessageBoxImage.Warning);
			return;
		}

		DialogResult = true;
	}

	private void CancelButton_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = false;
	}

	private void NameInput_GotFocus(object sender, RoutedEventArgs e)
	{
		ValuePlaceholder.Visibility = Visibility.Hidden;
	}

	private void NameInput_LostFocus(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(ValueInput.Text))
			ValuePlaceholder.Visibility = Visibility.Visible;
	}

	private void ReproductionSpeedInput_GotFocus(object sender, RoutedEventArgs e)
	{
		ReproductionSpeedPlaceholder.Visibility = Visibility.Hidden;
	}

	private void ReproductionSpeedInput_LostFocus(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(ReproductionSpeedInput.Text))
			ReproductionSpeedPlaceholder.Visibility = Visibility.Visible;
	}

	private void ReproductionSpeedInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
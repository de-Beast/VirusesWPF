namespace Viruses;

using System.Windows;

/// <summary>
/// Логика взаимодействия для ModalDialog.xaml
/// </summary>
public partial class ModalDialog : Window
{
	protected ModalDialog()
	{
		InitializeComponent();
	}

	public new bool? Show()
	{
		return ShowDialog();
	}

	private void OKButton_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(ValueInput.Text))
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
}

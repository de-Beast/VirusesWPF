namespace Viruses;
using System.Windows;

/// <summary>
/// Логика взаимодействия для ModalDialog.xaml
/// </summary>
public partial class ModalDialogHitman : Window
{
	public string ObjectName => ValueInput.Text;

	public string VictimName => VictimNameInput.Text;

	public ModalDialogHitman()
	{
		InitializeComponent();
	}

	public new bool? Show()
	{
		return ShowDialog();
	}

	private void OKButton_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(ValueInput.Text) || string.IsNullOrWhiteSpace(VictimNameInput.Text))
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

	private void VictimNameInput_GotFocus(object sender, RoutedEventArgs e)
	{
		VictimNamePlaceholder.Visibility = Visibility.Hidden;
	}

	private void VictimNameInput_LostFocus(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(VictimNameInput.Text))
			VictimNamePlaceholder.Visibility = Visibility.Visible;
	}
}

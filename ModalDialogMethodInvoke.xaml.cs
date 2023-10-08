using System;
namespace Viruses;

using System.Collections.Generic;
using System.Windows;

/// <summary>
/// Логика взаимодействия для ModalDialog.xaml
/// </summary>
public partial class ModalDialogMethodInvoke
{
	public string? Result { get; private set; }

	private List<Func<string>> Delegates { get; init; }

	public ModalDialogMethodInvoke(List<Func<string>> delegates)
	{
		InitializeComponent();

		Delegates = delegates;

		foreach (var func in delegates)
		{
			MethodComboBox.Items.Add(func.Method.Name);
		}
	}

	public new bool? Show()
	{
		return ShowDialog();
	}

	private void OKButton_Click(object sender, RoutedEventArgs e)
	{
		if (MethodComboBox.SelectedIndex == -1)
		{
			MessageBox.Show(this, "Choose method to invoke first", "Missing method", MessageBoxButton.OK, MessageBoxImage.Warning);
			return;
		}

		Result = Delegates[MethodComboBox.SelectedIndex].Invoke();

		DialogResult = true;
	}

	private void CancelButton_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = false;
	}

	private void MethodComboBox_SelectionChanged(object sender, RoutedEventArgs e)
	{
		MethodPlaceholder.Visibility = Visibility.Hidden;
	}
}

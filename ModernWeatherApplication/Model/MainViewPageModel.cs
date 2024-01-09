using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Controls;

namespace ModernWeatherApplication.Model;

public partial class MainViewPageModel : ObservableObject, INavigationAware
{
    [RelayCommand]
    private void DocButton()
    {
        var target = "https://github.com/myot233/ModernWeatherApplication/blob/master/ModernWeatherApplication/doc.MD";
        try
        {
            System.Diagnostics.Process.Start("explorer.exe", target);

        }
        catch (System.ComponentModel.Win32Exception noBrowser)
        {
            if (noBrowser.ErrorCode == -2147467259)
                System.Windows.MessageBox.Show(noBrowser.Message);
        }
        catch (Exception other)
        {
            System.Windows.MessageBox.Show(other.Message);
        }
    }

    [RelayCommand]
    private void SourceCodeButton()
    {
        var target = "https://github.com/myot233/ModernWeatherApplication";
        try
        {
            System.Diagnostics.Process.Start("explorer.exe",target);

        }
        catch (System.ComponentModel.Win32Exception noBrowser)
        {
            if (noBrowser.ErrorCode == -2147467259)
                System.Windows.MessageBox.Show(noBrowser.Message);
        }
        catch (Exception other)
        {
            System.Windows.MessageBox.Show(other.Message);
        }
    }
        
    public void OnNavigatedTo()
    {
        //throw new NotImplementedException();
    }

    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }
}
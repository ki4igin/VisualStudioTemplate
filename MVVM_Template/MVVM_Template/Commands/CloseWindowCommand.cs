using MVVM_Template.Commands.Base;
using System.Windows;

namespace MVVM_Template.Commands;

public class CloseWindowCommand : CommandBase
{
    public override bool CanExecute(object? parameter) =>
        parameter is Window;
    public override void Execute(object? parameter) =>
        (parameter as Window)?.Close();
}

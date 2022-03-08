using MVVM_Template.Commands.Base;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_Template.ViewModels;

public class MainViewModel : TitledViewModel
{

    #region NotifyProperty<int> Counter
    private int _counter;
    public int Counter { get => _counter; set => Set(ref _counter, value); }
    #endregion


    #region NotifyProperty<double> ProgressValue
    private double _progressValue;
    public double ProgressValue { get => _progressValue; set => Set(ref _progressValue, value); }
    #endregion



    private SimpleCommand<string>? _testCommand;
    public SimpleCommand<string> TestCommand => _testCommand ??= new(
        execute: async (val) =>
        {
            Counter++;
           await Task.Delay(1000);
        },
        canExecute: () => Counter % 2 == 0
        );

    private SimpleCommand? _testCommand1;
    public SimpleCommand TestCommand1 =>
        _testCommand1 ??= new SimpleCommand(
            execute: () => Counter++
        );

    //CancellationTokenSource _cts;

    private ICommand? _testCommand2;
    public ICommand TestCommand2 =>
        _testCommand2 ??= new CommandAsync(
            execute: async (progress, cts) =>
            {
                //_cts = new();                
                //var progress = new Progress<double>(p => ProgressValue = p);
                //try
                //{
                await OperationAsync(10);
                //await OperationAsync(10, progress, cts);
                //}
                //catch (OperationCanceledException)
                //{
                //    ProgressValue = 0;
                //}

            }
        );

    //private CommandBase? _testCommand3;
    //public CommandBase TestCommand3 =>
    //    _testCommand3 ??= new CommandBase(
    //        execute: () => _cts.Cancel()
    //    );


    public async Task OperationAsync(
        int secodnd,
        IProgress<double> progress = null,
        CancellationToken cancellationToken = default
        )
    {
        for (int i = 0; i < secodnd; i++)
        {
            progress?.Report((double)i / secodnd);
            await Task.Delay(1000, cancellationToken);

        }
    }

    public MainViewModel()
    {
        Title = "Окно";
    }
}

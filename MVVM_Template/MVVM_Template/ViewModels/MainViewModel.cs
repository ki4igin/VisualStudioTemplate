using MVVM_Template.Commands.Base;
using MVVM_Template.ViewModels.Base;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_Template.ViewModels;

public class MainViewModel : TitledViewModel
{
    #region NotifyProperty <int> Counter
    private int _counter;
    public int Counter
    {
        get => _counter;
        set
        {
            if (Set(ref _counter, value))
                ProgressBarEnable = (value % 2) == 0;
        }
    }
    #endregion

    #region NotifyProperty <double> ProgressValue
    private double _progressValue;
    public double ProgressValue { get => _progressValue; set => Set(ref _progressValue, value); }
    #endregion


    #region NotifyProperty <bool> ProgressBarEnable
    private bool _progressBarEnable;
    public bool ProgressBarEnable { get => _progressBarEnable; set => Set(ref _progressBarEnable, value); }
    #endregion



    //CancellationTokenSource _cts;
    
    private CommandAsync? _testCommand2;
    public CommandAsync TestCommand2 => _testCommand2 ??= new CommandAsync(
        execute: async (progress, cts) => await OperationAsync(10, progress, cts)
    );

    public async Task OperationAsync(
        int secodnd,
        IProgress<double>? progress = null,
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

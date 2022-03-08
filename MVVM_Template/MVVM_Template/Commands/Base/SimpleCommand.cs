using System;
using System.ComponentModel;

namespace MVVM_Template.Commands.Base;
public class SimpleCommand : CommandBase
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool> _canExecute;

    public SimpleCommand(Action execute) :
        this(execute, () => true)
    { }
    public SimpleCommand(Action<object?> execute) :
        this(execute, () => true)
    { }
    public SimpleCommand(Action execute, Func<bool> canExecute) :
        this((obj) => execute(), (obj) => canExecute())
    { }
    public SimpleCommand(Action<object?> execute, Func<bool> canExecute) :
        this(execute, (obj) => canExecute())
    { }
    public SimpleCommand(Action execute, Func<object?, bool> canExecute) :
        this((obj) => execute(), canExecute)
    { }
    public SimpleCommand(Action<object?> execute, Func<object?, bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public override bool CanExecute(object? parameter) =>
        _canExecute(parameter);

    public override void Execute(object? parameter)
    {
        if (CanExecute(parameter) is false)
            return;

        _execute(parameter);
    }
}

public class SimpleCommand<T> : CommandBase
{
    private readonly Action<T> _execute;
    private readonly Func<T, bool> _canExecute;

    public SimpleCommand(Action execute) :
        this(execute, () => true)
    { }
    public SimpleCommand(Action<T> execute) :
        this(execute, () => true)
    { }
    public SimpleCommand(Action execute, Func<bool> canExecute) :
        this((obj) => execute(), (obj) => canExecute())
    { }
    public SimpleCommand(Action<T> execute, Func<bool> canExecute) :
        this(execute, (obj) => canExecute())
    { }
    public SimpleCommand(Action execute, Func<T, bool> canExecute) :
        this((obj) => execute(), canExecute)
    { }
    public SimpleCommand(Action<T> execute, Func<T, bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public override bool CanExecute(object? parameter) =>
        _canExecute(ConvertParameter(parameter));

    public override void Execute(object? parameter)
    {
        var param = ConvertParameter(parameter);
        if (CanExecute(param) is false)
            return;

        _execute(param);
    }

    private static T ConvertParameter(object? parameter)
    {
        if (parameter is null)
            throw new ArgumentNullException("В команде нет параметра");
        if (parameter is T result)
            return result;

        Type commandType = typeof(T);
        Type parameterType = parameter.GetType();

        if (commandType.IsAssignableFrom(parameterType))
            return (T)parameter;

        TypeConverter commandTypeConverter = TypeDescriptor.GetConverter(commandType);
        if (commandTypeConverter.CanConvertFrom(parameterType))
            return (T)commandTypeConverter.ConvertFrom(parameter)!;

        TypeConverter? parameter_converter = TypeDescriptor.GetConverter(parameterType);
        if (parameter_converter.CanConvertTo(commandType))
            return (T)parameter_converter.ConvertFrom(parameter)!;

        return default!;
    }
}
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using WpfSDIAppln.Models;

namespace WpfSDIAppln.ViewModels;

public class FormViewViewModel : ViewModelBase
{
    private readonly DocumentModel _document;

    private string _patientWeightText = "75.0";
    private string _targetDoseText = "1.20";
    private string _scannerCapacityText = "3";
    private string _selectedExamType = "General XRay";
    private string _resultText = string.Empty;

    public FormViewViewModel(DocumentModel document)
    {
        _document = document;

        ExamTypes = new ObservableCollection<string>
        {
            "General XRay",
            "Cardiac CT",
            "Chest MRI"
        };

        CalculateCommand = new RelayCommand(_ => RecalculatePlan());
        ResetCommand = new RelayCommand(_ => Reset());

        RecalculatePlan();
    }

    public string Title => "Protocol Planning Workbench";
    public string Subtitle => "Use fields below, then click Calculate.";

    public ObservableCollection<string> ExamTypes { get; }

    public ICommand CalculateCommand { get; }
    public ICommand ResetCommand { get; }

    public string SelectedExamType
    {
        get => _selectedExamType;
        set
        {
            if (SetProperty(ref _selectedExamType, value))
            {
                RecalculatePlan();
            }
        }
    }

    public string PatientWeightText
    {
        get => _patientWeightText;
        set => SetProperty(ref _patientWeightText, value);
    }

    public string TargetDoseText
    {
        get => _targetDoseText;
        set => SetProperty(ref _targetDoseText, value);
    }

    public string ScannerCapacityText
    {
        get => _scannerCapacityText;
        set => SetProperty(ref _scannerCapacityText, value);
    }

    public string ResultText
    {
        get => _resultText;
        set => SetProperty(ref _resultText, value);
    }

    private void Reset()
    {
        PatientWeightText = "75.0";
        TargetDoseText = "1.20";
        ScannerCapacityText = "3";
        SelectedExamType = "General XRay";
        RecalculatePlan();
    }

    private void RecalculatePlan()
    {
        var weight = ParseAndClamp(PatientWeightText, 10.0, 300.0, 75.0);
        var dose = ParseAndClamp(TargetDoseText, 0.1, 5.0, 1.2);
        var capacity = (int)Math.Round(ParseAndClamp(ScannerCapacityText, 1.0, 12.0, 3.0));

        PatientWeightText = weight.ToString("0.0", CultureInfo.InvariantCulture);
        TargetDoseText = dose.ToString("0.00", CultureInfo.InvariantCulture);
        ScannerCapacityText = capacity.ToString(CultureInfo.InvariantCulture);

        var contrastVolumeMl = weight * dose;
        var slotMinutes = (int)Math.Round(60.0 / capacity);
        var prepFactor = SelectedExamType == "Cardiac CT" ? 1.15 : SelectedExamType == "Chest MRI" ? 1.10 : 1.0;
        var prepMinutes = (int)Math.Round(slotMinutes * prepFactor);

        ResultText =
            $"Protocol Plan{Environment.NewLine}" +
            $"------------{Environment.NewLine}" +
            $"Exam Type: {SelectedExamType}{Environment.NewLine}" +
            $"Patient Weight: {weight:0.0} kg{Environment.NewLine}" +
            $"Target Dose: {dose:0.00} mL/kg{Environment.NewLine}" +
            $"Scanner Capacity: {capacity} patients/hr{Environment.NewLine}{Environment.NewLine}" +
            $"Calculated Contrast Volume: {contrastVolumeMl:0.0} mL{Environment.NewLine}" +
            $"Estimated Slot Duration: {slotMinutes} minutes{Environment.NewLine}" +
            $"Suggested Prep Buffer: {prepMinutes} minutes{Environment.NewLine}{Environment.NewLine}" +
            "Operational Note: Use this estimate for scheduling triage and staffing alignment.";
    }

    private static double ParseAndClamp(string text, double min, double max, double fallback)
    {
        if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
        {
            value = fallback;
        }

        if (value < min)
        {
            value = min;
        }

        if (value > max)
        {
            value = max;
        }

        return value;
    }
}

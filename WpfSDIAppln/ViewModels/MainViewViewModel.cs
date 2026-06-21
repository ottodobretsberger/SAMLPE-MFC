using System.Collections.ObjectModel;
using WpfSDIAppln.Models;

namespace WpfSDIAppln.ViewModels;

public class MainViewViewModel : ViewModelBase
{
    private readonly DocumentModel _document;

    public MainViewViewModel(DocumentModel document)
    {
        _document = document;

        MetricCards = new ObservableCollection<DashboardMetric>
        {
            new("Patients in Queue", "14", "Median wait: 11 min", "#0078D4"),
            new("Avg Exam Duration", "22 min", "Within target threshold", "#107C10"),
            new("Critical Follow-ups", "3", "2 pending physician review", "#C42B1C")
        };

        ThroughputActions = new ObservableCollection<string>
        {
            "Prep room 2 for contrast-enhanced exam (next slot: 09:40).",
            "Re-route low acuity cases to room 4 to balance queue.",
            "Confirm protocol update acknowledgment for all technologists.",
            "Escalate unresolved critical findings before shift handoff."
        };
    }

    public string Title => "Imaging Operations Dashboard";
    public string Subtitle => "Shift overview for modality rooms";

    public ObservableCollection<DashboardMetric> MetricCards { get; }
    public ObservableCollection<string> ThroughputActions { get; }
}

public record DashboardMetric(string Title, string Value, string Detail, string AccentColor);

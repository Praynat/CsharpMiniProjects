using System.Windows.Documents;

public class ListBoxItemData
{
    public string Title { get; set; }
    public string ImageSource { get; set; }
    public string IconSource { get; set; }
    public string PopupTitle { get; set; }
    public Inline[] PopupDescriptionInlines { get; set; }
    public string Identifier { get; set; }
    public double TitleFontSize { get; set; } = 30;
}

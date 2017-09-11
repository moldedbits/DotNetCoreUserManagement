namespace UserAppService.Interfaces
{
    public interface ICanGenerateHelpSampleData
    {
        void GenerateHelpSampleData();

        bool IsHelpSampleDataGenerated { get; set; }
    }
}
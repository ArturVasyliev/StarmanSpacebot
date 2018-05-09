namespace StarmanLibrary.Services
{
    public interface ICommunicationService
    {
        string GetHelloMessage();

        string GetMarsStatus();

        string GetMoonStatus();

        string GetIssStatusText();

        double[] GetIssPosition();

        string GetHumansInSpace();

        string GetSpacePics();

        string GetDefaultResponse();

        string GetHelp();

        string GetSettings();
    }
}
